using IOPort.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IOPort
{
    /// <summary>
    /// Adapter to manupulate input and output of text files.
    /// </summary>
    public class IOPortAdapter : IIOPort
    {
        public string[] ReadInput(string filePath)
        {
            var filePathfull = Path.Combine(Environment.CurrentDirectory, filePath);

            return File.ReadAllLines(filePathfull);
        }

        public void WriteOutput(string[] text, string filePath)
        {
            var filePathfull = Path.Combine(Environment.CurrentDirectory, filePath);

            using StreamWriter outputFile = new StreamWriter(filePathfull, false);
            foreach (var line in text)
                outputFile.WriteLine(line);
            outputFile.Close();
        }
    }
}
