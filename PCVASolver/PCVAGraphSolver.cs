using PCVASolver.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCVASolver
{
    /// <summary>
    /// Solver for the PCVA (Asymmetric Salesman Problem) based on graphs and Breadth-First Search - BFS.
    /// </summary>
    public class PCVAGraphSolver : IPCVASolver
    {
        public List<City> AllCities { get; set; }

        public PCVAGraphSolver()
        {
            AllCities = new List<City>();
        }

        public PCVAGraphSolver(List<City> cities)
        {
            AllCities = cities;
        }

        public void ConstructGraph(string[] arcs)
        {
            foreach(var arc in arcs)
            {
                var split = arc.Split(' ');
                if(split.Length < 3)
                {
                    throw new ArgumentException("Invalid input line: " + arc);
                }

                City originCity = GetOrAddCity(split[0]);
                City destinyCity = GetOrAddCity(split[1]);

                int distance;
                if(!int.TryParse(split[2], out distance))
                {
                    throw new ArgumentException("Invalid distance in line: " + "arc");
                }

                originCity.AddPathTo(destinyCity.Name, distance);
            }
        }

        public City GetOrAddCity(string cityName)
        {
            var city = AllCities.FirstOrDefault(x => x.Name == cityName);
            if (city == null)
            {
                city = new City()
                {
                    Name = cityName,
                    Paths = new List<RoadPath>()
                };
                AllCities.Add(city);
            }

            return city; 
        }

        public (string[], int) ResolveSmallestPath(string originName, string destineName)
        {
            VerifyInputs(originName, destineName);

            var Solutions = GetInitialSolution(originName, destineName).ToList();

            while (Solutions.Any(x => x.IsSolved == false))
            {
                GenerateNextSolutions(Solutions, destineName);
            }

            Solution bestSolution = DecideBestSolution(Solutions);

            if (bestSolution == null) //Edge-case where the graph has no solution possible
                throw new ArgumentException($"No path connects both cities: {originName} to {destineName}");
            return (bestSolution.CitiesVisited.Select(x => x.Name).ToArray(), bestSolution.TotalPath);
        }

        #region private Methods
        public void VerifyInputs(string startName, string endName)
        {
            if(AllCities != null && !AllCities.Any())
            {
                throw new ArgumentException("No city was informed");
            }
            if(!AllCities.Any(x => x.Name == startName))
            {
                throw new ArgumentException("Invalid origin city name");
            }
            if (!AllCities.Any(x => x.Name == endName))
            {
                throw new ArgumentException("Invalid desitny city name");
            }
        }

        public IEnumerable<Solution> GetInitialSolution(string originName, string destineName)
        {
            var initialSolution = new Solution()
            {
                CitiesVisited = AllCities.Where(x => x.Name == originName).ToList(),
                TotalPath = 0,
                IsSolved = originName == destineName, //Edge-case where the orign is equal to the destine
            };

            return new List<Solution>() { initialSolution };
        }

        public void GenerateNextSolutions(List<Solution> solutions, string endName)
        {
            var currentSolution = solutions.First(x => x.IsSolved == false);
            IEnumerable<Solution> nextSolutions = GetNextSolutions(currentSolution, endName);
            solutions.Remove(currentSolution);
            var bestSolution = DecideBestSolution(solutions);
            if(bestSolution != null)
            {
                nextSolutions = nextSolutions.Where(x => x.TotalPath < bestSolution.TotalPath);
            }
            solutions.AddRange(nextSolutions);
        }

        public static Solution DecideBestSolution(IEnumerable<Solution> solutions)
         => solutions.Where(x => x.IsSolved == true).OrderBy(x => x.TotalPath).FirstOrDefault();

        public IEnumerable<Solution> GetNextSolutions(Solution currentSolution, string end)
        {
            var newSolutionList = new List<Solution>();
            var allVisitedCities = currentSolution.CitiesVisited.Select(x => x.Name);

            foreach (var path in currentSolution.CitiesVisited.Last().Paths)
            {
                if(allVisitedCities.Contains(path.DestineName))
                {
                    continue;
                }

                var nextCity = AllCities.FirstOrDefault(x => x.Name == path.DestineName);

                var newSolution = currentSolution.Clone();
                newSolution.CitiesVisited.Add(nextCity);
                newSolution.IsSolved = nextCity.Name == end;
                newSolution.TotalPath += path.Distance;

                newSolutionList.Add(newSolution);
            }

            return newSolutionList;
        }
        #endregion
    }
}
