using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class Sink : HouseObject
    {
        public Sink(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 180;
            spriteName = "sink";
        }
    }
}
