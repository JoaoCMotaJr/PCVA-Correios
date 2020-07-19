using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCVASolver
{
    public class Solution
    {
        public List<City> CitiesVisited { get; set; }
        public int TotalPath { get; set; }
        public bool IsSolved { get; set; }

        public Solution Clone()
        {
            var cities = CitiesVisited.ConvertAll(x => x);

            return new Solution()
            {
                CitiesVisited = cities,
                TotalPath = TotalPath,
                IsSolved = IsSolved,
            };
        }
    }
}
