using Sat.Recruitment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Core.Entities
{
    public class NormalUser : User
    {
        public override void CalculateMoney()
        {
            if (Money > 100 || (Money < 100 && Money > 10))
            {
                var percentage = ((Money < 100 && Money > 10)) ? Convert.ToDecimal(0.8) : Convert.ToDecimal(0.12);
                var gif = Money * percentage;
                Money += gif;
            }
        }
    }
}
