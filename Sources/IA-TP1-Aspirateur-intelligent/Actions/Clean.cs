using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class Clean : Action
    {
        // Attributs
        private int cost;

        // Constructor
        public Clean()
        {
            cost = 20;
        }

        // Clean the floor given the position of the vaccum
        public void enact(Floor floor, int[] vacXY)
        {
            floor.clean(vacXY);
        }

        // Put dirt on the floor
        public void reverse(Floor floor, int[] vacXY)
        {
            floor.dirt(vacXY);
        }
        
        // Give the cost of the action
        public int getCost()
        {
            return cost;
        }
    }
}
