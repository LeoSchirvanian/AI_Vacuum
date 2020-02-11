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

        // Constructor
        public Brain()
        {
            problem = new Problem();                      // Problem
            tasklist = new Queue<string>();               // Tasklist
            tree = new List<Modelisation.Node>();         // Tree

        }

        // New method search
        public Queue<string> newSearch(int[,] initstate, List<int[,]> desireStates, int[] vacXY)
        {
            // First check if initstate is one of our desireStates
            //*
            foreach (int[,] d in desireStates)
            {
                // If one of them is equal, then no need to search a tasklist
                if (isArrayEqual(initstate, d))
                {
                    // Stop method
                    Queue<string> q = new Queue<string>();
                    return q;
                }
            }
            //*/

            // Clear tree
            tree.Clear();
            tasklist.Clear();

            // Create a root node with initial state
            Modelisation.Node root = new Modelisation.Node(
                    initstate,      // initial state
                    vacXY,          // vacXY
                    0,              // depth
                    100,            // heuristic
                    false,          // visited
                    "nothing"          // lastaction
                );

            tree.Add(root);

            // Represent index of exploration : here it's Breadth first search
            int borderindex = 0;

            bool b = true;

            while (b)
            {
                // We check if the last node state of tree is on dirt or jewel
                Floor f = new Floor(tree[borderindex].getState());

                // If the vaccum is on jewel or dirt
                if (f.isJewelDirt() != 0)
                {
                    switch (f.isJewelDirt())
                    {
                        case 3:
                            tasklist.Enqueue("pickup");
                            b = false;
                            break;
                        case 5:
                            tasklist.Enqueue("clean");
                            b = false;
                            break;
                        case 7:
                            tasklist.Enqueue("pickup");
                            tasklist.Enqueue("clean");
                            b = false;
                            break;
                        default:
                            break;
                    }
                    
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

            return tasklist;
                  
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
