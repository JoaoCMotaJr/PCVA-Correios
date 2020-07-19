namespace PCVASolver.Interface
{
    /// <summary>
    /// Interface to resolve the Asymmetric Salesman Problem (PCVA). 
    /// It requires the definition of inputs ("ConstructGraph") before executing the solution. 
    /// </summary>
    public interface IPCVASolver
    {
        /// <summary>
        ///     Method to inform all the inputs needed to construct the graph of the Asymetric Salesman Problem.
        /// arcs: The input is an array of string
        /// Every arc must be 2 names and an integer, all comma separated.
        /// * The first name in the origin vertix name, without spaces;
        /// * The second name is the destine vertix name, without spaces;
        /// * The integer is the weight to execute this "travel";
        /// Ex: "OriginName DestineNames 13"
        /// </summary>
        /// <param name="arcs">array with arcs</param>
        void ConstructGraph(string[] arcs);
        /// <summary>
        ///     Resolve the PCVA problem with one origin and one destine. 
        ///It do not consider the path to return from the destine to origin
        /// </summary>
        /// <param name="originName">Origin vertix name, based on the arcs configured in ConstructGraph</param>
        /// <param name="destineName">Destine vertix name, based on the arcs configured in ConstructGraph</param>
        /// <returns> A tuple containing a string array with all the cities passed, in order from origin to destine
        /// A integer containing the total cost of the path
        /// </returns>
        (string[], int) ResolveSmallestPath(string originName, string destineName);
    }
}
