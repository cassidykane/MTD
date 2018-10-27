using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTDClasses
{
    /// <summary>
    /// Represents a generic Train for MTD
    /// </summary>
    public abstract class Train : IEnumerable
    {
        protected List<Domino> dominos;
        protected int engineValue;

        public Train(int engValue)
        {
            engineValue = engValue;
            dominos = new List<Domino>();
        }

        public int Count => dominos.Count;

        /// <summary>
        /// The first domino value that must be played on a train
        /// </summary>
        public int EngineValue => engineValue;

        public bool IsEmpty => (dominos.Count < 1) ? true : false;
        // change this to return null instead of arg exception
        public Domino LastDomino => IsEmpty ? null : dominos.Last();

        /// <summary>
        /// Side2 of the last domino in the train.  It's the value of the next domino that can be played.
        /// </summary>
        public int PlayableValue => (IsEmpty) ? engineValue : LastDomino.Side2;

        public void Add(Domino d)
        {
            dominos.Add(d);
        }

        public Domino this[int i]
        {
            get { return dominos[i]; }
            set { dominos[i] = value; }
        }


        /// <summary>
        /// Determines whether a hand can play a specific domino on this train and if the domino must be flipped.
        /// Because the rules for playing are different for Mexican and Player trains, this method is abstract.
        /// </summary>
        public abstract bool IsPlayable(Hand h, Domino d, out bool? mustFlip);

        /// <summary>
        /// A helper method that determines whether a specific domino can be played on this train.
        /// It can be called in the Mexican and Player train class implementations of the abstract method
        /// </summary>
        public bool IsPlayable(Domino d, out bool? mustFlip)
        {
            if (d.Side1 == PlayableValue)
            {
                mustFlip = false;
                return true;
            } else if (d.Side2 == PlayableValue) 
            {
                mustFlip = true;
                return true;
            } else
            {
                mustFlip = null;
                return false;
            }
        }

        // assumes the domino has already been removed from the hand
        public void Play(Hand h, Domino d)
        {
            if (this.IsPlayable(h, d, out bool? mustFlip))
            {
                if (mustFlip == true)
                {
                    d.Flip();
                }
                dominos.Add(d);
            }
            else
                throw new ArgumentException("This domino is not playable");
        }
        
        public override string ToString()
        {
            return engineValue.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Domino d in dominos)
                yield return d;
        }
    }
}