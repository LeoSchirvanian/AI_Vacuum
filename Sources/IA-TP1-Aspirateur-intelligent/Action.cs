using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    interface Action
    {
        void enact(Floor floor, int[] vacXY);
        void reverse(Floor floor, int[] vacXY);

        int getCost();
    }
}
