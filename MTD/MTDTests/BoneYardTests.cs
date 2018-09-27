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
        Domino d12;
        Domino d21;
        Domino d33;
        Domino d14;
        BoneYard dominos;
        
        [SetUp]
        public void SetUpAllTests()
        {
            dominos = new BoneYard();
            d12 = new Domino(1, 2);
            d21 = new Domino(2, 1);
            d33 = new Domino(3, 3);
            d14 = new Domino(1, 4);
            dominos.Add(d12);
            dominos.Add(d21);
            dominos.Add(d33);
        }
        /*
        [Test]
        public void TestConstructor()
        {
            
        }
        */
        [Test]
        public void TestDominosRemaining()
        {
            int answer = dominos.DominosRemaining;
            Assert.AreEqual(3, answer);
        }

        [Test]
        public void TestAdd()
        {
            dominos.Add(d14);
            Assert.AreEqual(4, dominos.DominosRemaining);
        }

        [Test]
        public void TestShuffle()
        {
            BoneYard shuffled = new BoneYard();
            shuffled.Add(d12);
            shuffled.Add(d21);
            shuffled.Add(d33);
            shuffled.Shuffle();
            Assert.AreNotEqual(dominos, shuffled);
        }
    }
}
