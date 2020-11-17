using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class Bath : HouseObject
    {
        public Bath(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 300;
            spriteName = "bath";
        }
    }
}
