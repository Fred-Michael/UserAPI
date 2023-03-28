using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Users.Core.DTOs;
using Users.Core.Services;

namespace Users.API.Controllers
{
    //work on your error logging
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser([FromRoute] string userId)
        {
            try
            {
                var user = await _userService.CheckUser(userId);
                return Ok(user);
            }
            catch (ArgumentNullException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest();
            }
            catch (NullReferenceException ex)
            {
                Log.Logger.Error(ex.Message);
                return NotFound();
            }
            catch (Exception)
            {
                Log.Logger.Error("An error occured");
                return StatusCode(500);
            }
        }

        [HttpPut("update-user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            try
            {
                await _userService.UpdateUser(user);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest();
            }
            catch (NullReferenceException ex)
            {
                Log.Logger.Error(ex.Message);
                return NotFound();
            }
            catch (Exception)
            {
                Log.Logger.Error("An error occured");
                return StatusCode(500);
            }
        }

        [HttpDelete("delete-user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest();
            }
            catch (NullReferenceException ex)
            {
                Log.Logger.Error(ex.Message);
                return NotFound();
            }
            catch (Exception)
            {
                Log.Logger.Error("An error occured");
                return StatusCode(500);
            }
        }

        //public async Task<IActionResult> RefreshUserToken()
        //{

        //}
    }
}
