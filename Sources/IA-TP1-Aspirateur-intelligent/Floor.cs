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
