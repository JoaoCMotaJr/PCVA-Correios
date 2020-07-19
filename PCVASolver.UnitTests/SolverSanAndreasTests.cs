using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCVASolver.UnitTests
{
    public class SolverSanAndreasTests
    {
        private PCVAGraphSolver _solver { get; set; }
        List<City> _allCities { get; set; }
        [SetUp]
        public void Setup()
        {
            var pathListsRC = new List<RoadPath>()
            {
                new RoadPath("LS", 2),
            };

            var pathListsLS = new List<RoadPath>()
            {
                new RoadPath("LV", 1),
                new RoadPath("SF", 1),
                new RoadPath("RC", 1),
            };

            var pathListsLV = new List<RoadPath>()
            {
                new RoadPath("BC", 1),
                new RoadPath("SF", 2),
                new RoadPath("LS", 1),
            };

            var pathListsSF = new List<RoadPath>()
            {
                new RoadPath("LV", 2),
                new RoadPath("WS", 1),
                new RoadPath("LS", 2),
            };

            var pathListsWS = new List<RoadPath>()
            {
                new RoadPath("SF", 2),
            };

            var pathListsBC = new List<RoadPath>()
            {
                new RoadPath("LV", 1),
            };

            _allCities = new List<City>
            {
                new City() { Name = "RC", Paths = pathListsRC },
                new City() { Name = "LS", Paths = pathListsLS },
                new City() { Name = "LV", Paths = pathListsLV },
                new City() { Name = "SF", Paths = pathListsSF },
                new City() { Name = "WS", Paths = pathListsWS },
                new City() { Name = "BC", Paths = pathListsBC },
                new City() { Name = "BH"}
            };

            _solver = new PCVAGraphSolver(_allCities);
        }

        [Test]
        public void ResolveSmallestPathTest_SAInput1_ShouldReturn()
        {
            //Arrenge

            //Act
            (var cities, var path) = _solver.ResolveSmallestPath("SF", "WS");

            //Assert
            Assert.AreEqual(2, cities.Length);
            Assert.AreEqual(1, path);
            Assert.AreEqual("SF", cities[0]);
            Assert.AreEqual("WS", cities[1]);
        }

        [Test]
        public void ResolveSmallestPathTest_SAInput2_ShouldReturn()
        {
            //Arrenge

            //Act
            (var cities, var path) = _solver.ResolveSmallestPath("LS", "BC");

            //Assert
            Assert.AreEqual(3, cities.Length);
            Assert.AreEqual(2, path);
            Assert.AreEqual("LS", cities[0]);
            Assert.AreEqual("LV", cities[1]);
            Assert.AreEqual("BC", cities[2]);
        }

        [Test]
        public void ResolveSmallestPathTest_SAInput3_ShouldReturn()
        {
            //Arrenge

            //Act
            (var cities, var path) = _solver.ResolveSmallestPath("WS", "BC");

            //Assert
            Assert.AreEqual(4, cities.Length);
            Assert.AreEqual(5, path);
            Assert.AreEqual("WS", cities[0]);
            Assert.AreEqual("SF", cities[1]);
            Assert.AreEqual("LV", cities[2]);
            Assert.AreEqual("BC", cities[3]);
        }

        [Test]
        public void ResolveSmallestPathTest_AlreadyInDestiny_ShouldReturn0()
        {
            //Arrenge

            //Act
            (var cities, var path) = _solver.ResolveSmallestPath("SF", "SF");

            //Assert
            Assert.AreEqual(1, cities.Length);
            Assert.AreEqual(0, path);
            Assert.AreEqual("SF", cities[0]);
        }

        [Test]
        public void ResolveSmallestPathTest_DestinyUnreachable_ShouldThrow()
        {
            //Arrenge

            //Act - Assert
            Assert.Throws<ArgumentException>(() => _solver.ResolveSmallestPath("SF", "BH"));
            Assert.Throws<ArgumentException>(() => _solver.ResolveSmallestPath("BH", "BC"));
        }
    }
}
