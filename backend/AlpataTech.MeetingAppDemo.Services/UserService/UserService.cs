using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Services.Common.EmailService;
using AlpataTech.MeetingAppDemo.Services.Common.FileStorageService;
using AutoMapper;
using System.Linq.Expressions;

namespace AlpataTech.MeetingAppDemo.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        private readonly IEmailService _emailService;
        private readonly IFileStorageService _fileStorageService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, RoleRepository roleRepository, IFileStorageService fileStorageService, IEmailService emailService, IImageService imageService, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _fileStorageService = fileStorageService;
            _emailService = emailService;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto, FileUploadModel? profilePictureFile)
        {
            // Map createUserDto type to User type
            var user = _mapper.Map<User>(createUserDto);

            // Hash the password using BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
            user.PasswordHash = passwordHash;

            // Save profile image and set profile image path
            if (profilePictureFile.FileData != null)
            {
                // Adjust the directory and file name as needed
                string fileName = "pfp_" + Guid.NewGuid().ToString() + ".webp";

                // Convert and compress image file
                var compressedImage = _imageService.CompressAndConvertWebP(profilePictureFile.FileData);

                // Call the service to upload the file
                _fileStorageService.UploadFileAsync(compressedImage, fileName);

                // Store JUST filename since we are using one folder for all files for now
                user.ProfileImage = fileName;
            }
            else
            {
                user.ProfileImage = "Default Profile Image Path";
            }


            // Add the user asynchronously
            await _userRepository.AddAsync(user);
            // Save changes to the database
            await _userRepository.SaveChangesAsync();

            // Get all roles from the database
            var roles = await _roleRepository.GetAllAsync();
            // Check if role already exists
            var defaultRole = roles.FirstOrDefault(role => role.Name.ToUpperInvariant() == "USER");

            if (defaultRole == null)
            {
                throw new Exception("User role is not in roles table");
            }
            // Add default UserRole for the created user
            await _roleRepository.AddUserRoleAsync(user.Id, defaultRole.Id);
            await _roleRepository.SaveChangesAsync();

            // Map the created user to UserDto and return
            var createdUser = await _userRepository.GetUserWithNavigationsAsync(user.Id);

            // Map User createdUser to UserDto userDto
            var userDto = _mapper.Map<UserDto>(createdUser);

            // Send welcome email to created user
            await _emailService.SendWelcomeEmailAsync(createdUser.Email, userDto);

            return userDto;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            Expression<Func<User, bool>> predicate = user => user.Email == email;
            // Use the FindAsync method with the predicate
            var users = await _userRepository.FindAsync(predicate);

            // Retrieve the first user that matches the predicate (or null if not found)
            var user = users.FirstOrDefault();

            return user;
        }

        public async Task<List<string>> GetUserRolesByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }
            var userDto = _mapper.Map<UserDto>(user);

            var roles = userDto.Roles;

            return roles;
        }

        public async Task<IEnumerable<UserDto>> FindUsersAsync(Expression<Func<User, bool>> predicate)
        {
            var users = await _userRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(id);

            if (userToUpdate != null)
            {
                // Handle the case where the user doesn't exist
                return null;
            }

            // Update the user properties with the values from updateUserDto
            _mapper.Map(updateUserDto, userToUpdate);

            // TODO: If there's a new profile picture, update it

            // Update the user in the repository
            _userRepository.Update(userToUpdate);
            await _userRepository.SaveChangesAsync();

            // Map and return the updated user
            return _mapper.Map<UserDto>(userToUpdate);
        }

        public async Task DeleteUserAsync(int id)
        {
            _userRepository.Remove(await _userRepository.GetByIdAsync(id));
            await _userRepository.SaveChangesAsync();
        }
        public async Task AddUserRoleAsync(int userId, string roleName)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Normalize the role name
            string normalizedRoleName = roleName.ToUpperInvariant();
            // Get all roles from the database
            var roles = await _roleRepository.GetAllAsync();
            // Check if role already exists
            var existingRole = roles.FirstOrDefault(role => role.Name.ToUpperInvariant() == normalizedRoleName);

            // Create new UserRole for User if role exists, else create the role and assign it
            if (existingRole == null)
            {
                user.Roles.Add(new UserRole
                {
                    RoleId = existingRole.Id
                });
            }
            else
            {
                // Role doesn't exist, create it and assign to the user
                var newRole = new Role
                {
                    Name = normalizedRoleName
                };

                // Add the role
                await _roleRepository.AddAsync(newRole);
                await _roleRepository.SaveChangesAsync();  // Save changes here

                // Assign the new role to the user
                user.Roles.Add(new UserRole
                {
                    RoleId = newRole.Id
                });
            }

            // Save changes (including user and user role associations)
            await _userRepository.SaveChangesAsync();
        }


        public async Task<byte[]?> GetProfilePicture(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                if (user.ProfileImage != null)
                {
                    Console.WriteLine("USER PROFİLE İMAGE İS" + user.ProfileImage);
                    return await _fileStorageService.GetFileAsync(user.ProfileImage);
                }
                else
                {
                    // TODO: Return default profile picture
                    return await _fileStorageService.GetFileAsync(null);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
