using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using MTDClasses;

namespace MTDTests
{
    [TestFixture]
    public class BoneYardTest
    {
        BoneYard b0;
        BoneYard b1;
        List<Domino> dList = new List<Domino>();
        
        [SetUp]
        public void SetUpAllTests()
        {
            b0 = new BoneYard(0);
            b1 = new BoneYard(1);
            dList.Add(new Domino(0, 0));
            dList.Add(new Domino(0, 1));
            dList.Add(new Domino(1, 1));
        }
       
        [Test]
        public void TestConstructor()
        {
            /*
            for (int i = 0; i <= dList.Count; i++)
            {
                Assert.AreEqual(dList[i].Side1, b1[i].Side1);
                Assert.AreEqual(dList[i].Side2, b1[i].Side2);
            }
            */
            Assert.AreEqual(dList[0].Side1, b1[0].Side1);
            Assert.AreEqual(dList[0].Side2, b1[0].Side2);
            Assert.AreEqual(dList[1].Side1, b1[1].Side1);
            Assert.AreEqual(dList[1].Side2, b1[1].Side2);
            Assert.AreEqual(dList[2].Side1, b1[2].Side1);
            Assert.AreEqual(dList[2].Side2, b1[2].Side2);


        }

        [Test]
        public void TestIndexer()
        {
            Assert.AreEqual(dList[2].Side1, b1[2].Side1);
            Assert.AreEqual(dList[2].Side2, b1[2].Side2);
        }
        
        [Test]
        public void TestDominosRemaining()
        {
            int answer = b1.DominosRemaining;
            Assert.AreEqual(3, answer);
        }
        
        [Test]
        public void TestAdd()
        {
            b1.Add(new Domino (1, 2));
            Assert.AreEqual(4, b1.DominosRemaining);
            Assert.AreEqual(2, b1[3].Side2);
        }
        
        [Test]
        public void TestShuffle()
        {
            b1.Shuffle();
            Assert.AreNotEqual(b1, dList);
        }
        
        [Test]
        public void TestIsEmpty()
        {
            Assert.That(b0.IsEmpty);
        }
        
        [Test]
        public void TestDraw()
        {
            b0.Add(b1.Draw());
            Assert.AreEqual(2, b1.DominosRemaining);

            b0.Add(b1.Draw());
            b0.Add(b1.Draw());
            Assert.AreNotEqual(b0, b1);
        }
    }
}
