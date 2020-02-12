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

        // Allow to know if the vaccum is on a cell with dirt or/and jewel
        public int isJewelDirt(int[,] state, int[] aspXY)
        {
            int[] vacXY = aspXY;
            int x = vacXY[0];
            int y = vacXY[1];

            switch (state[x, y])
            {
                // Vaccum + jewel
                case 3:
                    return 3;

                // Vaccum + dirt
                case 5:
                    return 5;

                // Vaccum + dirt + jewel
                case 7:
                    return 7;

                default:
                    return 0;
            }
        }

        // Method search
        public Queue<string> searchInforme(int[,] initstate, List<int[,]> desireStates, int[] vacXY)
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
            //int borderindex = 0;

            List<Modelisation.Node> succ = new List<Modelisation.Node>();

            bool b = true;

            while (b)
            {
                // We check if the last node state of tree is on dirt or jewel
                //Floor f = new Floor(tree[borderindex].getState());
                int d = isJewelDirt(tree[tree.Count-1].getState(), tree[tree.Count - 1].getVacXY());

                // If the vaccum is on jewel or dirt
                if ( d != 0)
                {
                    switch (d)
                    {
                        case 3:
                            tasklist.Enqueue("pickup");
                            return tasklist;
                        case 5:
                            tasklist.Enqueue("clean");
                            return tasklist;

                        case 7:
                            tasklist.Enqueue("pickup");
                            tasklist.Enqueue("clean");
                            return tasklist;
                        default:
                            b = false;
                            break;
                    }
                    
                }

                // If not
                else
                {
                    tree = problem.successionInforme( tree[tree.Count - 1], tree ); // Get successor
                }

                tasklist = generatetasklist(tree);

            }

            return tasklist;
                  
        }

        // Generate tasklist from a node list
        public Queue<string> generatetasklist(List<Modelisation.Node> l)
        {
            Queue<string> tasklist = new Queue<string>();

            if(l.Count != 0)
            {
                Modelisation.Node node = l[l.Count -1];

                // While the node is not the root node, we add its lastaction to the tasklist
                while (node.getLastAction() != "nothing")
                {
                    tasklist.Enqueue(node.getLastAction());
                    node = node.getParent();
                }

                //tasklist.Enqueue(node.getLastAction());

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
