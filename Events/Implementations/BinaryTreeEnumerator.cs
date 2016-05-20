﻿using System.Collections;
using System.Collections.Generic;
using Events.Interfaces;

namespace Events.Implementations
{
    internal class BinaryTreeEnumerator<T> : IEnumerator<T>
    {
        private readonly IBinaryTree<T> tree;
        private IBinaryTreeNode<T> lastNode;
        private bool finished;
        private readonly ISet<IBinaryTreeNode<T>> visited;
        private readonly Stack<IBinaryTreeNode<T>> path;

        public BinaryTreeEnumerator(IBinaryTree<T> tree)
        {
            this.tree = tree;
            visited = new HashSet<IBinaryTreeNode<T>>();
            path = new Stack<IBinaryTreeNode<T>>();
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (finished)
                return false;

            if (lastNode == null)
            {
                lastNode = tree.Root;
                return GoToMinNodeAndSaveIt();
            }

            if (lastNode.Right != null && !visited.Contains(lastNode.Right))
            {
                lastNode = lastNode.Right;
                return GoToMinNodeAndSaveIt();
            }

            GoToFirstUnusedParent();
            if (lastNode != null)
                return SaveCurrentNode();

            finished = true;
            return false;
        }

        private bool SaveCurrentNode()
        {
            visited.Add(lastNode);
            Current = lastNode.Value;
            return true;
        }

        private bool GoToMinNodeAndSaveIt()
        {
            GoToMinNode();
            return SaveCurrentNode();
        }

        private void GoToMinNode()
        {
            path.Push(lastNode);
            while (lastNode.Left != null)
                path.Push(lastNode = lastNode.Left);
        }

        private void GoToFirstUnusedParent()
        {
            while (visited.Contains(lastNode) && path.Count > 1)
            {
                path.Pop();
                lastNode = path.Peek();
            }

            //  We enumerated all nodes in tree
            if (visited.Contains(lastNode))
                lastNode = null;
        }

        public void Reset()
        {
            lastNode = null;
            finished = false;
            visited.Clear();
        }

        object IEnumerator.Current => Current;

        public T Current { get; private set; }
    }
}
