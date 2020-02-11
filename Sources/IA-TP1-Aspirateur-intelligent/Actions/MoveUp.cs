using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class MoveUp : Action
    {
        
        // Constructor
        public MoveUp()
        {

        }

        // Move the vaccum up
        public void enact(Floor floor, int[] vacXY)
        {
            floor.vaccumout((int[])vacXY.Clone()); // Vaccum leave its initial state
            int[] ncoo = vacXY;
            if (!(vacXY[0] == 0)) // If not the top of the floor
            {
                ncoo[0] -= 1;
            }

            // Put the vaccum
            floor.vaccumin(ncoo);
        }

    }
}
