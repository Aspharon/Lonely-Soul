using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class Shower : HouseObject
    {
        public Shower(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 90;
            spriteName = "shower";
        }
    }
}
