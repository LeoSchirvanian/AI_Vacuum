using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Aspirateur
    {
        private Sensor sensor;
        private Actors actors;
        private Brain brain;
        private Queue<string> tasklist;
        private int[,] state;
        private int[,] desire;

        public Aspirateur()
        {
            
            sensor = new Sensor();
            actors = new Actors();
            brain  = new Brain();
            tasklist = new Queue<string>();
            desire = calculateDesire();

        }

        public void wake()
        {
            state = sensor.getSurroundings();
            tasklist = brain.search(state, desire);
            //tasklist.Enqueue( brain.search(state, desire).Dequeue());

            /*
            string[] tasks = tasklist.ToArray();
            for(int i = 0; i < tasks.Length; i++)
            {
                Console.WriteLine("TASK " + i + " : " + tasks[i]);
            }
            */
            // actions at a time to avoid do nothing loop
            actors.execute(tasklist.Dequeue());
            actors.execute(tasklist.Dequeue());
        }

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
