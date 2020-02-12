using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Aspirateur
    {
        // Attributs
        private Sensor sensor;
        private Actors actors;
        private Brain brain;
        private Queue<string> tasklist;
        private int performance = 0;

        // Constructor
        public Aspirateur()
        {
            sensor = new Sensor();
            actors = new Actors();
            brain  = new Brain();
            tasklist = new Queue<string>();
        }

        // Awake the vaccum, which analyze the environment and add tasks to do in its tasklist to achieve desire in desire matrix
        public void wake()
        {
            // Get the state of the room
            int[,] state = sensor.getSurroundings();
            int[] vacXY = sensor.getVacXY();
            List<int[,]> desireStates = calculateDesireState();

            // Get the path : tasklist
            tasklist = brain.searchInforme(state, desireStates, vacXY);

            // Execute the queue
            int t = tasklist.Count;
            for (int i = 0; i < t; i++)
            {
                string dq = tasklist.Dequeue();

                // Performance update
                switch (dq)
                {
                    case "clean":
                        if( brain.isJewelDirt(state, vacXY) == 3 || brain.isJewelDirt(state, vacXY) == 7)
                        {
                            Console.WriteLine("Aspirate a jewel");
                            performance = -1000;
                        }
                        else
                        {
                            performance += 10;
                        }
                        break;

                    case "pickup":
                        if ( brain.isJewelDirt(state, vacXY) == 5)
                        {
                            Console.WriteLine("Pickup dirt");
                            performance = -100;
                        }
                        else
                        {
                            performance += 10;
                        }
                        break;

                    default:
                        break;
                }

                actors.execute(dq);
            }
            
        }


        // Create 9 differents desire states : 9 positions of vaccum in a clean room
        private List<int[,]> calculateDesireState()
        {
            List<int[,]> desireStates = new List<int[,]>();
            int gridsize = 5;

            int[,] desire = new int[gridsize, gridsize];

            // Copy desire matrix
            int[,] copyDesire = (int[,])desire.Clone();

            // Add 1 at every different cell and add it to the list 
            for (int i = 0; i < gridsize; i++)
            {
                for (int j = 0; j < gridsize; j++)
                {
                    desire[i, j] = 1;                          // Add 1
                    desireStates.Add(desire);                  // Add the new desire state in the list
                    desire = (int[,]) copyDesire.Clone();      // Reset desire
                }

            }

            return desireStates;
        }

        public int getPerformance()
        {
            return performance;
        }

    }
}
