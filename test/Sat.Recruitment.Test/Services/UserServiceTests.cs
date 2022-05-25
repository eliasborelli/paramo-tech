using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Enums;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Services.Interfaces;
using Sat.Recruitment.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private IUserService _userService;
        private Mock<IUserRepository> _userRepository;

        [TestInitialize]
        public void SetUp()
        {
            _userRepository = new Mock<IUserRepository>();
            _userService = new UserService(_userRepository.Object);
        }

        [TestMethod]
        public void User_ShoulCreateSuperUser()
        {
            //init
            var user = new User()
            {
                Name = "elias",
                Email = "eliasborelli@gmail.com",
                Address = "barragan n 21",
                Phone = "2216096294",
                UserType = UserType.SuperUser,
                Money = 100
            };

            //act
            var result = user.GetUserType();

            //assert
            Assert.AreEqual(result.GetType(), typeof(SuperUser));
        }

        [TestMethod]
        public void User_ShoulCreateNormalUser()
        {
            //init

            var user = new User()
            {
                Name = "elias",
                Email = "eliasborelli@gmail.com",
                Address = "barragan n 21",
                Phone = "2216096294",
                UserType = UserType.Normal,
                Money = 100
            };

            //act
            var result = user.GetUserType();

            //assert
            Assert.AreEqual(result.GetType(), typeof(NormalUser));
        }

        [TestMethod]
        public void User_ShoulCreatePremiumUser()
        {
            //init
            var user = new User()
            {
                Name = "elias",
                Email = "eliasborelli@gmail.com",
                Address = "barragan n 21",
                Phone = "2216096294",
                UserType = UserType.Premium,
                Money = 100
            };

            //act
            var result = user.GetUserType();

            //assert
            Assert.AreEqual(result.GetType(), typeof(PremiumUser));
        }

        [TestMethod]
        public void User_WhenCallCalculeMoneyInSuperUser()
        {
            //init
            var moneyAfterCall = 132;
            var user = new User()
            {
                Name = "elias",
                Email = "eliasborelli@gmail.com",
                Address = "barragan n 21",
                Phone = "2216096294",
                UserType = UserType.SuperUser,
                Money = 110
            };

            //act
            var result = user.GetUserType();

            result.CalculateMoney();

            //assert
            Assert.AreEqual(result.Money, moneyAfterCall);
        }

        [TestMethod]
        public void User_WhenCallCalculeMoneyInNormalUser()
        {
            //init
            decimal moneyAfterCall = 123.20m;
            var user = new User()
            {
                Name = "elias",
                Email = "eliasborelli@gmail.com",
                Address = "barragan n 21",
                Phone = "2216096294",
                UserType = UserType.Normal,
                Money = 110
            };

            //act
            var result = user.GetUserType();

            result.CalculateMoney();

            //assert
            Assert.AreEqual(result.Money, moneyAfterCall);
        }

        [TestMethod]
        public void User_WhenCallCalculeMoneyInPremiumUser()
        {
            //init
            decimal moneyAfterCall = 330m;
            var user = new User()
            {
                Name = "elias",
                Email = "eliasborelli@gmail.com",
                Address = "barragan n 21",
                Phone = "2216096294",
                UserType = UserType.Premium,
                Money = 110
            };

            //act
            var result = user.GetUserType();

            result.CalculateMoney();

            //assert
            Assert.AreEqual(result.Money, moneyAfterCall);
        }

        [TestMethod]
        public void User_ShoulFailedWhenUserIsDuplicated()
        {
            //init
            var user = new User()
            {
                Name = "elias",
                Email = "eliasborelli@gmail.com",
                Address = "barragan n 21",
                Phone = "2216096294",
                UserType = UserType.Normal,
                Money = 110
            };

            _userRepository.Setup(x => x.GetUsers()).Returns(Task.FromResult((IEnumerable<User>)(new List<User>() { new User() { Email = "eliasborelli@gmail.com" } })));

            _userService = new UserService(_userRepository.Object);

            //act
            var result = _userService.CreateUser(user).GetAwaiter().GetResult();

            //assert
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual(result.Error, "User is duplicated");
        }

        [TestMethod]
        public void User_ShoulOk()
        {
            //init
            var user = new User()
            {
                Name = "elias",
                Email = "eliasborelli@gmail.com",
                Address = "barragan n 21",
                Phone = "2216096294",
                UserType = UserType.Normal,
                Money = 110
            };

            _userRepository.Setup(x => x.GetUsers()).Returns(Task.FromResult((IEnumerable<User>)(new List<User>() { new User() })));

            _userService = new UserService(_userRepository.Object);

            //act
            var result = _userService.CreateUser(user).GetAwaiter().GetResult();

            //assert
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(result.Value, "User Created");
        }
    }
}
