using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stos;
using System;

namespace UnitTestProjectStos
{
    [TestClass]
    public class UnitTestStosChar
    {
        private IStos<char> stack;
        private Random rnd = new Random();
        private char RandomChar => (char)rnd.Next(33, 126);

        
        [TestMethod]
        public void StackEmpty()
        {
            stack = new StosWTablicy<char>();
            Assert.IsTrue(stack.IsEmpty);
        }

        
        [TestMethod]
        public void StackNotEmpty()
        {
            stack = new StosWTablicy<char>();
            stack.Push(RandomChar);
            Assert.IsTrue(!stack.IsEmpty);
        }

        
        [TestMethod]
        public void StackUpdatingToTheSameState()
        {
            stack = new StosWTablicy<char>();
            stack.Push(RandomChar);
            stack.Push(RandomChar);

            char[] tab1 = stack.ToArray();
            char e = RandomChar;
            stack.Push(e);
            stack.Pop();
            char[] tab2 = stack.ToArray();

            CollectionAssert.AreEqual(tab1, tab2);
        }

        
        [TestMethod]
        public void StackPeek()
        {
            stack = new StosWTablicy<char>();
            char e = RandomChar;

            stack.Push(e);

            Assert.AreEqual(stack.Peek, e);
        }

        
        [TestMethod]
        [ExpectedException(typeof(StosEmptyException))]
        public void StackEmptyPeekException()
        {
            stack = new StosWTablicy<char>();
            Assert.IsTrue(stack.IsEmpty);

            char c = stack.Peek;
        }

        
        [TestMethod]
        [ExpectedException(typeof(StosEmptyException))]
        public void StackEmptyPop()
        {
            stack = new StosWTablicy<char>();
            Assert.IsTrue(stack.IsEmpty);

            char c = stack.Pop();
        }

        [DataTestMethod]      
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(20)]
        [DataRow(50)]
        [DataRow(100)]
        [DataRow(200)]
        [DataRow(500)]
        [DataRow(1000)]
        public void StackTrimExcess(int index)
        {
            int expectedLength;
            if (index <= 5)
                expectedLength = index + 1;
            else
                expectedLength = index + Convert.ToInt32((1d / 10d) * index);
            var s = new StosWTablicy<int>();
            for (int i = 0; i < index; i++)
                s.Push(1);
            s.TrimExcess();
            Assert.AreEqual(expectedLength, s.Length());
        }
    }

}

