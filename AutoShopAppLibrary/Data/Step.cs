using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShopAppLibrary.Data
{
    internal class Step
    {
        public int CurrentStep { get; private set; }

        public Step()
        {
            CurrentStep = 1;
        }

        public void IncrementStep()
        {
            CurrentStep++;
        }

        public void ResetStep()
        {
            CurrentStep = 0;
        }
    }
}