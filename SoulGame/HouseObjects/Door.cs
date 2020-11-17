using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class Door : HouseObject
    {
        public Door(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 60;
            spriteName = "door";
        }
    }
}
