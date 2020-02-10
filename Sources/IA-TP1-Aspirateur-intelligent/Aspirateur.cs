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

        // Constructor
        public Aspirateur()
        {
            sensor = new Sensor();
            actors = new Actors();
            brain  = new Brain();
            tasklist = new Queue<string>();
            desire = calculateDesire();

        }

        // Awake the vaccum, which analyze the environment and add tasks to do in its tasklist to achieve desire in desire matrix
        public void wake()
        {
            // Get the state of the room
            state = sensor.getSurroundings();
            // Get the path : tasklist
            tasklist = brain.search(state, desire);
            
            /*
            foreach(string a in tasklist)
            {
                Console.WriteLine(a);
            }
            */
            
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

    }
}
