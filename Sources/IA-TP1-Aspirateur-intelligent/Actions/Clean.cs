using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class Clean : Action
    {
        private int cost;

        public Clean()
        {
            cost = 20;
        }

        public void enact(Floor floor, int[] vacXY)
        {
            floor.clean(vacXY);
        }

        public void reverse(Floor floor, int[] vacXY)
        {
            floor.dirt(vacXY);
        }

        public int getCost()
        {
            return cost;
        }
    }
}
