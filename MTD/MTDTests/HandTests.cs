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
    public class HandTests
    {
        BoneYard by;
        Hand def;
        Hand count1;
        Hand count2;
        Domino d11;
        Domino d22;
        MexicanTrain mt;

        [SetUp]
        public void SetUpAllTests()
        {
            by = new BoneYard(9);
            d11 = new Domino(1, 1);
            d22 = new Domino(2, 2);
            def = new Hand();
            count1 = new Hand();
            count2 = new Hand();
            mt = new MexicanTrain(1);
            count1.Add(d11);
            count2.Add(d11);
            count2.Add(d22);
            mt.Add(d11);
        }

        [Test]
        public void TestCount()
        {
            Assert.AreEqual(def.Count, 0);
        }

        [Test]
        public void TestOverloadedConstructor()
        {
            Hand p2 = new Hand(by, 2);
            Hand p5 = new Hand(by, 5);
            Hand p7 = new Hand(by, 7);
            Assert.AreEqual(p2.Count, 10);
            Assert.AreEqual(p5.Count, 9);
            Assert.AreEqual(p7.Count, 7);
        }

        [Test]
        public void TestAdd()
        {
            def.Add(new Domino(1, 1));
            Assert.AreEqual(def.Count, 1);
        }

        [Test]
        public void TestIsEmpty()
        {
            Assert.That(def.IsEmpty);
        }

        [Test] 
        public void TestScore()
        {
            Assert.AreEqual(count2.Score, 6);
        }

        [Test]
        public void TestHasDomino()
        {
            Assert.That(count1.HasDomino(1));
            Assert.False(count1.HasDomino(2));
        }

        [Test]
        public void TestHasDoubleDomino()
        {
            Assert.That(count1.HasDoubleDomino(1));
            Assert.False(count1.HasDoubleDomino(2));
        }

        [Test]
        public void TestIndexer()
        {
            Assert.AreEqual(count1[0].Side1, 1);
        }

        [Test]
        public void TestIndexOfDomino()
        {
            Assert.AreEqual(count1.IndexOfDomino(1), 0);
        }

        [Test]
        public void TestIndexOfDoubleDomino()
        {
            Assert.AreEqual(count1.IndexOfDomino(1), 0);
        }

        [Test]
        public void TestIndexOfHighDouble()
        {
            Assert.AreEqual(count2.IndexOfHighDouble(), 1);
        }

        [Test]
        public void TestRemoveAt()
        {
            count2.RemoveAt(1);
            Assert.AreEqual(count2.Count, 1);
        }

        [Test]
        public void TestGetDomino()
        {
            Domino d = count2.GetDomino(4);
            Assert.AreEqual(d, d22);
        }

        [Test]
        public void TestGetDoubleDomino()
        {
            Domino d = count2.GetDoubleDomino(2);
            Assert.AreEqual(d, d22);
        }

        [Test]
        public void TestDraw()
        {
            count2.Draw(by);
            Assert.AreEqual(count2.Count, 3);
        }

        [Test]
        public void TestPlay()
        {
            count1.Play(0, mt);
            Assert.AreEqual(mt.Count, 2);
        }

        [Test]
        public void TestPlayOverload1()
        {
            count1.Play(d11, mt);
            Assert.AreEqual(mt.Count, 2);
        }

        [Test]
        public void TestPlayOverload2()
        {
            count1.Play(mt);
            Assert.AreEqual(mt.Count, 2);
        }
    }
}
