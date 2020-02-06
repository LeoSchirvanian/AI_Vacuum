using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Modelisation
{
    class Node
    {
        private int[,] state;
        private int[] vacXY;
        private int depth;
        private int pathcost;
        private bool visited;
        private string lastaction;
        private Node parent;
        private List<Node> childs;

        public Node(int[,] s, int[] aspXY, int d, int pc, bool vis, string la, Node p)
        {
            state = s;
            vacXY = aspXY;
            depth = d;
            pathcost = pc;
            visited = vis;
            lastaction = la;
            parent = p;
            childs = new List<Node>();
        }
        public Node(int[,] s, int[] aspXY, int d, int pc, bool vis, string la)
        {
            state = (int[,]) s.Clone();
            vacXY = (int[]) aspXY.Clone();
            depth = d;
            pathcost = pc;
            visited = vis;
            lastaction = la;
            childs = new List<Node>();
            parent = null;
        }

        public int[,] getState()
        {
            return (int[,])state.Clone();
        }

        public int getDepth()
        {
            return depth;
        }

        public int getPathcost()
        {
            return pathcost;
        }

        public int[] getVacXY()
        {
            return (int[])vacXY.Clone();
        }

        public void visit()
        {
            visited = true;
        }

        public void unvisit()
        {
            visited = false;
        }

        public bool isVisited()
        {
            return visited;
        }

        public Node getParent()
        {
            return parent;
        }

        public string getLastAction()
        {
            return lastaction;
        }

        public void addChild(Node child)
        {
            childs.Add(child);
        }

        public void removeChild(Node child)
        {
            childs.Remove(child);
        }
    }
}
