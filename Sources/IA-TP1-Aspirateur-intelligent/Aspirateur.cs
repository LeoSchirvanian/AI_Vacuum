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
            tasklist = brain.search(state, desireStates, vacXY);

            // Find a path
            if(tasklist.Count > 0)
            {
                performance += 10;
            }

            if(tasklist.Count > 15)
            {
                performance = -100000;
            }

            // Execute the queue
            int t = tasklist.Count;
            for (int i = 0; i < t; i++)
            {
                actors.execute(tasklist.Dequeue());
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
