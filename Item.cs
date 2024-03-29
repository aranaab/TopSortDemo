
/*
This namespace contains the class Item. This class is a string with the name of an item in the graph

Each item has a collection of zero or more dependencies. Each dependency is also an item
*/
namespace TopoSortDemo
{
    public class Item
    {
        public string Name { get; private set; }
        public Item[] Dependencies { get; private set; }

        public Item(string name, params Item[] dependencies)
        {
            Name = name;
            Dependencies = dependencies;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}