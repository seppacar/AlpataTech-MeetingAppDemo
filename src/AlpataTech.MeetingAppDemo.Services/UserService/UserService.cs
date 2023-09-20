﻿using AlpataTech.MeetingAppDemo.DAL.Repository;
using AlpataTech.MeetingAppDemo.Entities;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Services.Common.FileStorageService;
using AutoMapper;
using System.Linq.Expressions;

namespace AlpataTech.MeetingAppDemo.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public UserService(UserRepository userRepository, IMapper mapper, IFileStorageService fileStorageService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
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
                string fileName = "pfp_" + Guid.NewGuid().ToString() + profilePictureFile.FileExtension;
                //string filePath = await _fileStorageService.UploadFileAsync(profilePictureBytes, fileName);

                // Call the service to upload the file
                _fileStorageService.UploadFileAsync(profilePictureFile.FileData, fileName);

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

            // Map the created user to UserDto and return
            return _mapper.Map<UserDto>(user);
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
