using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LonelySoul
{
    public class Person : GameObject
    {
        public Texture2D sprite;

        public Astar astar;
        public Stack<Node> path;
        public Node target;
        SpriteEffects spriteEffects;

        public Person()
        {
            position.X = 2 * 16;
            position.Y = 3 * 16;
            SetGrid();
            astar = new Astar(SetGrid());
            path = new Stack<Node>();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, Vector2.Zero, 1, spriteEffects, 0);
        }

        public override void Update(GameTime gameTime)
        {
            Walk();
        }

            public void WalkTo(Vector2 pos)
        {
            //if (FacingPos(pos / 16))
            {
                path = astar.FindPath(position / 16, pos / 16);
            }
            //else Face(pos / 16);
        }

        void Walk()
        {
            if (target == null)
            {
                if (path == null)
                    return;
                if (path.Count == 0)
                    return;
                else
                    target = path.Pop();
            }
            Vector2 tarPosNor = target.Position * 16;
            position += Vector2.Normalize(tarPosNor - position);

            Face(target.Position);

            if (position == tarPosNor)
                target = null;
        }

        bool FacingPos(Vector2 pos)
        {
            pos = pos * 16;
            if ((pos.X - position.X > 0 && spriteEffects == SpriteEffects.FlipHorizontally) | (pos.X - position.X < 0 && spriteEffects == SpriteEffects.None))
                return true;
            else return false;
        }

        void Face(Vector2 pos)
        {
            pos = pos * 16;
            if (Vector2.Normalize(pos - position).X > 0)
                spriteEffects = SpriteEffects.FlipHorizontally;
            else if (Vector2.Normalize(pos - position).X < 0)
                spriteEffects = SpriteEffects.None;
        }

        private List<List<Node>> SetGrid()
        {
            string[] stringGrid = new string[9];

            stringGrid[0] = "xxxxxxxxxxxxxx";
            stringGrid[1] = "xxxoxxxoxxooox";
            stringGrid[2] = "xoooxooooxooxx";
            stringGrid[3] = "xooxxooooxooox";
            stringGrid[4] = "xoooxooooxxoxx";
            stringGrid[5] = "xoooooooooooox";
            stringGrid[6] = "xoooooooooooox";
            stringGrid[7] = "xoooooooooooox";
            stringGrid[8] = "xxxxxxxxxxxxxx";

            List<List<Node>> grid = new List<List<Node>>();
            for (int x = 0; x < 14; x++)
            {
                List<Node> loopList = new List<Node>();
                for (int y = 0; y < 9; y++)
                {
                    Node node;
                    if (stringGrid[y][x] == 'x')
                        node = new Node(new Vector2(x, y), false);
                    else
                        node = new Node(new Vector2(x, y), true);
                    loopList.Add(node);
                }
                grid.Add(loopList);
            }

            return grid;
        }
    }


    public class Node
    {
        // Change this depending on what the desired size is for each element in the grid
        public static int NODE_SIZE = 1;
        public Node Parent;
        public Vector2 Position;
        public Vector2 Center
        {
            get
            {
                return new Vector2(Position.X + NODE_SIZE / 2, Position.Y + NODE_SIZE / 2);
            }
        }
        public float DistanceToTarget;
        public float Cost;
        public float Weight;
        public float F
        {
            get
            {
                if (DistanceToTarget != -1 && Cost != -1)
                    return DistanceToTarget + Cost;
                else
                    return -1;
            }
        }
        public bool Walkable;

        public Node(Vector2 pos, bool walkable, float weight = 1)
        {
            Parent = null;
            Position = pos;
            DistanceToTarget = -1;
            Cost = 1;
            Weight = weight;
            Walkable = walkable;
        }
    }

    public class Astar
    {
        List<List<Node>> Grid;
        int GridRows
        {
            get
            {
                return Grid[0].Count;
            }
        }
        int GridCols
        {
            get
            {
                return Grid.Count;
            }
        }

        public Astar(List<List<Node>> grid)
        {
            Grid = grid;
        }

        public Stack<Node> FindPath(Vector2 Start, Vector2 End)
        {
            Node start = new Node(new Vector2((int)(Start.X / Node.NODE_SIZE), (int)(Start.Y / Node.NODE_SIZE)), true);
            Node end = new Node(new Vector2((int)(End.X / Node.NODE_SIZE), (int)(End.Y / Node.NODE_SIZE)), true);

            Stack<Node> Path = new Stack<Node>();
            List<Node> OpenList = new List<Node>();
            List<Node> ClosedList = new List<Node>();
            List<Node> adjacencies;
            Node current = start;

            // add start node to Open List
            OpenList.Add(start);

            while (OpenList.Count != 0 && !ClosedList.Exists(x => x.Position == end.Position))
            {
                current = OpenList[0];
                OpenList.Remove(current);
                ClosedList.Add(current);
                adjacencies = GetAdjacentNodes(current);


                foreach (Node n in adjacencies)
                {
                    if (!ClosedList.Contains(n) && n.Walkable)
                    {
                        if (!OpenList.Contains(n))
                        {
                            n.Parent = current;
                            n.DistanceToTarget = Math.Abs(n.Position.X - end.Position.X) + Math.Abs(n.Position.Y - end.Position.Y);
                            n.Cost = n.Weight + n.Parent.Cost;
                            OpenList.Add(n);
                            OpenList = OpenList.OrderBy(node => node.F).ToList<Node>();
                        }
                    }
                }
            }

            // construct path, if end was not closed return null
            if (!ClosedList.Exists(x => x.Position == end.Position))
            {
                return null;
            }

            // if all good, return path
            Node temp = ClosedList[ClosedList.IndexOf(current)];
            if (temp == null) return null;
            do
            {
                Path.Push(temp);
                temp = temp.Parent;
            } while (temp != start && temp != null);
            return Path;
        }

        private List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> temp = new List<Node>();

            int row = (int)n.Position.Y;
            int col = (int)n.Position.X;

            if (row + 1 < GridRows)
            {
                temp.Add(Grid[col][row + 1]);
            }
            if (row - 1 >= 0)
            {
                temp.Add(Grid[col][row - 1]);
            }
            if (col - 1 >= 0)
            {
                temp.Add(Grid[col - 1][row]);
            }
            if (col + 1 < GridCols)
            {
                temp.Add(Grid[col + 1][row]);
            }

            return temp;
        }
    }
}
