using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PCVASolver
{
    public class Solver
    {
        IEnumerable<City> AllCities { get; set; }

        public Solver(IEnumerable<City> cities)
        {
            AllCities = cities;
        }

        public (string[], int) ResolveSmallestPath(string startName, string endName)
        {
            VerifyInputs(startName, endName);

            var Solutions = GetInitialSolution(startName).ToList();

            while (Solutions.Any(x => x.IsSolved == false))
            {
                GenerateNextSolutions(Solutions, endName);
            }

            Solution bestSolution = DecideBestSolution(Solutions);

            return (bestSolution.CitiesVisited.Select(x => x.Name).ToArray(), bestSolution.TotalPath);
        }

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

        public IEnumerable<Solution> GetInitialSolution(string startName)
        {
            var initialSolution = new Solution()
            {
                CitiesVisited = AllCities.Where(x => x.Name == startName).ToList(),
                TotalPath = 0,
                IsSolved = false,
                IsValid = false
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
    }
}
