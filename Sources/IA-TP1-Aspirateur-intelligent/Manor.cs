using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace IA_TP1_Aspirateur_intelligent
{
    public sealed class Manor
    {
        private static int GRID_SIZE = 3;

        private static Manor instance;
        private Floor floor;
        private static Aspirateur aspirateur;
        private static Schmutzfabrik schmutzfabrik;
        private static Juwelfabrik juwelfabrik;
        
        private int[] aspXY;

        private Random random = new Random();

        private Manor()
        {
            floor = new Floor(GRID_SIZE);
            schmutzfabrik = new Schmutzfabrik(probmatrixGenerator());
            juwelfabrik = new Juwelfabrik(probmatrixGenerator());
            aspirateur = new Aspirateur();
            aspXY = floor.getAspXY();
        }

        public static Manor getInstance()
        {
            if (instance == null)
            {
                instance = new Manor();
            }
            return instance;
        }

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

        private void schmutzfabrikThread()
        {
            while(true)
            {
                schmutzfabrik.dirty(floor);

                Thread.Sleep(10000);
            }
        }

        private void juwelfabrikThread()
        {
            while (true)
            {
                juwelfabrik.drop(floor);

                Thread.Sleep(10000);
            }
        }

        private void vaccumThread()
        {
            while(true)
            {
                aspirateur.wake();

                Thread.Sleep(1000);
            }
        }

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

        public int[,] getFloorState()
        {
            return floor.getState();
        }


        public Floor getFloor()
        {
            return floor;
        }

        public int[] getAspXY()
        {
            return (int[])floor.getAspXY().Clone();
            /*
            if (((floor.getState()[aspXY[0],aspXY[1]] % 4) % 2) == 1)
            {
                return aspXY;
            }

            else
            {
                for (int i = 0; i < floor.getState().GetLength(0); i++)
                {
                    for (int j = 0; j < floor.getState().GetLength(1); j++)
                    {
                        if (((floor.getState()[i,j] % 4) % 2) == 1)
                        {
                            aspXY[0] = i;
                            aspXY[1] = j;
                            return aspXY;
                        }
                    }
                }
            }
            */

            throw new Exception("Did not find the vaccum");

        }

        public int[,] DEBUG_getDesire()
        {
            return floor.getInitialState();
        }

        public int getGridSize()
        {
            return 3;
        }

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
