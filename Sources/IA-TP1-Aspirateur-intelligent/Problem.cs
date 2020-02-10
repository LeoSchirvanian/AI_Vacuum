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
            actions = new Dictionary<string, Action>();
            actions.Add("movedown", new Actions.MoveDown());
            actions.Add("moveleft", new Actions.MoveLeft());
            actions.Add("moveright", new Actions.MoveRight());
            actions.Add("moveup", new Actions.MoveUp());
            actions.Add("clean", new Actions.Clean());
            actions.Add("nothing", new Actions.Nothing());
            actions.Add("pickup", new Actions.Pickup());
        }

        // Test every actions possible given a node and add new states obtained in the dictionnary newstates
        public Dictionary<string, Modelisation.Node> succession(Modelisation.Node currentNode)
        {

            Dictionary<string, Modelisation.Node> newStates = new Dictionary<string, Modelisation.Node>();
            Floor testingFloor = new Floor(currentNode.getState());

            foreach (KeyValuePair<string, Action> entry in actions)
            {
                entry.Value.enact(testingFloor, currentNode.getVacXY());
                Modelisation.Node newnode = new Modelisation.Node(
                    testingFloor.getState(),
                    testingFloor.getAspXY(),
                    currentNode.getDepth() + 1,
                    currentNode.getPathcost() + entry.Value.getCost(),
                    false,
                    entry.Key,
                    currentNode
                    ) ;

                newStates.Add(entry.Key, newnode);
                testingFloor.reset(); // Return to the initial state

            }
            return newStates;
        }

        // Test every reverse actions possible given a node and add new states obtained in the dictionnary newstates
        public Dictionary<string, Modelisation.Node> retrosuccession(Modelisation.Node currentNode)
        {
            Dictionary<string, Modelisation.Node> newStates = new Dictionary<string, Modelisation.Node>();
            Floor testingFloor = new Floor(currentNode.getState());

            foreach (KeyValuePair<string, Action> entry in actions)
            {
                entry.Value.reverse(testingFloor, currentNode.getVacXY());
                Modelisation.Node newnode = new Modelisation.Node(
                    testingFloor.getState(),
                    testingFloor.getAspXY(),
                    currentNode.getDepth() + 1,
                    currentNode.getPathcost() + entry.Value.getCost(),
                    false,
                    entry.Key,
                    currentNode
                    );

                newStates.Add(entry.Key, newnode);
                testingFloor.reset();
            }
            return newStates;
        }

        // Test if we achieved our desire state
        public bool goalTest(int[,] tested)
        {
            return (tested == desire);
        }
    }
}
