﻿using DailyCodingProblem.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyCodingProblem.Tests
{
    [TestFixture]
    public class ProblemTests
    {
        [TestCase(new[] {10, 15, 3, 7}, 17, true)]
        [TestCase(new[] {10, 15, 3, 7}, 18, true)]
        [TestCase(new[] {10, 15, 3, 7}, 1, false)]
        [TestCase(new[] {10, -15, 3}, -5, true)]
        public void DoesTwoNumbersAddUpTests(int[] array, int number, bool expected)
        {
            // Arrange

            // Act
            bool actual = Problems.DoesTwoNumbersAddUp(array, number);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(new[] {1, 2, 3, 4, 5}, new[] {120, 60, 40, 30, 24})]
        public void TransformUsingDivisionTests(int[] input, int[] expected)
        {
            // Arrange

            // Act
            int[] actual = Problems.TransformUsingDivision(input);

            // Assert
            CollectionAssert.AreEqual(actual, expected);
        }

        [TestCase(new[] {1, 2, 3, 4, 5}, 6)]
        [TestCase(new[] {-1, -2}, 1)]
        [TestCase(new[] {1, 3}, 2)]
        [TestCase(new[] {2, 3}, 1)]
        public void GetFirstMissingPositiveNumberTests(int[] input, int expected)
        {
            // Arrange

            // Act
            int actual = Problems.GetFirstMissingPositiveNumber(input);

            // Assert
            Assert.AreEqual(actual, expected);
        }

        [TestCase(new[] {13, 1, 61, 5, 9, 2}, 17, new[] {1, 5, 9, 2})]
        public void GetSubsetAddingToNumberTests(int[] input, int requiredSum, int[] expected)
        {
            // Arrange

            // Act
            int[] actual = Problems.GetSubsetAddingToNumber(input, requiredSum);

            // Assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestCase("1,2,3,4,5,6", "6,5,4,3,2,1")]
        public void RearrangeTests(string inputLists, string expectedList)
        {
            // Arrange
            SllNode head = ParseToNodes(inputLists)[0];

            // Act
            Problems.Rearrange(head);

            // Assert
            string mergedListString = GetStringEquivalent(head);
            Assert.AreEqual(expectedList, mergedListString);
        }

        [TestCase("1,3,5#4,6#2,3", "1,2,3,3,4,5,6")]
        [TestCase("1,3,5#2", "1,2,3,5")]
        [TestCase("4,6#1,3,5#2,3", "1,2,3,3,4,5,6")]
        public void GetMergedListTests(string inputLists, string expectedList)
        {
            // Arrange
            IList<SllNode> inputs = ParseToNodes(inputLists);

            // Act
            var mergedListHead = Problems.GetMergedList(inputs);

            // Assert
            string mergedListString = GetStringEquivalent(mergedListHead);
            Assert.AreEqual(expectedList, mergedListString);
        }

        private static IList<SllNode> ParseToNodes(string inputString)
        {
            var pairs = inputString.Split('#');
            IList<SllNode> inputs = new List<SllNode>(pairs.Length);
            foreach (var pair in pairs)
            {
                SllNode headNode = new SllNode();
                inputs.Add(headNode);
                SllNode current = headNode;
                var items = pair.Split(',');
                foreach (var item in items)
                {
                    SllNode itemNode = new SllNode();
                    itemNode.Val = int.Parse(item);
                    current.Next = itemNode;
                    current = itemNode;
                }
            }

            return inputs;
        }

        private string GetStringEquivalent(SllNode mergedListHead)
        {
            StringBuilder sb = new StringBuilder();
            var current = mergedListHead;
            while (current.Next != null)
            {
                current = current.Next;
                sb.AppendFormat("{0},", current.Val);
            }

            return sb.ToString().TrimEnd(',');
        }

        [TestCase("1,3#5,8#4,10#20,25", "1,3#4,10#20,25")]
        [TestCase("1,3#4,10#5,8#20,25", "1,3#4,10#20,25")]
        [TestCase("1,3#4,10#11,14#20,25", "1,3#4,10#11,14#20,25")]
        [TestCase("1,3#4,10#11,14#1,25", "1,25")]
        [TestCase("1,3#5,8#4,10#4,25", "1,3#4,25")]
        public void GetMergedNodesTests(string inputPairs, string expectedPairs)
        {
            // Arrange
            IList<RangeNode> inputs = ParseToRangeNodes(inputPairs);
            IList<RangeNode> outputs = ParseToRangeNodes(expectedPairs);

            // Act
            var mergedList = Problems.GetMergedNodes(inputs);

            // Assert
            CollectionAssert.AreEquivalent(outputs, mergedList);
        }

        private static IList<RangeNode> ParseToRangeNodes(string inputString)
        {
            var pairs = inputString.Split('#');
            IList<RangeNode> inputs = new List<RangeNode>(pairs.Length);
            foreach (var pair in pairs)
            {
                var items = pair.Split(',');
                inputs.Add(new RangeNode(int.Parse(items[0]), int.Parse(items[1])));
            }

            return inputs;
        }

        [Test]
        public void GetDeepestNodeTests()
        {
            // Arrange
            var rootNode = CreateTree();
            // Act
            var deepestNode = Problems.GetDeepestNode(rootNode, out int depth);

            // Assert
            Assert.That(deepestNode.Val, Is.EqualTo(4));
            Assert.That(depth, Is.EqualTo(3));
        }

        private BTreeNode CreateTree()
        {
            /*
            1
           / \
          2   3
         /
        4
             */
            BTreeNode root = new BTreeNode { Val = 1};
            root.Left = new BTreeNode { Val = 2};
            root.Right = new BTreeNode { Val = 3 };
            root.Left.Left = new BTreeNode { Val = 4 };
            return root;
        }
    }
}
