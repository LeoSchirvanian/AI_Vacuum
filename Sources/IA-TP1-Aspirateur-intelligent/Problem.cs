using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Problem
    {
        private int[,] desire;

        private int[,] state;
        private Dictionary<string, Action> actions;
        
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

        public Dictionary<string, Modelisation.Node> succession(Modelisation.Node currentNode)
        {
            Dictionary<string, Modelisation.Node> newStates = new Dictionary<string, Modelisation.Node>();

            Floor testingFloor = new Floor(currentNode.getState());

            /*
            Console.WriteLine("Succession");
            Console.WriteLine("* -  -  -  -  -  *");
            string line;

            for (int i = 0; i < testingFloor.getState().GetLength(0); i++)
            {
                line = "|";
                for (int j = 0; j < testingFloor.getState().GetLength(1); j++)
                {
                    line += ' ' + testingFloor.getState()[i, j].ToString() + ' ';
                }

                line += '|';

                Console.WriteLine(line);
            }

            Console.WriteLine("* -  -  -  -  -  *");
            */
            

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

                
                /*
                Console.WriteLine("Apres : " + entry.Key);
                Console.WriteLine("* -  -  -  -  -  *");

                line = "";
                for (int i = 0; i < testingFloor.getState().GetLength(0); i++)
                {
                    line = "|";
                    for (int j = 0; j < testingFloor.getState().GetLength(1); j++)
                    {
                        line += ' ' + testingFloor.getState()[i, j].ToString() + ' ';
                    }

                    line += '|';

                    Console.WriteLine(line);
                }

                Console.WriteLine("* -  -  -  -  -  *");
                */

                newStates.Add(entry.Key, newnode);
                testingFloor.reset();
                //testingFloor = new Floor(currentNode.getState());
                //entry.Value.reverse(testingFloor, testingFloor.getAspXY());


            }
            

            return newStates;
        }

        public Dictionary<string, Modelisation.Node> retrosuccession(Modelisation.Node currentNode)
        {
            Dictionary<string, Modelisation.Node> newStates = new Dictionary<string, Modelisation.Node>();

            Floor testingFloor = new Floor(currentNode.getState());

            /*
            Console.WriteLine("Retrosuccession");
            Console.WriteLine("* -  -  -  -  -  *");
            string line;

            for (int i = 0; i < testingFloor.getState().GetLength(0); i++)
            {
                line = "|";
                for (int j = 0; j < testingFloor.getState().GetLength(1); j++)
                {
                    line += ' ' + testingFloor.getState()[i, j].ToString() + ' ';
                }

                line += '|';

                Console.WriteLine(line);
            }

            Console.WriteLine("* -  -  -  -  -  *");
            */

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
                //testingFloor = new Floor(currentNode.getState());
                //entry.Value.enact(testingFloor, testingFloor.getAspXY());
            }


            return newStates;
        }

        public bool goalTest(int[,] tested)
        {
            return (tested == desire);
        }
    }
}
