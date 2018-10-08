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
        public void TestOverloadedConstructor()
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
        public void TestPlay()
        {
            mt.Play(h, d11);
            Assert.AreEqual(mt.LastDomino, d11);
        }
    }
}
