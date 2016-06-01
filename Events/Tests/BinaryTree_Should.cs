using System;
using System.Collections.Generic;
using System.Linq;
using Events.Implementations;
using FluentAssertions;
using NUnit.Framework;

namespace Events.Tests
{
    [TestFixture]
    public class BinaryTree_Should : TestBase
    {
        private BinaryTree<int> tree;
        
        [SetUp]
        public void SetUp()
        {
            tree = new BinaryTree<int>();
        }

        #region Main tests

        [Test]
        public void AddOneItemCorrectly()
        {
            tree.Add(123).Should().BeTrue();
        }

        [Test]
        public void AddSomeItemsCorrectly()
        {
            tree.Add(123).Should().BeTrue();
            tree.Add(42).Should().BeTrue();
        }

        [Test]
        public void AddManyItemsCorrectly()
        {
            var values = Enumerable.Range(0, 1000).ToList();
            foreach (var value in values)
                tree.Add(value).Should().BeTrue();
            foreach (var value in values)
                tree.Contains(value).Should().BeTrue();
        }

        [Test]
        public void NotAddItemsTwiceOrMore()
        {
            tree.Add(42).Should().BeTrue();
            tree.Add(42).Should().BeFalse();
            tree.Add(42).Should().BeFalse();
        }

        [Test]
        public void ContainElements_AfterAdding()
        {
            var num1 = 123;
            var num2 = 45;

            tree.Add(num1);
            tree.Add(num2);

            tree.Contains(num1).Should().BeTrue();
            tree.Contains(num2).Should().BeTrue();
        }

        [Test]
        public void NotContainUnexistingElements()
        {
            tree.Contains(555).Should().BeFalse();
        }

        [Test]
        public void FailOnAddingElement_WhenElementIsNull()
        {
            var tree = new BinaryTree<object>();
            Action adding = () => tree.Add(null);
            adding.ShouldThrow<Exception>();
        }

        #endregion
        
        #region By index accessing tests

        [Test]
        public void FailOnAcessByIndex_WhenIndexIsNegative()
        {
            foreach (var x in GetRandomDistinctInts(100))
                tree.Add(x);
            Action access = () => { var t = tree[rnd.Next(int.MinValue, 0)]; };
            access.ShouldThrow<Exception>();
        }

        [Test]
        public void FailOnAccessByIndex_WhenIndexOutOfRange()
        {
            foreach (var x in GetRandomDistinctInts(100))
                tree.Add(x);
            Action access = () => { var t = tree[rnd.Next(100, int.MaxValue)]; };
            access.ShouldThrow<Exception>();
        }

        [Test]
        public void ReturnElementsByIndexCorrectly()
        {
            BuildSampleTree(tree);
            var sortedValues = SampleTreeValues.OrderBy(x => x).ToList();
            foreach (var index in Enumerable.Range(0, sortedValues.Count))
                tree[index].Should().Be(sortedValues[index]);
        }

        #endregion

        #region Performance tests

        [Test, Timeout(10000), TestCaseSource(nameof(PerfrormanceTestCases))]
        public void WorkFast_WithManyRandomElements(IList<int> elements)
        {
            foreach (var element in elements)
                tree.Add(element);
            foreach (var element in elements)
                tree.Contains(element).Should().BeTrue();
        }

        [Test, Timeout(1000)]
        public void WorkFast_WithAscendingSequence()
        {
//            var tree = new AVLTree<int>();
            var elements = Enumerable.Range(0, (int) 1e4).ToList();
            foreach (var element in elements)
                tree.Add(element);
            foreach (var element in elements)
                tree.Contains(element).Should().BeTrue();
        }

        private IEnumerable<TestCaseData> PerfrormanceTestCases
        {
            get
            {
                yield return new TestCaseData(GetRandomDistinctInts((int)1e5).ToList());
                yield return new TestCaseData(GetRandomDistinctInts((int)2e5).ToList());
                yield return new TestCaseData(GetRandomDistinctInts((int)4e5).ToList());
            }
        }

        #endregion
    }
}
