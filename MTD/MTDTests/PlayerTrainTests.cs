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
    class PlayerTrainTests
    {
        PlayerTrain ptEmpty;
        PlayerTrain pt;
        Hand h;
        Domino d01;
        Domino d11;

        [SetUp]
        public void SetUpAllTests()
        {
            ptEmpty = new PlayerTrain(h, 0);
            pt = new PlayerTrain(h, 0);
            h = new Hand();
            d01 = new Domino(0, 1);
            d11 = new Domino(1, 1);

            pt.Add(d01);
            h.Add(d11);
        }

        [Test]
        public void TestCount()
        {
            Assert.AreEqual(ptEmpty.Count, 0);
        }

        [Test]
        public void TestOverloadedConstructor()
        {
            Assert.AreEqual(ptEmpty.Count, 0);
            Assert.AreEqual(ptEmpty.EngineValue, 0);
        }

        [Test]
        public void TestEngineValue()
        {
            Assert.AreEqual(pt.EngineValue, 0);
        }

        [Test]
        public void TestIsEmpty()
        {
            Assert.True(ptEmpty.IsEmpty);
            Assert.False(pt.IsEmpty);
        }

        [Test]
        public void TestAdd()
        {
            pt.Add(d11);
            Assert.AreEqual(pt.Count, 2);
        }

        [Test]
        public void TestIndexer()
        {
            Assert.AreEqual(pt[0], d01);
        }

        [Test]
        public void TestLastDomino()
        {
            pt.Add(d11);
            Assert.AreEqual(pt.LastDomino, d11);
        }

        [Test]
        public void TestPlayableValue()
        {
            Assert.AreEqual(pt.PlayableValue, 1);
        }

        [Test]
        public void TestIsOpen()
        {
            Assert.False(pt.IsOpen);
        }

        [Test]
        public void TestOpen()
        {
            pt.Open();
            Assert.True(pt.IsOpen);
        }

        [Test]
        public void TestClose()
        {
            pt.Open();
            Assert.True(pt.IsOpen);
            pt.Close();
            Assert.False(pt.IsOpen);
        }

        [Test]
        public void TestIsPlayable()
        {
            pt.Open();
            Assert.True(pt.IsPlayable(h, d11, out bool? mustFlip));
        }

        [Test]
        public void TestPlay()
        {
            pt.Play(h, d11);
            Assert.AreEqual(pt.LastDomino, d11);
        }
    }
}
