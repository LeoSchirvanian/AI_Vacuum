using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class Pickup : Action
    {
        private int cost;

        public Pickup()
        {
            cost = 20;
        }

        public void enact(Floor floor, int[] vacXY)
        {
            floor.pickup(vacXY);
        }

        public void reverse(Floor floor, int[] vacXY)
        {
            floor.jewels(vacXY);
        }

        public int getCost()
        {
            return cost;
        }
    }
}
