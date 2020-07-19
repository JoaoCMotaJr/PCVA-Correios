using System;
using System.Collections.Generic;
using System.Text;

namespace PCVASolver
{
    public class City
    {
        public string Name { get; set; }
        public IEnumerable<RoadPath> Paths { get; set; }
    }
}
