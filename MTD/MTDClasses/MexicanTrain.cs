using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    public class MexicanTrain : Train
    {
        public MexicanTrain(int engineValue) : base(engineValue) { }

        public override bool IsPlayable(Hand h, Domino d, out bool? mustFlip) => IsPlayable(d, out mustFlip);
        //{
        //    if (IsPlayable(d, out mustFlip))
        //        return true;
        //    /mustFlip = null;
        //    return false;
        //}
    }
}
