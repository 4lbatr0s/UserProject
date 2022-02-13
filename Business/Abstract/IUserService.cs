using Core.Utilities.Results;
using Entity;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> Create(UserDto userDto);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(string id);
        IDataResult<List<User>> GetByDate(string date);
        IDataResult<List<User>> GetByDateTest(string date);
        IResult Delete(string id);
        IDataResult<User> Update(UserDtoForUpdate userDtoForUpdate);

        IDataResult<List<User>> GetAllTest();
 
    }
}
