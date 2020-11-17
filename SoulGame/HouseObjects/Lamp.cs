using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class Lamp : HouseObject
    {
        public Lamp(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 120;
            spriteName = "lamp";
        }
    }
}
