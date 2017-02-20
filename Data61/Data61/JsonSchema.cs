using System.Collections.Generic;

namespace Data61
{
    /// <summary>
    /// Object to describe the DataSets 
    /// </summary>
    public class Data
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
    }

    /// <summary>
    /// Used to show the info of the Source DataTables
    /// </summary>
    public class Source
    {
        public Data Data { get; set; }
    }

    /// <summary>
    /// Used to show the info of the Target DataTables
    /// </summary>
    public class Target
    {
        public Data Data { get; set; }
    }

    /// <summary>
    /// Used to create Dataset objects
    /// </summary>
    public class Datasets
    {
        public List<Source> Source { get; set; }
        public List<Target> Target { get; set; }
    }

    /// <summary>
    /// Used to generate mapping objects
    /// </summary>
    public class Mappings
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public string Operation { get; set; }
    }

    /// <summary>
    /// Main object 
    /// </summary>
    class JsonSchema
    {
        public Datasets Datasets { get; set; }

        public List<Mappings> Mappings { get; set; }
    }
}
