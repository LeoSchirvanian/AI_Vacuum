using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP1_Aspirateur_intelligent.Modelisation
{
    class Node
    {
        // Attributs
        private int[,] state;
        private int[] vacXY;
        private int depth;
        private int pathcost;
        private bool visited;
        private string lastaction;
        private Node parent;
        private List<Node> childs;

        // Constructor with parent
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

        // Constructor without parent
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

        // Get state
        public int[,] getState()
        {
            return (int[,])state.Clone();
        }

        // Get depth
        public int getDepth()
        {
            return depth;
        }

        // Get Pathcost
        public int getPathcost()
        {
            return pathcost;
        }

        // Get vacXY
        public int[] getVacXY()
        {
            return (int[])vacXY.Clone();
        }

        // Visited : true
        public void visit()
        {
            visited = true;
        }

        // Visited : false
        public void unvisit()
        {
            visited = false;
        }

        // Get visited boolean
        public bool isVisited()
        {
            return visited;
        }

        // Get parent
        public Node getParent()
        {
            return parent;
        }

        // Get lastAction
        public string getLastAction()
        {
            return lastaction;
        }

        // Add a child node
        public void addChild(Node child)
        {
            childs.Add(child);
        }

        // Remove a child
        public void removeChild(Node child)
        {
            childs.Remove(child);
        }
    }
}
