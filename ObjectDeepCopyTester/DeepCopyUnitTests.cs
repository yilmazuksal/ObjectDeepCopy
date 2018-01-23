using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectDeepCopy;

namespace ObjectDeepCopyTester
{
    [TestClass]
    public class DeepCopyUnitTests
    {
        [TestMethod]
        public void CircularReferenceTest()
        {
            Node n1 = new Node();
            n1.no = 1;
            n1.name = "node 1";
            Node n2 = new Node();
            n2.no = 2;
            n1.name = "node 2";

            n1.next = n2;
            n2.next = n1;

            Node deepCopy = (Node)ObjectDeepCopier.Copy(n1);

            Assert.AreEqual(n1.name, deepCopy.name);
            Assert.AreEqual(n1.no, deepCopy.no);
            Assert.AreNotSame(n1, deepCopy);
            Assert.AreEqual(n1.next.name, deepCopy.next.name);
            Assert.AreEqual(n1.next.no, deepCopy.next.no);
            Assert.AreNotSame(n1.next, deepCopy.next);
        }

        [TestMethod]
        public void NonCircularReferenceTest()
        {
            Node n1 = new Node();
            n1.no = 1;
            n1.name = "node 1";
            Node n2 = new Node();
            n2.no = 2;
            n1.name = "node 2";

            n1.next = n2;
            n2.next = null;

            Node deepCopy = (Node)ObjectDeepCopier.Copy(n1);

            Assert.AreEqual(n1.name, deepCopy.name);
            Assert.AreEqual(n1.no, deepCopy.no);
            Assert.AreNotSame(n1, deepCopy);
            Assert.AreEqual(n1.next.name, deepCopy.next.name);
            Assert.AreEqual(n1.next.no, deepCopy.next.no);
            Assert.AreNotSame(n1.next, deepCopy.next);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),"Object to deep copy cannot be null")]
        public void NullObjectDeepCopyTest()
        {
            ObjectDeepCopier.Copy(null);
        }


    }
}
