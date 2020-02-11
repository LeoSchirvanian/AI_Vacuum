using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Brain
    {
        // Attributs
        private Problem problem;
        private Queue<string> tasklist;
        private List<Modelisation.Node> tree;

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

        // New method search
        public Queue<string> newSearch(int[,] initstate, List<int[,]> desireStates, int[] vacXY)
        {
            // First check if initstate is one of our desireStates
            foreach (int[,] d in desireStates)
            {
                // If one of them is equal, then no need to search a tasklist
                if ( isArrayEqual(initstate, d))
                {
                    // Stop method
                    Queue<string> q = new Queue<string>();
                    return q;
                }

                // If not, we need to search
                else
                {
                    
                    // Create a root node with initial state
                    Modelisation.Node root = new Modelisation.Node(
                            initstate,      // initial state
                            vacXY,          // vacXY
                            0,              // depth
                            100,            // heuristic
                            false,          // visited
                            "root"          // lastaction
                        );

                    // Clear tree
                    tasklist.Clear();

                    // Add the root lastAction
                    //tree.Add(root.getLastAction());

                    // Represent index of exploration : here it's Breadth first search
                    int borderindex = 0;

                    while (true)
                    {
                        Floor f = new Floor(initstate);

                        // If the vaccum is on jewel or dirt
                        if (f.isJewelDirt())
                        {
                            // TODO : We only need to pickup then to clean, could be optimized
                            //Queue<string> queue = new Queue<string>();
                            tasklist.Enqueue("pickup");
                            tasklist.Enqueue("clean");
                            return tasklist;
                        }

                        // If not
                        else
                        {
                            Modelisation.Node succ = problem.newSuccession(tree[borderindex]);  // Get successor
                            
                            tree.Add(succ);                         // Add it to the tree
                            tasklist.Enqueue(succ.getLastAction()); // add last action

                            borderindex++; // Increment borderIndex
                        }
                        
                    }
                }
                    

            }
        }

        public Queue<string> search(int[,] initstate, int[,] desiredstate)
        {
            // Create a root node with initial state
            Modelisation.Node rootn = new Modelisation.Node(
                    initstate,      // initial state
                    new[] { 0, 0 }, // vacXY
                    0,              // depth
                    0,              // pathcost
                    false,          // visited
                    "root"          // lastaction
                );
            // Create a goal node with desire state
            Modelisation.Node goaln = new Modelisation.Node(
                    desiredstate,   // desired state
                    new[] { 0, 0 }, // vacXY
                    0,              // depth
                    0,              // pathcost
                    false,          // visited
                    "goal"          // lastaction
                );

            // Clear all tree
            tree_fs.Clear();
            tree_fg.Clear();
            frontiere_fs.Clear();
            frontiere_fg.Clear();
            visited_fs.Clear();
            visited_fg.Clear();

            // Add root and goal node respectively in tree_fs and tree_fg
            tree_fs.Add(rootn);
            tree_fg.Add(goaln);

            // Represent index of exploration : here it's Breadth first search
            int borderindex = 0;

            while (true)
            {
                // Get goal retrosuccesors and root successors 
                Dictionary<string, Modelisation.Node> s_successors = problem.succession(tree_fs[borderindex]);
                Dictionary<string, Modelisation.Node> g_successors = problem.retrosuccession(tree_fg[borderindex]);

                // Loop root successors
                foreach (KeyValuePair<string,Modelisation.Node> entry in s_successors)
                {
                    // GetRange(index,count) : copy the list from index to index+count
                    // Check if one node of tree_fg is equal to entry.value (with method isArrayEqual)
                    if ( isPresent(entry.Value, tree_fg.GetRange(0, borderindex)) != null )
                    {
                        Console.WriteLine("Trouve un truc en A ");                                        // If true, print it
                        Modelisation.Node[] x = isPresent(entry.Value, tree_fg.GetRange(0, borderindex)); // isPresent return an array of equal nodes
                        return generateTasklist(x[0], x[1]);                                              // Stop execution and return equal node to get the full path with generateTaskList method
                    }
                    tree_fs.Add(entry.Value); // If not true, add the node to tree_fs and continue
                    
                }

                // Loop goal ancestors
                foreach (KeyValuePair<string, Modelisation.Node> entry in g_successors)
                {
                    // GetRange(index,count) : copy the list from index to index+count
                    // Check if one node of tree_fs is equal to entry.value (with method isArrayEqual)
                    if (isPresent(entry.Value, tree_fs.GetRange(0, borderindex)) != null)
                    {
                        Console.WriteLine("Trouve un truc en B ");                                        // If true, print it
                        Modelisation.Node[] x = isPresent(entry.Value, tree_fs.GetRange(0, borderindex)); // isPresent return an array of equal nodes
                        return generateTasklist(x[1], x[0]);                                              // Stop execution and return equal node to get the full path with generateTaskList method
                    }
                    tree_fg.Add(entry.Value);
                }
                borderindex++; // Increment borderIndex
            }

        }

        // Check if node is in visited node list, if true return an array of the same array, if not return null
        private Modelisation.Node[] isPresent(Modelisation.Node node, List<Modelisation.Node> visited)
        {
            foreach( Modelisation.Node n in visited)
            {
                if ( isArrayEqual(node.getState(), n.getState()) )
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
