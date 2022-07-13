using HostLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Storage.Module.Controllers.Base;
using Storage.Module.Controllers.DTO;
using Storage.Module.Entities;
using Storage.Module.Localization;
using Storage.Module.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Module.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtAuthService _jwtAuthService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository userRepository, IJwtAuthService jwtAuthService, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _jwtAuthService = jwtAuthService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userRepository.Get();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<User> GetAsync(long id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] User obj)
        {
            if (obj == null)
            {
                return BadRequest(StorageLoc.Empty);
            }

            return StringToResult(await _userRepository.CreateAsync(obj));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] User obj)
        {
            if (obj == null)
            {
                return BadRequest(StorageLoc.Empty);
            }

            if (await _userRepository.LoginAsync(obj))
            {
                string encodedJwt = _jwtAuthService.GetToken(obj.UserName);

                var response = new
                {
                    username = obj.UserName,
                    token = encodedJwt
                };

                return Ok(response);
            }
            else
            {
                return BadRequest(StorageLoc.IncorrectLoginOrPassword);
            }
        }

        [Authorize]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] UserChangePasswordDTO obj)
        {
            if (obj == null)
            {
                return BadRequest(StorageLoc.Empty);
            }

            return StringToResult(await _userRepository.ChangePasswordAsync(obj.UserName, obj.OldPassword, obj.NewPassword));
        }

        [Authorize]
        [HttpPut("updatelanguage")]
        public async Task<IActionResult> UpdateLanguageAsync(string userName, string language)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest(StorageLoc.EmptyLogin);
            }

            return StringToResult(await _userRepository.UpdateLanguageAsync(userName, language));
        }

        [Authorize]
        [HttpGet("getlanguage")]
        public async Task<IActionResult> GetLanguageAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest(StorageLoc.EmptyLogin);
            }

            (bool isSuccess, string message) = await _userRepository.GetLanguageAsync(userName);

            if (isSuccess)
            {
                return Ok(message);
            }

            return BadRequest(message);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] User newObj)
        {
            if (newObj == null || newObj.Id != id)
            {
                return BadRequest(StorageLoc.NotEqualIds);
            }

            var obj = await _userRepository.GetByIdAsync(newObj.Id);

            if (obj == null)
            {
                return NotFound(string.Format(StorageLoc.NotFoundWithId, nameof(User), newObj.Id));
            }

            return StringToResult(await _userRepository.UpdateAsync(obj, newObj));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var obj = await _userRepository.GetByIdAsync(id);

            if (obj == null)
            {
                return NotFound(StorageLoc.NotFoundForRemove);
            }

            return StringToResult(await _userRepository.DeleteAsync(obj));
        }
    }
}