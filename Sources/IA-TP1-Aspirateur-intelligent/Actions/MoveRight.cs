using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class MoveRight : Action
    {
        
        // Constructor
        public MoveRight()
        {

        }
        
        // Move the vaccum right
        public void enact(Floor floor, int[] vacXY)
        {
            floor.vaccumout((int[])vacXY.Clone()); // Vaccum leave its initial state
            int[] ncoo = vacXY;
            if (!(vacXY[1] == floor.getState().GetLength(1)-1)) // If not the extrem right of the floor
            {
                ncoo[1] += 1;
            }

            // Put the vaccum
            floor.vaccumin(ncoo);
        }
        
    }
}
