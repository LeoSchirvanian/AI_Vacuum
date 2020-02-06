using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Room
    {
        private bool hasDirt;
        private bool hasJewels;
        private bool hasVaccum;
        private int state = 0;

        public Room()
        {
            hasDirt = false;
            hasJewels = false;
            hasVaccum = false;
        }

        public int getState()
        {
            state = 0;
            /*
             * dirt | jewels | vaccum === > d|j|v
             
                djv
                --- = 0
                --v = 1
                -j- = 2
                -jv = 3
                d-- = 4
                d-v = 5
                dj- = 6 
                djv = 7

             */

            if(hasDirt)
            {
                state += 4;
            }
            if (hasJewels)
            {
                state += 2;
            }
            if (hasVaccum)
            {
                state += 1;
            }

            return state;
            
        }

        public void dirt()
        {
            hasDirt = true;
        }

        public void clean()
        {
            hasDirt = false;
        }

        public void jewels()
        {
            hasJewels = true;
        }

        public void pickup()
        {
            hasJewels = false;
        }

        public void vaccumin()
        {
            hasVaccum = true;
        }

        public void vaccumout()
        {
            hasVaccum = false;
        }
    }
}
