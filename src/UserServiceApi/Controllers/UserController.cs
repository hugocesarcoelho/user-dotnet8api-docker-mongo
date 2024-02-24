using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UserServiceRepository.Interface;
using UserServiceRepository.Model;

namespace UserServiceApi.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(User user)
        {
            var response = await _userRepository.CreateAsync(user);
            return CreatedAtRoute(nameof(GetUserByIdAsync), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([Required] string id, User user)
        {
            await _userRepository.UpdateAsync(id, user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}", Name = nameof(GetUserByIdAsync))]
        public async Task<IActionResult> GetUserByIdAsync([Required] string id)
            => Ok(await _userRepository.GetByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int offset = 0, [FromQuery] int fetch = 100)
            => Ok(await _userRepository.GetAllAsync(offset, fetch));
    }
}