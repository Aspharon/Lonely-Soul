using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class Piano : HouseObject
    {
        public Piano(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 30;
            spriteName = "piano";
        }
    }
}
