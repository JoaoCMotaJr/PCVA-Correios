using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCVASolver
{
    public class City
    {
        public City()
        {
            Paths = new List<RoadPath>();
        }

        public string Name { get; set; }
        public List<RoadPath> Paths { get; set; }

        public void AddPathTo(string name, int distance)
            => Paths.Add(new RoadPath(name, distance));
        
    }
}
