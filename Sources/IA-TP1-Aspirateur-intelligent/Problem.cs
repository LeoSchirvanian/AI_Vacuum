using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Problem
    {
        // Attributs
        private int[,] desire;
        private int[,] state;
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

        // Test every actions given a node and return only a node with the lowest heuristic
        public Modelisation.Node newSuccession(Modelisation.Node currentNode)
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

                int h = testingFloor.heuristique(cooList); //heuristic
                
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

        public int[,] getState()
        {
            return state;
        }

        // Test if we achieved our desire state
        public bool goalTest(int[,] tested)
        {
            return (tested == desire);
        }
    }
}
