﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTuriasCore.Models
{
    public class CoordinateModel
    {
        public long Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public override string ToString()
        {
            return $@"
COORDINATE
    Longitude: {Longitude}
    Latitude: {Latitude}
";
        }
    }
}
