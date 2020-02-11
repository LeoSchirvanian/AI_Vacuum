using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class Pickup : Action
    {
        
        // Constructor
        public Pickup()
        {

        }

        // Pick a jewel
        public void enact(Floor floor, int[] vacXY)
        {
            floor.pickup(vacXY);
        }
    }
}
