﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LonelySoul.HouseObjects
{
    class Bed : HouseObject
    {
        public Bed(byte X, byte Y) : base(X, Y)
        {
            hauntTime = 120;
            spriteName = "bed";
        }
    }
}
