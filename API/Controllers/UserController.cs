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
    private readonly IAuthenticationService _authenticationService;
    private readonly ICryptographyServices _cryptographyServices;
    private readonly IUserAuthorizationService _userAuthorizationService;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository, IAuthenticationService authenticationService, ICryptographyServices cryptographyServices, IUserAuthorizationService userAuthorizationService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _cryptographyServices = cryptographyServices;
        _userAuthorizationService = userAuthorizationService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get([FromHeader] string token, [FromHeader] string email, [FromHeader] Guid userId){
        try{
            User user = await _authenticationService.Authenticate(email, userId, token);
            if(user == null){
                return StatusCode(401, "Unauthorized");
            }

            if(!_userAuthorizationService.AuthorizeViewUsers(user, null)){
                return StatusCode(403, "Forbidden");
            }

            IEnumerable<User> users = await _userRepository.GetAll();
            return StatusCode(200, users);
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{userId}", Name = "GetUser")]
    public async Task<IActionResult> Get([FromRoute]Guid userId, [FromHeader] string token, [FromHeader] string email, [FromHeader] Guid requestUserId){
        User userAuthenticated = await _authenticationService.Authenticate(email, requestUserId, token);

        if(userAuthenticated == null){
            return StatusCode(401, "Unauthorized");
        }

        if((userAuthenticated.Id == userId && !_userAuthorizationService.AuthorizeViewUsers(userAuthenticated, userId)) || (userAuthenticated.Id != userId && !_userAuthorizationService.AuthorizeViewUsers(userAuthenticated, null))){
            return StatusCode(403, "Forbidden");
        }
        try{
            User user = await _userRepository.GetById(userId);
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
    public async Task<IActionResult> Create([FromForm] CreateUserDto dto, [FromHeader] string token, [FromHeader] string email, [FromHeader] Guid userId)
    {
        try
        {
            // Authenticate and authorize user
            User user = await _authenticationService.Authenticate(email, userId, token);
            if (user == null)
            {
                return StatusCode(401, "Unauthorized");
            }

            if (!_userAuthorizationService.AuthorizeCreateUsers(user))
            {
                return StatusCode(403, "Forbidden");
            }

            // Check if user exists
            User userExists = await _userRepository.GetByEmail(dto.Email);
            if (userExists != null)
            {
                return StatusCode(409, "Email already exists");
            }

            // Handle Profile Picture upload
            string? profilePicturePath = null;
            if (dto.ProfilePicture != null)
            {
                // Save profile picture logic
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile_pictures");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                var fileExtension = Path.GetExtension(dto.ProfilePicture.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsDir, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ProfilePicture.CopyToAsync(fileStream);
                }

                profilePicturePath = $"/profile_pictures/{uniqueFileName}";
            }

            // Create new user with profile picture path
            User newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = _cryptographyServices.Encrypt(dto.Password),
                Role = dto.Role,
                Status = dto.Status ?? AccountStatus.ACTIVE,
                ProfilePicture = profilePicturePath,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            newUser.AllowPermitionsByRole();

            await _userRepository.Create(newUser);

            return Ok(new
            {
                newUser.Id,
                newUser.Name,
                newUser.Email,
                newUser.ProfilePicture, // The relative URL to the profile picture
                newUser.Role,
                newUser.Status,
                newUser.CreatedAt,
                newUser.UpdatedAt
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }


    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register([FromForm] CreateUserDto dto){
        try{
            User userExists = await _userRepository.GetByEmail(dto.Email);

            if(userExists != null){
                return StatusCode(409, "Email already exists");
            }

            User user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = _cryptographyServices.Encrypt(dto.Password),
                Role = UserRole.ORDINARY,
                Status = AccountStatus.ACTIVE,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            user.AllowPermitionsByRole();

            await _userRepository.Create(user);

            return StatusCode(201, "User created");
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> Login([FromForm] LoginDto dto){
        try{
            User? user = await _authenticationService.Login(dto.Email, dto.Password);
            if(user == null){
                return StatusCode(404, "email or password incorrect");
            }
            string token = _cryptographyServices.GenerateToken(user.Id, user.Email);
            return StatusCode(200, new { token, userId = user.Id, email = user.Email });
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{userId}", Name = "UpdateUser")]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromForm] UpdateUserDto dto, [FromHeader] string token, [FromHeader] string email, [FromHeader] Guid userRequestId){

        User userAuthenticated = await _authenticationService.Authenticate(email, userRequestId, token);

        if(userAuthenticated == null){
            return StatusCode(401, "Unauthorized");
        }

        if(userAuthenticated.Id == userId && !_userAuthorizationService.AuthorizeUpdateUsers(userAuthenticated, userId)){
            return StatusCode(403, "Forbidden");
        }else if(!_userAuthorizationService.AuthorizeUpdateUsers(userAuthenticated, null)){
            return StatusCode(403, "Forbidden");
        }

        User user;
        try{
            user = await _userRepository.GetById(userId);

            if (user == null)
            {
                return StatusCode(404, "User not found");
            }

        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
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

        if (dto.ProfilePicture != null){
            // Save profile picture logic
            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile_pictures");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            var fileExtension = Path.GetExtension(dto.ProfilePicture.FileName);
            var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadsDir, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ProfilePicture.CopyToAsync(fileStream);
            }

            user.ProfilePicture = $"/profile_pictures/{uniqueFileName}";
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

    [HttpDelete("{userId}", Name = "DeleteUser")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId, [FromHeader] string token, [FromHeader] string email, [FromHeader] Guid requestedId){
        User userAuthenticated = await _authenticationService.Authenticate(email, requestedId, token);

        if(userAuthenticated == null){
            return StatusCode(401, "Unauthorized");
        }

        if((userAuthenticated.Id == userId && !_userAuthorizationService.AuthorizeDeleteUsers(userAuthenticated, userId)) || (userAuthenticated.Id != userId && !_userAuthorizationService.AuthorizeDeleteUsers(userAuthenticated, null))){
            return StatusCode(403, "Forbidden");
        }

        try{
            User user = await _userRepository.GetById(userId);
            if(user == null){
                return StatusCode(404, "User not found");
            }

        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }

        try{
            await _userRepository.Delete(userId);
            return StatusCode(204, "User deleted");
        }catch(Exception e){
            _logger.LogError(e.Message);
            return StatusCode(500, e.Message);
        }
    }
}