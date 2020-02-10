using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class Pickup : Action
    {
        // Attributs
        private int cost;
        
        // Constructor
        public Pickup()
        {
            cost = 20;
        }

        // Pick a jewel
        public void enact(Floor floor, int[] vacXY)
        {
            floor.pickup(vacXY);
        }

        // Put a jewel on the floor
        public void reverse(Floor floor, int[] vacXY)
        {
            floor.jewels(vacXY);
        }

        // Give the cost of the action
        public int getCost()
        {
            return cost;
        }
    }
}
