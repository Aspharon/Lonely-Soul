using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class Computer : HouseObject
    {
        public Computer(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 600;
            spriteName = "computer";
        }
    }
}
