using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace UnitTests.Input
{
    public class InputTests
    {
        [Test]
        public void ReadPathsTest()
        {
            //Source https://www.c-sharpcorner.com/UploadFile/mahesh/how-to-read-a-text-file-in-C-Sharp/#:~:text=The%20File%20class%20provides%20two,and%20then%20closes%20the%20file.
            //arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Input/Files/trechos.txt");

            //act
            string[] lines = File.ReadAllLines(filePath);

            //assert
            Assert.AreNotEqual(lines.Count(), 0);
        }

    }
}
