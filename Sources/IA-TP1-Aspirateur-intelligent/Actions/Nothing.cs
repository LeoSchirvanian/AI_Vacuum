using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class Nothing : Action
    {
        private int cost;

        public Nothing()
        {
            cost = 2;

        }

        public void enact(Floor floor, int[] vacXY)
        {

        }

        public void reverse(Floor floor, int[] vacXY)
        {

        }

        public int getCost()
        {
            return cost;
        }

    }
}
