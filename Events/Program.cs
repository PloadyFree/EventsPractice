﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tree = new BinaryTree<int>();
            var treeVales = new[]
   {
                8,
                4, 12,
                2, 6, 10, 14,
                1, 3, 5, 7, 9, 11, 13, 15
            };
            foreach (var value in treeVales)
                tree.Add(value);
            foreach (var value in tree)
                Console.WriteLine("Node number {0}", value);
        }
    }
}
