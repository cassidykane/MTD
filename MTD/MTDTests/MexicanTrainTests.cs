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
    class MexicanTrainTests
    {
        MexicanTrain mtEmpty;
        MexicanTrain mt;
        Hand h;
        Domino d01;
        Domino d11;

        [SetUp]
        public void SetUpAllTests()
        {
            mtEmpty = new MexicanTrain(0);
            mt = new MexicanTrain(0);
            h = new Hand();
            d01 = new Domino(0, 1);
            d11 = new Domino(1, 1);

            mt.Add(d01);
            h.Add(d11);
        }

        [Test]
        public void TestCount()
        {
            Assert.AreEqual(mtEmpty.Count, 0);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(mtEmpty.Count, 0);
            Assert.AreEqual(mtEmpty.EngineValue, 0);
        }

        [Test]
        public void TestEngineValue()
        {
            Assert.AreEqual(mt.EngineValue, 0);
        }

        [Test]
        public void TestIsEmpty()
        {
            Assert.True(mtEmpty.IsEmpty);
            Assert.False(mt.IsEmpty);
        }

        [Test]
        public void TestAdd()
        {
            mt.Add(d11);
            Assert.AreEqual(mt.Count, 2);
        }

        [Test] 
        public void TestIndexer()
        {
            Assert.AreEqual(mt[0], d01);
        }

        [Test]
        public void TestLastDomino()
        {
            mt.Add(d11);
            Assert.AreEqual(mt.LastDomino, d11);
        }

        [Test]
        public void TestLastDominoEmptyTrain()
        {
            Assert.IsNull(mtEmpty.LastDomino);
        }

        [Test]
        public void TestPlayableValue()
        {
            Assert.AreEqual(mt.PlayableValue, 1);
        }

        [Test] 
        public void TestIsPlayable()
        {
            Assert.True(mt.IsPlayable(h, d11, out bool? mustFlip));
        }

        [Test] 
        public void TestPlaySide1()
        {
            Domino d12 = new Domino(1, 2);
            mt.Play(h, d12);
            Assert.AreEqual(mt.LastDomino, d12);
            Assert.AreEqual(mt.Count, 2);
            Assert.AreEqual(mt.PlayableValue, 2);
        }

        [Test]
        public void TestPlaySide2()
        {
            Domino d21 = new Domino(2, 1);
            mt.Play(h, d21);
            Assert.AreEqual(mt.LastDomino, d21);
            Assert.AreEqual(mt.Count, 2);
            Assert.AreEqual(mt.PlayableValue, 2);
        }

        [Test]
        public void TestInvalidPlay()
        {
            Domino d22 = new Domino(2, 2);
            Assert.Throws<ArgumentException>(() => mt.Play(h, d22));
            Assert.AreEqual(mt.LastDomino, d01);
            Assert.AreEqual(mt.Count, 1);
            Assert.AreEqual(mt.PlayableValue, 1);
        }
    }
}
