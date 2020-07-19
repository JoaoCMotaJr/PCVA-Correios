using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCVASolver.UnitTests
{
    public class SolverInitializationTests
    {
        [Test]
        public void GetOrCreateCityTest_NewCity_ShouldAdd()
        {
            //Arrange
            var solver = new PCVAGraphSolver();

            //Act
            var city = solver.GetOrAddCity("newCity");

            //Assert
            Assert.AreEqual(1, solver.AllCities.Count());
            Assert.AreEqual("newCity", solver.AllCities.First().Name);
            Assert.AreEqual(0, solver.AllCities.First().Paths.Count());

            Assert.AreEqual("newCity", city.Name);
            Assert.AreEqual(0, city.Paths.Count());
        }

        [Test]
        public void GetOrCreateCityTest_GetCity_ShouldGet()
        {
            //Arrange
            var solver = new PCVAGraphSolver(new List<City>() { new City() { Name = "oldCity" } });

            //Act
            var city = solver.GetOrAddCity("oldCity");

            //Assert
            Assert.AreEqual(1, solver.AllCities.Count());
            Assert.AreEqual("oldCity", solver.AllCities.First().Name);
            Assert.AreEqual("oldCity", city.Name);
        }

        [Test]
        public void AddPathTo_AddNewPath_ShouldAdd()
        {
            //Arrange
            var city = new City();

            //Act
            city.AddPathTo("Destiny", 1);

            //Arrange
            Assert.AreEqual(1, city.Paths.Count());
            Assert.AreEqual("Destiny", city.Paths.First().DestineName);
        }

    }
}
