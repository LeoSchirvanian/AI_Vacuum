using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Room
    {
        // Attributs
        private bool hasDirt;
        private bool hasJewels;
        private bool hasVaccum;
        private int state = 0;

        // Constructor
        public Room()
        {
            hasDirt = false;
            hasJewels = false;
            hasVaccum = false;
        }

        // Return state number (4 : dirt, 2 : jewel, 1 : vaccum)
        public int getState()
        {
            state = 0;

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

        // There is dirt
        public void dirt()
        {
            hasDirt = true;
        }

        // There is no dirt
        public void clean()
        {
            hasDirt = false;
        }

        // There is jewel
        public void jewels()
        {
            hasJewels = true;
        }

        // There is no jewel
        public void pickup()
        {
            hasJewels = false;
        }

        // There is vaccum
        public void vaccumin()
        {
            hasVaccum = true;
        }

        // There is no vaccum
        public void vaccumout()
        {
            hasVaccum = false;
        }
    }
}
