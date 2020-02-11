using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class MoveDown : Action
    {
        
        // Constructor
        public MoveDown()
        {

        }

        // Move the vaccum down
        public void enact(Floor floor, int[] vacXY)
        {
            floor.vaccumout((int[])vacXY.Clone()); // Vaccum leave its initial state
            int[] ncoo = vacXY;
            if (!(vacXY[0] == floor.getState().GetLength(0)-1)) // If not the bottom of the floor
            { 
                ncoo[0] += 1; 
            }

            // Put the vaccum
            floor.vaccumin(ncoo); 
        }

    }
}
