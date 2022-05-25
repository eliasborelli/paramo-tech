using Sat.Recruitment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Core.Entities
{
    public class SuperUser : User
    {
        public override void CalculateMoney()
        {
            if (Money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = Money * percentage;
                Money += gif;
            }
        }
    }
}
