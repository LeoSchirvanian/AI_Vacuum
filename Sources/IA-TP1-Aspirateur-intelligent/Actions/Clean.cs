using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Actions
{
    class Clean : Action
    {

        // Constructor
        public Clean()
        {

        }

        // Clean the floor given the position of the vaccum
        public void enact(Floor floor, int[] vacXY)
        {
            floor.clean(vacXY);
        }

    }
}
