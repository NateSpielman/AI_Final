using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeStatus
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class Node 
    {
        //Only itself and derived classes can set this status
        protected NodeStatus status;

        public Node parent;
        //Children get assigned by the constructor or are empty
        protected List<Node> children = new List<Node>();
        protected Node onlyChild;

        private Node root;
        protected Node GetRoot => root;
        public bool HasChildren => children.Count > 0;

        public Node()
        {
            parent = null;
            root = this;
        }

        public Node(List<Node> children)
        {
            SetChildren(children);
        }

        public Node(Node node)
        {
            node.parent = this;
            onlyChild = node;
        }

        public virtual NodeStatus Evaluate() => NodeStatus.FAILURE;

        public void SetChildren(List<Node> children)
        {
            foreach (Node child in children)
            {
                AddChild(child);
            }
        }

        private void AddChild(Node node)
        {
            children.Add(node);
            node.parent = this;
        }

        public void SetRoot(Node root)
        {
            this.root = root;

            foreach (Node child in children)
                child.SetRoot(root);
        }
    }
}
