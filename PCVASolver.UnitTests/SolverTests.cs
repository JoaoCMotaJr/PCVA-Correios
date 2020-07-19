using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PCVASolver.UnitTests
{
    public class SolverTests
    {
        private PCVAGraphSolver _solver { get; set; }
        List<City> _allCities { get; set; }
        [SetUp]
        public void Setup()
        {
            var pathLists1 = new List<RoadPath>()
            {
                new RoadPath("TESTE2", 1),
                new RoadPath("TESTE3", 3)
            };

            var pathLists2 = new List<RoadPath>()
            {
                new RoadPath("TESTE1", 1),
                new RoadPath("TESTE3", 3)
            };

            var pathLists3 = new List<RoadPath>()
            {
                new RoadPath("TESTE1", 1),
                new RoadPath("TESTE2", 3)
            };

            _allCities = new List<City>
            {
                new City() { Name = "TESTE1", Paths = pathLists1 },
                new City() { Name = "TESTE2", Paths = pathLists2 },
                new City() { Name = "TESTE3", Paths = pathLists3 }
            };

            _solver = new PCVAGraphSolver(_allCities);
        }

        [Test]
        public void GetInitialSolutionTest_ValidEntry_ShouldReturn()
        {
            //Arrange
            //Act
            var solution = _solver.GetInitialSolution("TESTE1", "TESTE2");

            //Asset
            Assert.AreEqual(1, solution.Count());
            Assert.AreEqual(1, solution.First().CitiesVisited.Count());
            Assert.AreEqual("TESTE1", solution.First().CitiesVisited.First().Name);
        }

        #region VerifyTest
        [Test]
        public void VerifyTest_InvalidOrigin_ShouldThrow()
        {
            //Arrange
            //Act - Asset
            Assert.Throws<ArgumentException>(() => _solver.VerifyInputs("INVALIDO", "TESTE2"));
        }

        [Test]
        public void VerifyTest_InvalidDestine_ShouldThrow()
        {
            //Arrange
            //Act - Asset
            Assert.Throws<ArgumentException>(() => _solver.VerifyInputs("TESTE1", "INVALIDO"));
        }

        [Test]
        public void VerifyTest_InvalidCity_ShouldThrow()
        {
            //Arrange
            var emptySolver = new PCVAGraphSolver(new List<City>());
            //Act - Asset
            Assert.Throws<ArgumentException>(() => emptySolver.VerifyInputs("TESTE1", "TESTE2"));
        }

        [Test]
        public void VerifyTest_ValidCity_ShouldNotThrow()
        {
            //Arrange
            //Act
            //Asset
            Assert.DoesNotThrow(() => _solver.VerifyInputs("TESTE1", "TESTE2"));
        }
        #endregion

        #region GenerateNextSolutions
        [Test]
        public void GetNextSolutionsTest_FirstStep_ShouldGenerate2Solutions()
        {
            //Arrange
            var solution = new Solution()
            {
                CitiesVisited = _allCities.Where(x => x.Name == "TESTE1").ToList(),
                IsSolved = false,
                TotalPath = 0
            };

            //Act
            var nextSolutions = _solver.GetNextSolutions(solution, "TESTE3");
            var SolutionToTest2 = nextSolutions
                .Where(x => x.CitiesVisited.Any(y => y.Name.Contains("TESTE2")));
            var SolutionToTest3 = nextSolutions
                .Where(x => x.CitiesVisited.Any(y => y.Name.Contains("TESTE3")));

            //Assert
            Assert.AreEqual(2, nextSolutions.Count());

            Assert.AreEqual(1, SolutionToTest2.Count());
            Assert.IsFalse(SolutionToTest2.First().IsSolved);
            Assert.AreEqual(1, SolutionToTest2.First().TotalPath);

            Assert.AreEqual(1, SolutionToTest3.Count());
            Assert.IsTrue(SolutionToTest3.First().IsSolved);
            Assert.AreEqual(3, SolutionToTest3.First().TotalPath);

        }

        [Test]
        public void GetNextSolutionsTest_SecondStep_ShouldGenerate2Solutions()
        {
            //Arrange
            var solution = new Solution()
            {
                CitiesVisited = _allCities.Where(x => x.Name == "TESTE1").ToList(),
                IsSolved = false,
                TotalPath = 0
            };

            solution.CitiesVisited.Add(_allCities.FirstOrDefault(x => x.Name == "TESTE2"));
            solution.TotalPath += 1;
            //Act
            var nextSolutions = _solver.GetNextSolutions(solution, "TESTE3");
            var SolutionToTest3 = nextSolutions.First();

            //Assert
            Assert.AreEqual(1, nextSolutions.Count());

            Assert.AreEqual(3, SolutionToTest3.CitiesVisited.Count());
            Assert.IsTrue(SolutionToTest3.IsSolved);
            Assert.AreEqual(4, SolutionToTest3.TotalPath);
        }
        #endregion

        [Test]
        public void DecideBestSolutionTest_Valid_ShouldReturnTheShortest()
        {
            //Arrange
            var solutionList = new List<Solution>()
            {
                new Solution() { IsSolved = true, TotalPath = 15 },
                new Solution() { IsSolved = false, TotalPath = 1 },
                new Solution() { IsSolved = true, TotalPath = 10 },

            };

            //Act
            var solution = PCVAGraphSolver.DecideBestSolution(solutionList);

            //Assert
            Assert.IsNotNull(solution);
            Assert.AreEqual(10, solution.TotalPath);
        }

        [Test]
        public void DecideBestSolutionTest_Invalid_ShouldReturnNull()
        {
            //Arrange
            var solutionList = new List<Solution>()
            {
                new Solution() { IsSolved = false, TotalPath = 15 },
                new Solution() { IsSolved = false, TotalPath = 1 },
                new Solution() { IsSolved = false, TotalPath = 10 },

            };

            //Act
            var solution = PCVAGraphSolver.DecideBestSolution(solutionList);

            //Assert
            Assert.IsNull(solution);
        }

        [Test]
        public void ResolveSmallestPathTest_Valid_ShouldReturn3()
        {
            //Arrenge

            //Act
            (var cities, var path) = _solver.ResolveSmallestPath("TESTE1", "TESTE3");

            //Assert
            Assert.AreEqual(2, cities.Length);
            Assert.AreEqual(3, path);
            Assert.AreEqual("TESTE1", cities[0]);
            Assert.AreEqual("TESTE3", cities[1]);
        }

        [Test]
        public void ResolveSmallestPathTest_Valid_ShouldReturn2()
        {
            //Arrenge

            //Act
            (var cities, var path) = _solver.ResolveSmallestPath("TESTE1", "TESTE2");

            //Assert
            Assert.AreEqual(2, cities.Length);
            Assert.AreEqual(1, path);
            Assert.AreEqual("TESTE1", cities[0]);
            Assert.AreEqual("TESTE2", cities[1]);
        }

        [Test]
        public void ResolveSmallestPathTest_Valid2_ShouldReturn3()
        {
            //Arrenge

            //Act
            (var cities, var path) = _solver.ResolveSmallestPath("TESTE2", "TESTE3");

            //Assert
            Assert.AreEqual(2, cities.Length);
            Assert.AreEqual(3, path);
            Assert.AreEqual("TESTE2", cities[0]);
            Assert.AreEqual("TESTE3", cities[1]);
        }

        [Test]
        public void ResolveSmallestPathTest_Invalid_ShouldThrow()
        {
            //Arrenge

            //Act - Assert
            Assert.Throws<ArgumentException>(() => _solver.ResolveSmallestPath("INVALIDO", "TESTE2"));
            Assert.Throws<ArgumentException>(() => _solver.ResolveSmallestPath("TESTE2", "INVALIDO"));
        }
    }
}