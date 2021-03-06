﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent
{
    class Sensor
    {
        
        // Constructor
        public Sensor()
        {
            
        }

        // Get Floor state
        public int[,] getSurroundings()
        {
            return (int[,]) Manor.getInstance().getFloorState().Clone();
        }

        // Get vacXY
        public int[] getVacXY()
        {
            return (int[]) Manor.getInstance().getAspXY().Clone();
        }
    }
}
