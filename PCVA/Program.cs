using IOPort;
using IOPort.Interface;
using PCVASolver;
using PCVASolver.Interface;
using System;
using System.Collections.Generic;

namespace PCVA
{
    class Program
    {
        static void Main(string[] args)
        {
            //Emulando DI
            IIOPort _IOPort = new IOPortAdapter();
            IPCVASolver _pcvaSolver = new PCVAGraphSolver();

            var arcsFileName = "Files/trechos.txt";
            var problemsFileName = "Files/encomendas.txt";
            var solutionFileName = "Files/rotas.txt";
            try
            {
                ExecuteSolver(_IOPort, _pcvaSolver, arcsFileName, problemsFileName, solutionFileName);
                Console.WriteLine($"Output in {solutionFileName}; Press any key to close;");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception thrown: {e.Message}; Press any key to close;");
                Console.ReadKey();
            }
        }

        private static void ExecuteSolver(IIOPort _IOPort, IPCVASolver _pcvaSolver, string arcsFileName, 
            string problemsFileName, string solutionFileName)
        {
            var arcs = _IOPort.ReadInput(arcsFileName);
            var problems = _IOPort.ReadInput(problemsFileName);

            var output = new List<string>();

            _pcvaSolver.ConstructGraph(arcs);
            foreach (var problem in problems)
            {
                var names = problem.Split(' ');
                (string[] vertix, int pathCost) = _pcvaSolver.ResolveSmallestPath(names[0], names[1]);

                output.Add(string.Join(' ', vertix) + " " + pathCost);
            }

            _IOPort.WriteOutput(output.ToArray(), solutionFileName);
        }
    }
}
