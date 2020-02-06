using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Schmutzfabrik
    {
        private int[,] probability_matrix;

        private Random random = new Random();

        public Schmutzfabrik(int[,] probmatrix)
        {
            probability_matrix = probmatrix;
        }

        public void dirty(Floor floor)
        {
            int x = random.Next(floor.getState().GetLength(0));
            int y = random.Next(floor.getState().GetLength(1));

            if ( random.Next(101) <= probability_matrix[x,y] )
            {
                floor.dirt(new[] { x, y });
            }
        }

    }
}
