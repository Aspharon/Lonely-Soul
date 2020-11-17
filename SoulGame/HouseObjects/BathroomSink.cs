using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class BathroomSink : HouseObject
    {
        public BathroomSink(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 180;
            spriteName = "bathroomSink";
        }
    }
}
