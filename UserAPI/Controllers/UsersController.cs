using Business.Abstract;
using Entity;
using Entity.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("create")]

        public IActionResult Create(UserDto userDto)
        {
            var result = _userService.Create(userDto);
            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            return result.Success 
                ? Ok(result)
                :BadRequest(result.Message);
        }

        [HttpGet("byid")]

        public IActionResult GetById(string id)
        {
            var result = _userService.GetById(id);
            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }

        [HttpGet("bydate")]

        public IActionResult GetByDate(string dateTime)
        {
            var result = _userService.GetByDate(dateTime);
            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }



        [HttpPut("update")]

        public IActionResult Update(UserDtoForUpdate userDtoForUpdate)
        {
            var result = _userService.Update(userDtoForUpdate);
            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }

        [HttpDelete("delete")]

        public IActionResult Update(string id)
        {
            var result =  _userService.Delete(id);
            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }
    }
}
