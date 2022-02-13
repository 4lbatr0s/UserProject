using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<User> Create(UserDto userDto)
        {
            string date = DateTime.Now.ToString("dd.MM.yyyy");
            date = date.Replace('\u00A0', ' '); //to remove possible unicode characters.
            User user = new User { 
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.CurrentCulture),
                Gender = userDto.Gender,
                Name = userDto.Name,
                Surname = userDto.Surname,
                BirthDate = DateTime.ParseExact(userDto.BirthDate, "dd.MM.yyyy", CultureInfo.CurrentCulture)
            };
           
            _userDal.Add(user);
            return new SuccessDataResult<User>(Messages.UserCreated);
        }

        //delete by id.
        public IResult Delete(string id)
        {
            var ifExistsOrNot= _userDal.GetAll(u => u.Id.ToString() == id).Any();
            if (!ifExistsOrNot)
            {
                return new ErrorResult(Messages.UserDoesNotExist);
            }
            var result = _userDal.Get(u => u.Id.ToString() == id);
            _userDal.Delete(result);
            return new SuccessResult(Messages.UserDeleted);
        }

        //get all users.
        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();
            return new SuccessDataResult<List<User>>(result);
        }

        public IDataResult<List<User>> GetAllTest()
        {
            var result = _userDal.GetAllTest();
            return new SuccessDataResult<List<User>>(result);
        }

        //get all users filtered by the date created.
        public IDataResult<List<User>> GetByDate(string dateTime)
        {
            var date= DateTime.ParseExact(dateTime, "dd.MM.yyyy", CultureInfo.CurrentCulture);
            var userDateMatchExist = _userDal.GetAll(u => u.CreatedAt == date).Any();
            var result= _userDal.GetAll(u => u.CreatedAt == date);
            if (!userDateMatchExist)
            {
                return new ErrorDataResult<List<User>>(Messages.UserDateMismatch);
            }
            else
            {
                return new SuccessDataResult<List<User>>(result);
            }
            
        }

        public IDataResult<List<User>> GetByDateTest(string dateTime)
        {
            var date = DateTime.ParseExact(dateTime, "dd.MM.yyyy", CultureInfo.CurrentCulture);
            var result = _userDal.GetAll(u => u.CreatedAt == date);
            return new SuccessDataResult<List<User>>(result);

        }

        //get user by id.
        public IDataResult<User> GetById(string id)
        {
            var userExistOrNot = _userDal.GetAll(u => u.Id.ToString() == id).Any();
            var result = _userDal.Get(u => u.Id.ToString() == id);
            if (!userExistOrNot)
            {
                return new ErrorDataResult<User>(Messages.UserDoesNotExist);
            }
            else
            {
                return new SuccessDataResult<User>(result);
            }
        }

        //update user.
        public IDataResult<User> Update(UserDtoForUpdate userDtoForUpdate)
        {
            var userExistOrNot = _userDal.GetAll(u => u.Id.ToString() == userDtoForUpdate.Id).Any();
            if (!userExistOrNot)
            {
                return new ErrorDataResult<User>(Messages.UserDoesNotExist);
            }
            else
            {
                User user = new User
                {
                   Id = Guid.Parse(userDtoForUpdate.Id),
                   Name = userDtoForUpdate.Name,
                   Surname = userDtoForUpdate.Surname,
                   BirthDate = DateTime.ParseExact(userDtoForUpdate.BirthDate, "dd.MM.yyyy", CultureInfo.CurrentCulture),
                   CreatedAt = DateTime.ParseExact(userDtoForUpdate.CreatedAt, "dd.MM.yyyy", CultureInfo.CurrentCulture),
                   Gender = userDtoForUpdate.Gender

            };
                _userDal.Update(user);
                return new SuccessDataResult<User>(user, Messages.UserUpdated);
            }
        }
    }
}
