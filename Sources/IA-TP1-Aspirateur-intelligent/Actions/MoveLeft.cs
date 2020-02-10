using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class MoveLeft : Action
    {
        // Attributs
        private int cost;
        
        // Constructor
        public MoveLeft()
        {
            cost = 5;
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

        // Move the vaccum right
        public void reverse(Floor floor, int[] vacXY)
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
        
        // Give the cost of the action
        public int getCost()
        {
            return cost;
        }
    }
}
