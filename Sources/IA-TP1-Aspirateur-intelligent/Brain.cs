using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Brain
    {
        // Attributs
        private Problem problem;
        private List<Modelisation.Node> tree_fs;
        private List<Modelisation.Node> tree_fg;
        private List<Modelisation.Node> frontiere_fs;
        private List<Modelisation.Node> frontiere_fg;
        private List<Modelisation.Node> visited_fs;
        private List<Modelisation.Node> visited_fg;


        // Constructor
        public Brain()
        {
            problem = new Problem();                      // Problem
            tree_fs = new List<Modelisation.Node>();      // List of node
            tree_fg = new List<Modelisation.Node>();      // List of node
            frontiere_fs = new List<Modelisation.Node>(); // List of node
            frontiere_fg = new List<Modelisation.Node>(); // List of node
            visited_fs = new List<Modelisation.Node>();   // List of node
            visited_fg = new List<Modelisation.Node>();   // List of node

        }

        // Generate a tasklist, first find the root node and descend the tree till finding the goal node and returning the action tasklist queue
        private Queue<string> generateTasklist(Modelisation.Node ns, Modelisation.Node ng)
        {
            //Modelisation.Node intersection = search(state);
            List<string> actions = new List<string>();

            // While we do not find the root node
            while(ns.getLastAction() != "root" )
            {
                actions.Insert(0, ns.getLastAction());
                ns = ns.getParent();  // We keep climbing the tree
            }

            // While we do not find the root node
            while (ng.getLastAction() != "goal")
            {
                actions.Add(ng.getLastAction());
                ng = ng.getParent();  // We keep descending the tree
            }

            // New queue filled with the 
            Queue<string> queue = new Queue<string>(actions);

            //Console.WriteLine("Tasklist returning ...");
            return queue;
        }


        public Queue<string> search(int[,] initstate, int[,] desiredstate)
        {
            // Create a root node with a initial state
            Modelisation.Node rootn = new Modelisation.Node(
                    initstate,      // initial state
                    new[] { 0, 0 }, // vacXY
                    0,              // depth
                    0,              // pathcost
                    false,          // visited
                    "root"          // lastaction
                );
            Modelisation.Node goaln = new Modelisation.Node(
                    desiredstate,   // desired state
                    new[] { 0, 0 }, // vacXY
                    0,              // depth
                    0,              // pathcost
                    false,          // visited
                    "goal"          // lastaction
                );

            // Clear
            tree_fs.Clear();
            tree_fg.Clear();
            frontiere_fs.Clear();
            frontiere_fg.Clear();
            visited_fs.Clear();
            visited_fg.Clear();

            // Add root and goal node
            tree_fs.Add(rootn);
            tree_fg.Add(goaln);

            int borderindex = 0;

            while (true)
            {
                //Console.WriteLine("Tour " + caca++);
                //Console.WriteLine("Compte : " + frontiere_fs.Count);
                Dictionary<string, Modelisation.Node> s_successors = problem.succession(tree_fs[borderindex]);
                
                Dictionary<string, Modelisation.Node> g_successors = problem.retrosuccession(tree_fg[borderindex]);

                //visited_fs.Add(frontiere_fs[0]);
                //visited_fg.Add(frontiere_fg[0]);

                //frontiere_fs.RemoveAt(0);
                //frontiere_fg.RemoveAt(0);



                foreach (KeyValuePair<string,Modelisation.Node> entry in s_successors)
                {
                    //if (isPresent(entry.Value, visited_fs) != null)
                    /*
                    if ( isPresent(entry.Value, tree_fs.GetRange(0, borderindex)) != null )
                    {
                        Console.WriteLine("Entry is present");
                        break;
                    }*/

                    //if (isPresent(entry.Value, visited_fg) != null)
                    if ( isPresent(entry.Value, tree_fg.GetRange(0, borderindex)) != null )
                    {
                        Console.WriteLine("Trouve un truc en A ");
                        //Modelisation.Node[] x = isPresent(entry.Value, visited_fg);
                        Modelisation.Node[] x = isPresent(entry.Value, tree_fg.GetRange(0, borderindex));
                        return generateTasklist(x[0], x[1]);
                    }
                    //frontiere_fs.Add(entry.Value);
                    tree_fs.Add(entry.Value);
                    
                }

                //Console.ReadLine();

                foreach(KeyValuePair<string, Modelisation.Node> entry in g_successors)
                {
                    //if (isPresent(entry.Value, visited_fg) != null)
                    /*
                    if (isPresent(entry.Value, tree_fg.GetRange(0, borderindex)) != null)
                    {
                        break;
                    } */
                    //if (isPresent(entry.Value, visited_fs) != null)
                    if (isPresent(entry.Value, tree_fs.GetRange(0, borderindex)) != null)
                    {
                        Console.WriteLine("Trouve un truc en B ");
                        //Modelisation.Node[] x = isPresent(entry.Value, visited_fs);
                        Modelisation.Node[] x = isPresent(entry.Value, tree_fs.GetRange(0, borderindex));
                        return generateTasklist(x[1], x[0]);
                    }
                    //frontiere_fg.Add(entry.Value);
                    tree_fg.Add(entry.Value);
                }

                //frontiere_fs.RemoveAt(0);
                //frontiere_fg.RemoveAt(0);

                //Console.ReadLine();
                borderindex++;
            }

        }

        // Check if node is in visited node list
        private Modelisation.Node[] isPresent(Modelisation.Node node, List<Modelisation.Node> visited)
        {
            foreach( Modelisation.Node n in visited)
            {
                //Console.WriteLine("Checked for presence");
                if ( isArrayEqual(node.getState(), n.getState()) )
                //if (node == n)
                {
                    return new[] { node, n };
                }
            }
            return null;
        }

        // Array equal method
        private bool isArrayEqual(int[,] a, int[,] b)
        {
            if ((a.GetLength(0) != b.GetLength(0)) || (a.GetLength(1) != b.GetLength(1)))
            {
                return false;
            }

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] != b[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
