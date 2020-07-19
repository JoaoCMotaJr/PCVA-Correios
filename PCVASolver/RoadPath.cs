using System;
using System.Collections.Generic;
using System.Text;

namespace PCVASolver
{
    public class RoadPath
    {
        public RoadPath(string destineName, int distance)
        {
            DestineName = destineName;
            Distance = distance;
        }

        public readonly string DestineName;
        public readonly int Distance;
    }
}
