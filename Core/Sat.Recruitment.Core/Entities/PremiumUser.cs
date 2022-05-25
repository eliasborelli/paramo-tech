using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Core.Entities
{
    public class PremiumUser : User
    {
        public override void CalculateMoney()
        {
            if (Money > 100)
            {
                var gif = Money * 2;
                Money += gif;
            }
        }
    }
}
