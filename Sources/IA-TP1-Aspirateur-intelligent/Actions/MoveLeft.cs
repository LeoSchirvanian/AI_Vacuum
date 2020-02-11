using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class MoveLeft : Action
    {
        
        // Constructor
        public MoveLeft()
        {

        }

        // Move the vaccum left
        public void enact(Floor floor, int[] vacXY)
        {
            floor.vaccumout((int[])vacXY.Clone()); // Vaccum leave its initial state
            int[] ncoo = vacXY;
            if (!(vacXY[1] == 0)) // If not the extrem left of the floor
            {
                ncoo[1] -= 1;
            }

            // Put the vaccum
            floor.vaccumin(ncoo);
        }

    }
}
