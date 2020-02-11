using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Problem
    {
        // Attributs
        private Dictionary<string, Action> actions;
        
        // Constructor
        public Problem()
        {

            // Attributs
            actions = new Dictionary<string, Action>();

            // Add movement
            actions.Add("movedown", new Actions.MoveDown());
            actions.Add("moveleft", new Actions.MoveLeft());
            actions.Add("moveright", new Actions.MoveRight());
            actions.Add("moveup", new Actions.MoveUp());

        }

        // Calculate the heuristic number
        // Heuristic is equivalent to Manhattan distance, we first calculate the manhattan distance between a dirt/jewel and the vaccum and multiply it by a weight
        // This weight correspond to the value of the object detected by the vaccum (2, 4 or 6) returned by getJewelDirt method.
        // Hence the vaccum is more focused on big object (dirt + jewel > dirt > jewel)
        public int heuristique(List<(int, int, int)> cooList, int[] aspXY)
        {
            int h = 0;

            int[] vacXY = aspXY;
            int x = vacXY[0];
            int y = vacXY[1];


            for (int i = 0; i < cooList.Count; i++)
            {
                // Definition of heuristic 
                h += (Math.Abs(x - cooList[i].Item1) + Math.Abs(y - cooList[i].Item2)) * cooList[i].Item3;
            }
            return h;

        }

        // Test every actions given a node and return only a node with the lowest heuristic
        public Modelisation.Node succession(Modelisation.Node currentNode)
        {
            // Create
            Floor testingFloor = new Floor(currentNode.getState());

            // Init
            int minH = 100;
            Floor succFloor = new Floor(currentNode.getState());   // For now, the successor is the root itself
            string lastAct = "";

            List<(int, int, int)> cooList = testingFloor.getJewelDirt();

            // We determine the lowest heuristic successor
            foreach (KeyValuePair<string, Action> entry in actions)
            {
                entry.Value.enact(testingFloor, currentNode.getVacXY());

                int h = heuristique(cooList, testingFloor.getAspXY()); //heuristic
                
                // If the heuristic is below the actual min
                if (h < minH)
                {
                    minH = h;                                       // We store this heuristic
                    succFloor = new Floor(testingFloor.getState()); // We store this floor
                    lastAct = entry.Key;                            // And the best action
                }

                testingFloor.reset();                               // Return to the initial state
            }

            //After the foreach, we can create this best successor
            Modelisation.Node newState = new Modelisation.Node(
                succFloor.getState(),
                succFloor.getAspXY(),
                currentNode.getDepth() + 1,
                minH,
                false,
                lastAct,
                currentNode
            );

            return newState;

        }

    }
}
