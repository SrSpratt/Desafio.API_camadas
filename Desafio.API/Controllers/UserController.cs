using Desafio.Domain.Dtos;
using Desafio.Domain.Setup;
using Desafio.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IApiConfig _config;

        public UserController(IUserService service, IApiConfig config)
        {
            _service = service;
            _config = config;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var user = await _service.ReadUser(id);
            return user == null ? NoContent() : Ok(user);
        }


        //Talvez eu use 
        [HttpGet("{name:alpha}")]
        public async Task<ActionResult<UserDTO>> Login(string name)
        {
            try
            {
                var user = await _service.Login(name, "senha");
                return user == null ? NoContent() : Ok(user);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody]LoginRequest login)
        {

            try
            {
                var user = await _service.Login(login.Name, login.Password);
                return user == null ? NoContent() : Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Index()
        {
            var users = await _service.ReadUsers();
            return users == null ? NoContent() : Ok(users); 
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create([FromBody]UserDTO user)
        {
            var userId = await _service.CreateUser(user);
            //var userObject = await _service.ReadUser(userId);

            return CreatedAtAction("Get", new { id = user.ID }, null);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDTO>> Update(int id, [FromBody]UserDTO user)
        {
            try
            {
                await _service.UpdateUser(id, user);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserDTO>> Delete(int id)
        {
            try
            {
                await _service.DeleteUser(id);
                var result = await _service.ReadUser(id);
                return result == null ? Ok() : BadRequest("Delete didn't work");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
