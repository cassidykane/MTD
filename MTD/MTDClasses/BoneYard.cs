using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MTDClasses
{
    public class BoneYard
    {
        private List<Domino> dominos;

        public BoneYard(int maxDots)
        {
            //int numOfDominos = ((maxDots*maxDots + 3 * maxDots + 2)/2);
            dominos = new List<Domino>();
            if (maxDots > 0)
            {
                for (int side1 = 0; side1 <= maxDots; side1++)
                {
                    Domino domino = new Domino(side1, side1);
                    dominos.Add(domino);
                    //if (side1 <= maxDots)
                    //{
                        for (int side2 = side1 + 1; side2 <= maxDots; side2++)
                        {
                            domino = new Domino(side1, side2);
                            dominos.Add(domino);
                        }
                    //}
                }
            }

        }

        public Domino this[int i]
        {
            get { return dominos[i]; }
            set { dominos[i] = value; }
        }
        
        public int DominosRemaining
        {
            get
            {
                return dominos.Count();
            }
        }
        
        public void Add(Domino domino) => dominos.Add(domino);
 
        public void Shuffle()
        {
            var rand = new Random();
            List<Domino> shuffled = new List<Domino>();
            int index;

            while (dominos.Count > 0)
            {
                index = rand.Next(dominos.Count);
                shuffled.Add(dominos[index]);
                dominos.RemoveAt(index);
            }
            foreach (Domino d in shuffled)
                dominos.Add(d);
        }

        public bool IsEmpty() => (dominos.Count < 1) ? true : false;
        
        public Domino Draw()
        {
            Domino d;
            var rand = new Random();
            int index = rand.Next(dominos.Count);

            d = dominos[index];
            dominos.RemoveAt(index);
            return d;
        }
        
        /*
        public override string ToString()
        {
        }
        */
    }
}
