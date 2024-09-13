using Microsoft.AspNetCore.Mvc;
using APP.Interfaces.Repository;
using APP.Interfaces.Services;
using APP.Entities;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    private readonly ICryptographyServices _cryptographyServices;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository, ICryptographyServices cryptographyServices){
        _logger = logger;
        _userRepository = userRepository;
        _cryptographyServices = cryptographyServices;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        try{
            IEnumerable<User> users = await _userRepository.GetAll();
            return StatusCode(200, users);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }

    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<IActionResult> Get(Guid id)
    {
        try{
            User user = await _userRepository.GetById(id);
            if(user == null){
                return StatusCode(404, "User not found");
            }
            return StatusCode(200, user);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        string hashPassword = _cryptographyServices.Encrypt(dto.Password);
        User user = new User(dto.Name, dto.Email, hashPassword, dto.Role, dto.ProfilePicture);

        try{
            User userExists = await _userRepository.GetByEmail(dto.Email);
            if(userExists != null){
                return StatusCode(409, "Email already exists");
            }
            await _userRepository.Create(user);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
        return StatusCode(201, user);
    }

    [HttpPut("{userId}", Name = "UpdateUser")]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UpdateUserDto dto)
    {
        User user = await _userRepository.GetById(userId);
        if (user == null)
        {
            return StatusCode(404, "User not found");
        }

        if (dto.Name != null)
        {
            user.Name = dto.Name;
        }

        if (dto.Email != null)
        {
            User userExists = await _userRepository.GetByEmail(dto.Email);
            if (userExists != null)
            {
                return StatusCode(409, "Email already exists");
            }
            user.Email = dto.Email;
        }

        if (dto.Password != null)
        {
            user.Password = _cryptographyServices.Encrypt(dto.Password);
        }

        if (dto.Role != null)
        {
            user.Role = (UserRole)dto.Role;
            user.AllowPermitionsByRole();
        }

        if (dto.ProfilePicture != null)
        {
            user.ProfilePicture = dto.ProfilePicture;
        }

        user.UpdatedAt = DateTime.Now;

        try
        {
            await _userRepository.Update(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }

        return StatusCode(204, "User updated");
    }

    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try{
            await _userRepository.Delete(id);
            return StatusCode(204, "User deleted");
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }
}