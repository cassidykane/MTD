using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    [Serializable()]
    public class Domino
    {

        private int side1;
        private int side2;

        public Domino()
        {
        }

        public Domino(int p1, int p2)
        {
            this.Side1 = p1;
            this.Side2 = p2;
        }

        // don't use an auto implemented property because of the validation in the setter - p 390
        public int Side1
        {
            get { return side1; }
            set
            {
                if (value < 0 || value > 14)
                {
                    throw new ArgumentException("value is out of range");
                }
                side1 = value;
            }
        }


        public int Side2
        {
            get { return side2; }
            set
            {
                if (value < 0 || value > 14)
                {
                    throw new ArgumentException("value is out of range");
                }
                side2 = value;
            }
        }

        public void Flip()
        {
            int placeholder = side1;
            Side1 = side2;
            Side2 = placeholder;
            
        }

        /// This is how I would have done this in 233N
        public int Score => side1 + side2;

        // because it's a read only property, I can use the "expression bodied syntax" or a lamdba expression - p 393
        //public int Score => side1 + side2;

        //ditto for the first version of this method and the next one
        public bool IsDouble() => (side1 == side2) ? true : false;

        // could you do this one using a lambda expression?
        public string Filename => String.Format("d{0}{1}.png", side1, side2);

        public override string ToString() => String.Format("Side 1: {0}  Side 2: {1}", side1, side2);

        // could you overload the == and != operators?
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Domino d = (Domino)obj;
            if (this.Side1 == d.Side1 && this.Side1 == d.Side1)
                return true;
            else
                return false;
        }
       
        public static bool operator == (Domino d1, Domino d2)
        {
            if (Object.Equals(d1, null))
                if (Object.Equals(d2, null))
                    return true;
                else
                    return false;
            else
                return d1.Equals(d2);
        }

        public static bool operator != (Domino d1, Domino d2)
        {
            return !(d1 == d2);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
