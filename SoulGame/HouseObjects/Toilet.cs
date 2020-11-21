using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class Toilet : HouseObject
    {
        public Toilet(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 150;
            spriteName = "toilet";
            walkPosition = position;
        }
    }
}
