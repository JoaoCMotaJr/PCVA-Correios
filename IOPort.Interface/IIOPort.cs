using System;
using System.Collections.Generic;
using System.Text;

namespace IOPort.Interface
{
    public interface IIOPort
    {
        /// <summary>
        ///    Read all input file and return an array with all the content by lines.
        ///    The path is a relative path based on the Current Directory.
        /// </summary>
        /// <param name="filePath">Relative path/name of the file to be read, based on the current directory</param>
        /// <returns> A string array for each line in the file</returns>
        public string[] ReadInput(string filePath);
        /// <summary>
        ///     Write a text in a file, saved in relative path based on the Current Directory.
        ///     Will override previous files with the same name
        /// </summary>
        /// <param name="text">String array with the lines</param>
        /// <param name="filePath">Relative path/name of the file to be write, based on the current directory</param>
        public void WriteOutput(string[] text, string filePath);
    }
}
