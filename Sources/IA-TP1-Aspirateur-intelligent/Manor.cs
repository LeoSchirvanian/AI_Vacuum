using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace IA_TP1_Aspirateur_intelligent
{
    public sealed class Manor
    {
        // Constante
        private static int GRID_SIZE = 3;

        // Attributs
        private static Manor instance;
        private Floor floor;
        private static Aspirateur aspirateur;
        private static Schmutzfabrik schmutzfabrik;
        private static Juwelfabrik juwelfabrik;
        
        private int[] aspXY;

        private Random random = new Random();


        // Constructeur
        private Manor()
        {
            floor = new Floor(GRID_SIZE);
            schmutzfabrik = new Schmutzfabrik(probmatrixGenerator());
            juwelfabrik = new Juwelfabrik(probmatrixGenerator());
            aspirateur = new Aspirateur();
            aspXY = floor.getAspXY();
        }

        // Create a instance of manor object if null and return manor
        public static Manor getInstance()
        {
            if (instance == null)
            {
                instance = new Manor();
            }
            return instance;
        }

        // Create a probability matrix with random number from 0 to 100
        private int[,] probmatrixGenerator()
        {
            int x = floor.getState().GetLength(0);
            int y = floor.getState().GetLength(1);

            int[,] probmatrix = new int[x,y];

            for (int i = 0; i < probmatrix.GetLength(0); i++)
            {
                for (int j = 0; j < probmatrix.GetLength(1); j++)
                {
                    probmatrix[i, j] = random.Next(101);
                }
            }

            return probmatrix;
        }

        // Each 10s, randomly try or not to create dirt with the dirt method of schmutzfeabrik object
        private void schmutzfabrikThread()
        {
            while(true)
            {
                schmutzfabrik.dirty(floor);

                // Sleep 10s
                Thread.Sleep(10000);
            }
        }

        // Each 10s, randomly try or not to create jewel with the drop method of juwelfabrik object
        private void juwelfabrikThread()
        {
            while (true)
            {
                juwelfabrik.drop(floor);

                // Sleep 10s
                Thread.Sleep(10000);
            }
        }

        // Each second, the vaccum is awake 
        private void vaccumThread()
        {
            while(true)
            {
                aspirateur.wake();

                // Sleep 1s
                Thread.Sleep(1000);
            }
        }

        // Start the manor and create a schmutzfabrikThread, a juwelfabrikThread, a vaccumThread, start them and print floor every 2 seconds
        public void setAlive()
        {
            Thread schmutzThread = new Thread(new ThreadStart(schmutzfabrikThread));
            Thread juwelThread = new Thread(new ThreadStart(juwelfabrikThread));
            Thread vacThread = new Thread(new ThreadStart(vaccumThread));

            schmutzThread.Start();
            juwelThread.Start();
            vacThread.Start();

            while (true)
            {
                //vacThread.Join();
                printFloorState();
                //Console.WriteLine("THREAD : " + vacThread.ThreadState);
                //Console.ReadLine();
                Thread.Sleep(2000);
                
            }
        }

        // Print the floor
        private void printFloorState()
        {
            int[,] state = floor.getState();

            string line;

            Console.WriteLine("* -  -  -  -  -  *");

            for (int i = 0; i < state.GetLength(0); i++)
            {
                line = "|";
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    line += ' ' + state[i, j].ToString() + ' ';
                }

                line += '|';

                Console.WriteLine(line);
            }

            Console.WriteLine("* -  -  -  -  -  *");
        }

        // Get floor state 
        public int[,] getFloorState()
        {
            return floor.getState();
        }

        // Get floor object
        public Floor getFloor()
        {
            return floor;
        }

        // Get vaccum position
        public int[] getAspXY()
        {
            return (int[])floor.getAspXY().Clone();
            throw new Exception("Did not find the vaccum");
        }

        // Get initial state
        public int[,] DEBUG_getDesire()
        {
            return floor.getInitialState();
        }

        // Get grid size
        public int getGridSize()
        {
            return 3;
        }

        // Compare array and return a boolean
        public bool isArrayEqual(int[,] a, int[,] b)
        {
            if ( (a.GetLength(0) != b.GetLength(0)) || (a.GetLength(1) != b.GetLength(1)))
            {
                return false;
            }

            for (int i=0; i < a.GetLength(0); i++)
            {
                for (int j=0; j < a.GetLength(1); j++)
                {
                    if ( a[i,j] != b[i,j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
