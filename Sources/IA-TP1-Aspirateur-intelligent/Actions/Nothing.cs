using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class Nothing : Action
    {
        // Attributs
        private int cost;
        
        // Constructor
        public Nothing()
        {
            cost = 2;

        }

        // Do nothing
        public void enact(Floor floor, int[] vacXY)
        {

        }

        // Do everything ... or not
        public void reverse(Floor floor, int[] vacXY)
        {

        }
        
        // Give the cost of the action
        public int getCost()
        {
            return cost;
        }

    }
}
