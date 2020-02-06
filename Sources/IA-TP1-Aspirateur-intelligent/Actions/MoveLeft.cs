using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class MoveLeft : Action
    {
        private int cost;

        public MoveLeft()
        {
            cost = 5;
        }

        public void enact(Floor floor, int[] vacXY)
        {
            floor.vaccumout((int[])vacXY.Clone());
            int[] ncoo = vacXY;
            if (!(vacXY[1] == 0))
            {
                ncoo[1] -= 1;
            }
            
            floor.vaccumin(ncoo);
        }

        public void reverse(Floor floor, int[] vacXY)
        {
            floor.vaccumout((int[])vacXY.Clone());
            int[] ncoo = vacXY;
            if (!(vacXY[1] == floor.getState().GetLength(1)-1))
            {
                ncoo[1] += 1;
            }
            
            floor.vaccumin(ncoo);
        }

        public int getCost()
        {
            return cost;
        }
    }
}
