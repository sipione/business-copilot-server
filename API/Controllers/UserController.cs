using Microsoft.AspNetCore.Mvc;
using APP.Interfaces.Services;
using APP.Entities;
using APP.Exceptions;
using APP.UseCases;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase{
    private readonly ILogger<UserController> _logger;
    private readonly ICryptographyServices _cryptographyServices;
    private readonly GetAllUsersUseCase _getAllUsersUseCase;
    private readonly GetUserByIdUseCase _getUserByIdUseCase;
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly UpdateUserUseCase _updateUserUseCase;
    private readonly DeleteUserUseCase _deleteUserUseCase;
    private readonly RegisterUserUseCase _registerUserUseCase;
    private readonly LoginUserUseCase _loginUserUseCase;

    public UserController(
        ILogger<UserController> logger,
        ICryptographyServices cryptographyServices,
        GetAllUsersUseCase getAllUsersUseCase,
        GetUserByIdUseCase getUserByIdUseCase,
        CreateUserUseCase createUserUseCase,
        UpdateUserUseCase updateUserUseCase,
        DeleteUserUseCase deleteUserUseCase,
        RegisterUserUseCase registerUserUseCase,
        LoginUserUseCase loginUserUseCase
    ){
        _logger = logger;
        _cryptographyServices = cryptographyServices;
        _getAllUsersUseCase = getAllUsersUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _createUserUseCase = createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
        _registerUserUseCase = registerUserUseCase;
        _loginUserUseCase = loginUserUseCase;
    }

    private string SaveProfilePicture(IFormFile profilePicture){
        // Save profile picture logic
        var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile_pictures");
        if (!Directory.Exists(uploadsDir))
        {
            Directory.CreateDirectory(uploadsDir);
        }

        var fileExtension = Path.GetExtension(profilePicture.FileName);
        var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
        var filePath = Path.Combine(uploadsDir, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            profilePicture.CopyTo(fileStream);
        }

        return $"/profile_pictures/{uniqueFileName}";
    }

    private void DeleteProfilePicture(string profilePicturePath){
        var currentProfilePicturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profilePicturePath);
        if (System.IO.File.Exists(currentProfilePicturePath))
        {
            System.IO.File.Delete(currentProfilePicturePath);
        }
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get([FromHeader] string token, [FromHeader] Guid userId){
        try{
            IEnumerable<User> users = await _getAllUsersUseCase.Execute(userId, token);
            return StatusCode(200, users);
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{userId}", Name = "GetUser")]
    public async Task<IActionResult> Get([FromRoute]Guid userId, [FromHeader] string token, [FromHeader] Guid requestUserId){
        try{
            User user = await _getUserByIdUseCase.Execute(userId, requestUserId, token);

            List<StakeholderRefDto> stakeholders = user.Stakeholders.Select(s => new StakeholderRefDto{
                Id = s.Id,
                Name = s.Name,
                Type = s.Type
            }).ToList();

            UserResponseDto response = new UserResponseDto{
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Status = user.Status,
                ProfilePicture = user.ProfilePicture,
                Stakeholders = stakeholders
            };

            return StatusCode(200, response);

        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> Create([FromForm] CreateUserDto dto, [FromHeader] string token, [FromHeader] Guid userId){
        User newUser;
        try{
            // Handle Profile Picture upload
            string? profilePicturePath = null;
            if (dto.ProfilePicture != null){
                profilePicturePath = SaveProfilePicture(dto.ProfilePicture);
            }

            newUser = new User(
                dto.Name,
                dto.Email,
                _cryptographyServices.Encrypt(dto.Password),
                dto.Role,
                profilePicturePath,
                dto.Status
            );
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, $"Error on create new User: {e.Message}");
        }

        try{
            Guid newUserId = await _createUserUseCase.Execute(userId, token, newUser);
            return StatusCode(201, new { userId = newUserId, message= "User created" });

        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register([FromForm] CreateUserDto dto){
        string? profilePicturePath = null;
        if (dto.ProfilePicture != null){
            profilePicturePath = SaveProfilePicture(dto.ProfilePicture);
        }

        User newUser = new User(
            dto.Name,
            dto.Email,
            _cryptographyServices.Encrypt(dto.Password),
            UserRole.ORDINARY,
            profilePicturePath,
            AccountStatus.ACTIVE
        );

        try{
            await _registerUserUseCase.Execute(newUser);
            return StatusCode(201, "User registered");

        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException){
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> Login([FromForm] LoginDto dto){
        try{
            User user = await _loginUserUseCase.OpenSession(dto.Email, dto.Password);
            string token = _loginUserUseCase.GenerateToken(user.Id, user.Email);
            return StatusCode(200, new { accessToken = token, userId = user.Id, userEmail = user.Email, message = "Login successful" });
        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{userId}", Name = "UpdateUser")]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromForm] UpdateUserDto dto, [FromHeader] string token, [FromHeader] Guid userRequestId){
        string profilePicturePath = null;

        if (dto.ProfilePicture != null){
            profilePicturePath = SaveProfilePicture(dto.ProfilePicture);
        }

        if ((dto.CurrentProfilePicturePath != null && dto.ProfilePicture == null) || (dto.ProfilePicture != null && dto.CurrentProfilePicturePath != null)){
            profilePicturePath = dto.CurrentProfilePicturePath;
        }

        User userUpdated = new User(
            dto.Name,
            dto.Email,
            "default",
            dto.Role,
            profilePicturePath,
            dto.Status
        );

        try{
            await _updateUserUseCase.Execute(userId, token, userRequestId, userUpdated);
            return StatusCode(204, "User updated");
        }
        catch (Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{userId}", Name = "DeleteUser")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromHeader] string token, [FromHeader] Guid requestedId){
        try{
            await _deleteUserUseCase.Execute(userId, token, requestedId, DeleteProfilePicture);
            
            return StatusCode(204, "User deleted");

        }catch(Exception e){
            _logger.LogError(e.Message);
            if (e is CommonExceptions commonException)
            {
                return StatusCode(commonException.StatusCode, commonException.Message);
            }
            return StatusCode(500, e.Message);
        }
    }
}