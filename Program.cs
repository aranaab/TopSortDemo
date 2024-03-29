/* Create a C# program that solves the following dependency problem:
 
A person needs to figure out which order his/her clothes need to be put on. 
The person creates a file that contains the dependencies.
 
This input is a declared array of dependencies with the [0] index being the dependency and the [1] index being the item. 
 
A simple input would be:
 
                var input = new string[,]
                               {
                                                                //dependency    //item
                                               {"t-shirt",             "dress shirt"},
                                                        {"dress shirt", "pants"},
                                                        {"dress shirt", "suit jacket"},
                                                        {"tie",                           "suit jacket"},
                                                        {"pants",     "suit jacket"},
                                                        {"belt",         "suit jacket"},
                                                        {"suit jacket", "overcoat"},
                                                        {"dress shirt", "tie"},
                                                        {"suit jacket", "sun glasses"},
                                                        {"sun glasses", "overcoat"},
                                                        {"left sock",                "pants"},
                                                        {"pants",     "belt"},
                                                        {"suit jacket", "left shoe"},
                                                        {"suit jacket", "right shoe"},
                                                        {"left shoe",               "overcoat"},
                                                        {"right sock",             "pants"},
                                                        {"right shoe",            "overcoat"},
                                                        {"t-shirt",    "suit jacket"}
                                       };
 
In this example, it shows that they must put on their left sock before their pants. Also, 
they must put on their pants before their belt.
 
From this data, write a program that provides the order that each object needs to be put on.
 
The output should be a line-delimited list of objects. If there are multiple objects that
can be done at the same time, list each object on the same line, alphabetically 
sorted, comma separated.
 
Therefore, the output for this sample file would be:
 
left sock,right sock, t-shirt
dress shirt
pants, tie
belt
suit jacket
left shoe, right shoe, sun glasses
overcoat
 
Evaluation Criteria
 
You will be evaluated on the following criteria:
 
1.            Correctness of the solution
2.            Algorithmic, logic, and programming skills
3.            Performance considerations
4.            Design and code structure (modular, etc)
5.            Coding style
6.            Usability
7.            Testability
8.            Documentation
 */

using System;
using System.Collections.Generic;

namespace TopoSortDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Group_DirectDependencies();
            
            //Sort_Cycle_Exception();
            
        }
        
        static void Group_DirectDependencies()
        {

            //We could get an exception here. For example if there is a cycle exception. This means that if you can take a node, follow all of it's edges, and come back to the node. Use Try/Catch block for minimal exception handling
            try{

            //This is the primary data input. A list of items and their dependencies. Put into variables so that each item is created once
            var j = new Item("t-shirt");
            var a = new Item("dress shirt", j);
            var k = new Item("left sock");
            var l = new Item("right sock");
            var b = new Item("pants", a,k,l);
            var g = new Item("belt", b);
            var c = new Item("suit jacket", a,b,g);
            var f = new Item("sun glasses", c);
            var h = new Item("left shoe", c);
            var i = new Item("right shoe", c);  
            var d = new Item("overcoat", c, f, h, i);
            var e = new Item("tie", a);

            //Create an unsorted list of all items to perform the topological sort        
            var unsorted = new[] { a, b, c, d, e, f, g, h,i,j,k,l };

            
            var sorted = TopologicalSort.Group(unsorted, x => x.Dependencies);

            Print(sorted);
            //Console.WriteLine("Group Direct Dependencies");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        
    
        static void Print<T>(IList<ICollection<T>> items)
        {
            
            foreach (var group in items)
            {
                List<string> list = new List<string>();
                foreach (var item in group)
                {
                    
                    
                    list.Add(item.ToString());

                    //Console.Write(item);
                    //Console.Write(", ");
                    
                }
                //Sort the group of items in alphabetical order
                //***Could be some room for performance optimization here. Lists with < 1 item or lists that are already sorted could skip sorting
                list.Sort();
                int j = 0;
                foreach (var str in list)
                
                {
                    
                
                Console.Write(list[j]);
                j++;

                //Only print a comma when printing the second to last item
                    if (j != list.Count())
                    {
                        
                        Console.Write(", ");
                    
                    }
                }
                //Put easch group of items on a new line
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
