using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    public class Floor
    {
        // Attributs
        private int[,] state;
        private int[,] initialState;
        //private List<int[,]> desireStates;

        // Constructor by size
        public Floor(int size)
        {
            state = new int[size, size];
            initialState = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    state[i, j] = 0;
                }

            }
            state[0, 0] = 1;
            initialState = (int[,])state.Clone();
        }

        // Constructor by table
        public Floor(int[,] s)
        {
            state = (int[,])s.Clone();
            initialState = (int[,])s.Clone();
        }

        // Return state
        public int[,] getState()
        {
            return state;
        }

        // Return initialState
        public int[,] getInitialState()
        {
            return initialState;
        }

        // Allow to know where is the vaccum
        public int[] getAspXY()
        {
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (((state[i, j] % 4) % 2) == 1)
                    {
                        int[] aspXY = new int[2];
                        aspXY[0] = i;
                        aspXY[1] = j;
                        return aspXY;
                    }
                }
            }
        
            throw new Exception("Did not find the vaccum");
        }

        // Return a list of coordinates and value of dirt/jewel object on the floor
        public List<(int, int, int)> getJewelDirt()
        {
            List<(int, int, int)> cooList = new List<(int, int, int)>();
            int gridsize = 5;

            for (int i = 0; i < gridsize; i++)
            {
                for (int j = 0; j < gridsize; j++)
                {
                    // If not clean or not vaccum alone, add localisation into the list
                    if (state[i, j] > 1)
                    {
                        cooList.Add((i, j, state[i, j]));
                    }
                }
            }
            
            return cooList;
        }

        // Calculate the heuristic number
        // Heuristic is equivalent to Manhattan distance, we first calculate the manhattan distance between a dirt/jewel and the vaccum and multiply it by a weight
        // This weight correspond to the value of the object detected by the vaccum (2, 4 or 6) returned by getJewelDirt method.
        // Hence the vaccum is more focused on big object (dirt + jewel > dirt > jewel)
        public int heuristique(List<(int, int, int)> cooList)
        {
            int h = 0;

            int[] vacXY = getAspXY();
            int x = vacXY[0];
            int y = vacXY[1];


            for (int i = 0; i < cooList.Count; i++)
            {
                // Definition of heuristic 
                h += ( Math.Abs(x - cooList[i].Item1) + Math.Abs(y - cooList[i].Item2) ) * cooList[i].Item3;
            }
            return h;

        }

        // Allow to know if the vaccum is on a cell with dirt or/and jewel
        public int isJewelDirt()
        {
            int[] vacXY = getAspXY();
            int x = vacXY[0];
            int y = vacXY[1];

            switch(state[x, y])
            {
                case 3:
                    return 3;
                    break;

                case 5:
                    return 5;
                    break;

                case 7:
                    return 7;
                    break;

                default:
                    return 0;
                    break;
            }

            /*
            if(state[x,y] > 1)
            {
                return true;
            }
            return false;
            */
        }

        // Add dirt on the floor, dirt => x % 4 == 0 or x > 4
        public void dirt(int[] coo)
        {
            if ( (state[coo[0], coo[1]] % 4) == state[coo[0], coo[1]] )
            {
                state[coo[0], coo[1]] += 4;
            }
            
        }

        // Clean a dirt by dividing by 4
        public void clean(int[] coo)
        {
            state[coo[0], coo[1]] %= 4;
        }

        // Add jewels on the floor
        public void jewels(int[] coo)
        {
            int pstate = state[coo[0], coo[1]];

            // If there is no dirt and no jewels
            if ( pstate % 4 == pstate)
            {
                if (pstate % 2 == pstate )
                {
                    state[coo[0], coo[1]] += 2; //Add jewels
                }
            }
            // If there is no jewels
            else
            {
                pstate %= 4;
                if (pstate % 2 == pstate)
                {
                    state[coo[0], coo[1]] += 2; // Add jewels
                }
            }

        }

        // Pick a jewel on the floor
        public void pickup(int[] coo)
        {
            int pstate = state[coo[0], coo[1]];
            
            // If there is no dirt
            if ( pstate % 4 == pstate )
            {
                state[coo[0], coo[1]] %= 2; // Remove the jewel
            }
            // If there is dirt
            else
            {
                pstate %= 4; //Remove dirt
                pstate %= 2; //Remove jewel
                pstate += 4; //Add dirt
                state[coo[0], coo[1]] = pstate;
            }

        }

        // Add vaccum in a cell of the floor
        public void vaccumin(int[] coo)
        {
            //Console.WriteLine("Moved in of :" + coo[0] + ", " + coo[1]);
            int pstate = state[coo[0], coo[1]];

            // If there is no dirt
            if (pstate % 4 == pstate)
            {
                // If there is no jewel
                if (pstate % 2 == pstate)
                {
                    // If there is no vaccum
                    if ( pstate % 1 == pstate )
                    {
                        state[coo[0], coo[1]] += 1; // Add 1
                    }
                }
                // If there is jewel
                else
                {
                    pstate %= 2;
                    // If there is no vaccum
                    if (pstate % 1 == pstate)
                    {
                        state[coo[0], coo[1]] += 1; // Add 1
                    }
                }
            }
            // If there is dirt
            else
            {
                pstate %= 4;
                // If there is no jewel
                if (pstate % 2 == pstate)
                {
                    // If there is no vaccum
                    if (pstate % 1 == pstate)
                    {
                        state[coo[0], coo[1]] += 1; // Add 1
                    }
                }
                // If there is jewel
                else
                {
                    pstate %= 2;
                    // If there is no vaccum
                    if (pstate % 1 == pstate)
                    {
                        state[coo[0], coo[1]] += 1; // Add 1
                    }
                }
            }
        }

        // Remove vaccum in a cell of the floor
        public void vaccumout(int[] coo)
        {
            //Console.WriteLine("Moved out of :" + coo[0] + ", " + coo[1]);
            int pstate = state[coo[0], coo[1]];

            // If there is no dirt
            if (pstate % 4 == pstate)
            {
                // If there is no jewel
                if (pstate % 2 == pstate)
                {
                    state[coo[0], coo[1]] %= 1; // Remove vaccum
                }
                else
                {
                    state[coo[0], coo[1]] %= 2; // Remove jewel
                    state[coo[0], coo[1]] %= 1; // Remove vaccum
                    state[coo[0], coo[1]] += 2; // Add jewel
                }
            }
            // If there is dirt
            else
            {
                pstate %= 4;
                state[coo[0], coo[1]] %= 4; // Remove dirt
                // If there is no jewel
                if ( pstate % 2 == pstate)
                {
                    state[coo[0], coo[1]] %= 1; // Remove vaccum
                }
                // If there is jewel
                else
                {
                    state[coo[0], coo[1]] %= 2; // Remove jewel
                    state[coo[0], coo[1]] %= 1; // Remove vaccum
                    state[coo[0], coo[1]] += 2; // Add jewel
                }
                state[coo[0], coo[1]] += 4; // Add dirt

            }

        }

        // Reset state of the floor based on initialState
        public void reset()
        {
            this.state = (int[,])initialState.Clone();
        }

    }
}
