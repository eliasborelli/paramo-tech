using Sat.Recruitment.Core.Enums;
using System;

namespace Sat.Recruitment.Core.Entities
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
        public virtual void CalculateMoney() { }

        public User GetUserType()
        {
            var user = new User();
            switch (UserType)
            {
                case UserType.SuperUser:
                    user = new SuperUser() { Address = this.Address, Email = this.Email, Name = this.Name, Phone = this.Phone, Money = this.Money, UserType = UserType.SuperUser };
                    break;
                case UserType.Normal:
                    user = new NormalUser() { Address = this.Address, Email = this.Email, Name = this.Name, Phone = this.Phone, Money = this.Money, UserType = UserType.Normal };
                    break;
                case UserType.Premium:
                    user = new PremiumUser() { Address = this.Address, Email = this.Email, Name = this.Name, Phone = this.Phone, Money = this.Money, UserType = UserType.Premium };
                    break;
                default:
                    break;
            }
            return user;

        }
    }
}
