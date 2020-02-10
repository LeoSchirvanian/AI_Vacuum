using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    // Interface
    interface Action
    {
        // Methods in each Actions : MoveDown, MoveUp, MoveLeft, MoveRight
        void enact(Floor floor, int[] vacXY);
        void reverse(Floor floor, int[] vacXY);
        int getCost();
    }
}
