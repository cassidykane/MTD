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

        public BoneYard(/*int maxDots*/)
        {
            dominos = new List<Domino>();
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
            List<Domino> shuffledBones = new List<Domino>();
            int index;
            while (dominos.Count > 0)
            {
                index = rand.Next(dominos.Count);
                shuffledBones.Add(dominos[index]);
                dominos.RemoveAt(index);
            }
            foreach (Domino bone in shuffledBones)
                dominos.Add(bone);
        }
        /*
        public bool IsEmpty()
        {

        }

        public int DominosRemaining
        {
        }

        public Domino Draw()
        {
        }

        public Domino this[int index]
        {
        }

        public override string ToString()
        {
        }
        */
    }
}
