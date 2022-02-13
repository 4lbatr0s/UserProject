using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity;
using Entity.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Test
{  

    [TestClass]
    public class UserManagerTests
    {
        Mock<IUserDal> _mockUserDal;
        List<User> _dbUsers;
        [TestInitialize]
        public void Start()
        {
            _mockUserDal = new Mock<IUserDal>(); //works like IUserDal owns a EfUserDal on the background.
            var dateOne =  DateTime.ParseExact("10.02.2022", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var dateTwo = DateTime.ParseExact("11.02.2022", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var dateThree = DateTime.ParseExact("12.02.2022", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var dateFour = DateTime.ParseExact("13.02.2022", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var dateFive = DateTime.ParseExact("14.02.2022", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var dateSix = DateTime.ParseExact("15.02.2022", "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var birthOne = DateTime.ParseExact("10.02.1998", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var birthTwo = DateTime.ParseExact("11.02.1998", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var birthThree = DateTime.ParseExact("12.02.1998", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var birthFour = DateTime.ParseExact("13.02.1998", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var birthFive = DateTime.ParseExact("14.02.1998", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var birthSix = DateTime.ParseExact("15.02.1998", "dd.MM.yyyy", CultureInfo.InvariantCulture);

            _dbUsers = new List<User>
            {
                new User {Id = Guid.Parse("783996da-cafb-4f4f-a1f5-7f431bfccad3"), BirthDate = birthOne, CreatedAt = dateOne, Gender = "Male",Name = "serhat", Surname = "oner"},
                new User {Id =  Guid.Parse("31218761-2763-4306-8844-fd6eafe74e93"), BirthDate = birthTwo, CreatedAt = dateTwo, Gender = "Female",Name = "Maria", Surname = "Callas"},
                new User {Id =  Guid.Parse("e0aea2ea-c393-4e85-9e9b-968b65e2e3ed"), BirthDate = birthThree, CreatedAt = dateThree, Gender = "Male",Name = "Leonard", Surname = "Cohen"},
                new User {Id =  Guid.Parse("3677ab3f-635d-4764-852a-7c946a88f925"), BirthDate = birthFour, CreatedAt = dateFour, Gender = "Female",Name = "Marie", Surname = "Curie"},
                new User {Id = Guid.Parse("5431f998-af5e-46a9-85f5-958183258602"), BirthDate =  birthFive, CreatedAt = dateFive, Gender = "Female",Name = "Amalia", Surname = "Rodrigues"}

            };
            _mockUserDal.Setup(m => m.GetAllTest()).Returns(_dbUsers);
            _mockUserDal.Setup(m => m.GetByDateTest(dateOne.ToString())).Returns(_dbUsers.Where(u => u.CreatedAt == dateOne).ToList());
            _mockUserDal.Setup(m => m.GetByDateTest("10.02.1998")).Returns(_dbUsers.Where(x => x.CreatedAt == dateOne).ToList());
            
            //User olivia = new User
            //{
            //    Id = Guid.Parse("123496da-cafb-4f4f-a1f5-7f431bfccad3"),
            //    Name = "Olivia",
            //    Surname = "Principa",
            //    BirthDate = birthSix,
            //    CreatedAt = dateSix,
            //    Gender = "Female"
            //};
            //_mockUserDal.Setup(m => m.Add(olivia)).Verifiable();
        }

        [TestMethod]
        public void AllUsersMustBeListed()
        {
            //Arrange
            IUserService userService= new UserManager(_mockUserDal.Object);

            //Act : aksiyon alınan kısım
            IDataResult<List<User>> users = userService.GetAllTest();
            //Assert
            Assert.AreEqual(5, users.Data.Count);
        }

        [TestMethod]
        public void AllUsersMustBeListedByDate()
        {
            //Arrange
            IUserService userService = new UserManager(_mockUserDal.Object);

            //Act : aksiyon alınan kısım
            IDataResult<List<User>> users = userService.GetByDateTest("10.02.2022");
            //Assert
            Assert.AreEqual(1, users.Data.Count);
        }


        //[TestMethod]
        //public void UserManager_GetById_Invalid_Id()
        //{
        //    _mockUserDal.Setup(m => m.Get(u => u.Id == _dbUsers.ElementAt(0).Id)).Returns(_dbUsers.ElementAt(0));
        //    //Arrange
        //    IUserService userService = new UserManager(_mockUserDal.Object);
        //    //Act : aksiyon alınan kısım
        //    IDataResult<User> user = userService.GetById();
        //    //Assert
        //    Assert.AreEqual("Amalia", user.Data.Name);
        //}


        //[TestMethod]
        //public void UserManager_Add_User_Test()
        //{
        //    _mockUserDal.Setup(m => m.Get(u => u.Id == _dbUsers.ElementAt(0).Id)).Returns(_dbUsers.ElementAt(0));
        //    //Arrange
        //    IUserService userService = new UserManager(_mockUserDal.Object);
        //    //Act : aksiyon alınan kısım
        //    UserDto userDto = new UserDto
        //    {
        //        Name = "Olivia",
        //        Surname = "Principa",
        //        BirthDate = birthSix
        //    };
        //    IDataResult<User> user = userService.Create()
        //    //Assert
        //    Assert.AreEqual("Amalia", user.Data.Name);
        //}


    }
}
