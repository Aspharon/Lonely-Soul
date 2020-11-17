using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class Roll : HouseObject
    {
        public Roll(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 60;
            spriteName = "roll";
        }
    }
}
