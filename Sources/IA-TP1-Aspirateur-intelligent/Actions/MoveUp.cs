using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class MoveUp : Action
    {
        // Attributs
        private int cost;
        
        // Constructor
        public MoveUp()
        {
            cost = 5;
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

        // Move the vaccum down
        public void reverse(Floor floor, int[] vacXY)
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
        
        // Give the cost of the action
        public int getCost()
        {
            return cost;
        }
    }
}
