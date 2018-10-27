using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    /// <summary>
    /// Represents a hand of dominos
    /// </summary>
    public class Hand
    {
        /// <summary>
        /// The list of dominos in the hand
        /// </summary>
        List<Domino> hand;

        /// <summary>
        /// Creates an empty hand
        /// </summary>
        public Hand()
        {
            hand = new List<Domino>();
        }

        /// <summary>
        /// Creates a hand of dominos from the boneyard.
        /// The number of dominos is based on the number of players
        /// 2–4 players: 10 dominoes each
        /// 5–6 players: 9 dominoes each
        /// 7–8 players: 7 dominoes each
        /// </summary>
        /// <param name="by"></param>
        /// <param name="numPlayers"></param>
        public Hand(BoneYard by, int numPlayers)
        {
            hand = new List<Domino>();
            int handSize;
            switch (numPlayers)
            {
                case 2: case 3: case 4:
                    handSize = 10;
                    while (handSize > 0)
                    {
                        hand.Add(by.Draw());
                        handSize--;
                    }
                    break;
                case 5: case 6:
                    handSize = 9;
                    while (handSize > 0)
                    {
                        hand.Add(by.Draw());
                        handSize--;
                    }
                    break;
                case 7: case 8:
                    handSize = 7;
                    while (handSize > 0)
                    {
                        hand.Add(by.Draw());
                        handSize--;
                    }
                    break;
                default:
                    throw new ArgumentException("must hav 2-8 players");
            }
        }

        public void Add(Domino d) => hand.Add(d);


        public int Count => hand.Count();

        public bool IsEmpty => (hand.Count < 1) ? true : false;
        

        /// <summary>
        /// Sum of the score of each domino in the hand
        /// </summary>
        public int Score
        {
            get
            {
                int total = 0;
                foreach (Domino d in hand)
                {
                    total += d.Score;
                }
                return total;
            }
        }

        /// <summary>
        /// Does the hand contain a domino with value in side 1 or side 2?
        /// </summary>
        /// <param name="value">The number of dots on one side of the domino that you're looking for</param>
        public bool HasDomino(int value)
        {
            foreach (Domino d in hand)
            {
                if (d.Side1 == value || d.Side2 == value)
                    return true;
            }
            return false;
        }

        /// <summary>
        ///  DOes the hand contain a double of a certain value?
        /// </summary>
        /// <param name="value">The number of (double) dots that you're looking for</param>
        public bool HasDoubleDomino(int value)
        {
            foreach (Domino d in hand)
            {
                if (d.Side1 == value && d.Side2 == value)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The index of a domino with a value in the hand
        /// </summary>
        /// <param name="value">The number of dots on one side of the domino that you're looking for</param>
        /// <returns>-1 if the domino doesn't exist in the hand</returns>
        public int IndexOfDomino(int value)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Side1 == value || hand[i].Side2 == value)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// The index of a domino with both side values in the hand
        /// </summary>
        /// <param name="d">The number of dots on one side of the domino that you're looking for</param>
        /// <returns>-1 if the domino doesn't exist in the hand</returns>
        public int IndexOfDomino(Domino d)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Side1 == d.Side1 && hand[i].Side2 == d.Side2)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// The index of the double domino with the value you're looking for
        /// </summary>
        /// <param name="value">The number of (double) dots that you're looking for</param>
        /// <returns>-1 if the domino doesn't exist in the hand</returns>
        public int IndexOfDoubleDomino(int value)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Side1 == value && hand[i].Side2 == value)
                    return i;
            }
            return -1;

        }

        /// <summary>
        /// The index of the highest double domino in the hand
        /// </summary>
        /// <returns>-1 if there isn't a double in the hand</returns>
        public int IndexOfHighDouble()
        {
            int highestDouble = -1;
            //int indexOfHighestDouble;
            foreach (Domino d in hand)
            {
                if (d.Side1 == d.Side2 && d.Side1 > highestDouble)
                    highestDouble = d.Side1;
            }
            return IndexOfDoubleDomino(highestDouble);
        }

        public Domino this[int i]
        {
            get { return hand[i]; }
            set { hand[i] = value; }
        }

        public void RemoveAt(int index) => hand.RemoveAt(index);

        /// <summary>
        /// Finds a domino with a certain number of dots in the hand.
        /// If it can find the domino, it removes it from the hand and returns it.
        /// Otherwise it returns null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Domino GetDomino(int value)
        {
            foreach (Domino d in hand)
            {
                if (d.Score == value)
                    return d;
            }
            return null;
        }

        /// <summary>
        /// Finds a domino with a certain number of double dots in the hand.
        /// If it can find the domino, it removes it from the hand and returns it.
        /// Otherwise it returns null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Domino GetDoubleDomino(int value) => hand[IndexOfDoubleDomino(value)];

        /// <summary>
        /// Draws a domino from the boneyard and adds it to the hand
        /// </summary>
        /// <param name="by"></param>
        public void Draw(BoneYard by) => hand.Add(by.Draw());

        /// <summary>
        /// Plays the domino at the index on the train.
        /// Flips the domino if necessary before playing.
        /// Removes the domino from the hand.
        /// Throws an exception if the domino at the index
        /// is not playable.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="t"></param>
        public void Play(int index, Train t)
        {
            Domino d = hand[index];
            hand.Remove(d);
            t.Play(this, d);
        }

        /// <summary>
        /// Plays the domino from the hand on the train.
        /// Flips the domino if necessary before playing.
        /// Removes the domino from the hand.
        /// Throws an exception if the domino is not in the hand
        /// or is not playable.
        /// </summary>
        public void Play(Domino d, Train t)
        {
            if (!hand.Contains(d))
                throw new ArgumentException("This domino is not in the hand");
            hand.Remove(d);
            t.Play(this, d);
        }

        /// <summary>
        /// Plays the first playable domino in the hand on the train
        /// Removes the domino from the hand.
        /// Returns the domino.
        /// Throws an exception if no dominos in the hand are playable.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Domino Play(Train t)
        {
            foreach (Domino d in hand)
            {
                t.Play(this, d);
                hand.Remove(d);
                return d;
            }
            throw new ArgumentException("no playable dominos in the hand");
        }

        //public override string ToString() { }
    
    }
}
