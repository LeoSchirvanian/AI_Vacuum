using System;
using System.Threading;

namespace IA_TP1_Aspirateur_intelligent
{
    class Program
    {
        static void Main(string[] args)
        {
            Manor manor = Manor.getInstance();
            manor.setAlive();
        }
    }
}
