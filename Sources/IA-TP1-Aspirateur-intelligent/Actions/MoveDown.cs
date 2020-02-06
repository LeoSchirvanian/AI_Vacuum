using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class MoveDown : Action
    {
        private int cost;

        public MoveDown()
        {
            cost = 5;
        }

        public void enact(Floor floor, int[] vacXY)
        {
            floor.vaccumout((int[])vacXY.Clone());
            int[] ncoo = vacXY;
            if (!(vacXY[0] == floor.getState().GetLength(0)-1)) 
            { 
                ncoo[0] += 1; 
            }
            
            floor.vaccumin(ncoo);
        }

        public void reverse(Floor floor, int[] vacXY)
        {
            floor.vaccumout((int[])vacXY.Clone());
            int[] ncoo = vacXY;
            if (!(vacXY[0] == 0))
            {
                ncoo[0] -= 1;
            }
            
            floor.vaccumin(ncoo);
        }

        public int getCost()
        {
            return cost;
        }
    }
}
