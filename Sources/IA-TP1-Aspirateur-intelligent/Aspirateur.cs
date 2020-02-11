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
        private int[,] state;
        private int[,] desire;
        private List<int[,]> desireStates;

        // Constructor
        public Aspirateur()
        {
            sensor = new Sensor();
            actors = new Actors();
            brain  = new Brain();
            tasklist = new Queue<string>();
            desire = calculateDesire();
            desireStates = calculateDesireState();

        }

        // Awake the vaccum, which analyze the environment and add tasks to do in its tasklist to achieve desire in desire matrix
        public void wake()
        {
            // Get the state of the room
            state = sensor.getSurroundings();
            // Get the path : tasklist
            //tasklist = brain.search(state, desire); // TODO: Change desire to desireStates
            tasklist = brain.newSearch(state, desireStates, sensor.getVacXY());

            // Execute tasklist
            actors.execute(tasklist.Dequeue());
            actors.execute(tasklist.Dequeue());
        }

        // Create a desire matrix filled with 0 except in 0,0 with a 1
        private int[,] calculateDesire()
        {
            int gridsize = 3;
            desire = new int[gridsize, gridsize];

            for (int i = 0; i < gridsize; i++)
            {
                for (int j = 0; j < gridsize; j++)
                {
                    desire[i, j] = 0;
                }

            }
            desire[0, 0] = 1;
            return desire;
        }


        // Create 9 differents desire states : 9 positions of vaccum in a clean room
        private List<int[,]> calculateDesireState()
        {
            List<int[,]> desireStates = new List<int[,]>();
            int gridsize = 3;

            int[,] desire = new int[gridsize, gridsize];

            // Null matrix
            for (int i = 0; i < gridsize; i++)
            {
                for (int j = 0; j < gridsize; j++)
                {
                    desire[i, j] = 0;
                }

            }

            // Copy desire matrix
            int[,] copyDesire = (int[,])desire.Clone();

            // Add 1 at every different cell and add it to the list 
            for (int i = 0; i < gridsize; i++)
            {
                for (int j = 0; j < gridsize; j++)
                {
                    desire[i, j] = 1;         // Add 1
                    desireStates.Add(desire); // Add the new desire state in the list
                    desire = copyDesire;      // Reset desire
                }

            }

            return desireStates;
        }

    }
}
