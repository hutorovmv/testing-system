using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using AutoMapper;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.BLL.DTO;
using TestingSystem.DAL.Interfaces;
using TestingSystem.Models.Entities;

namespace TestingSystem.BLL.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserManagementService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await _uow.UserManager.FindByNameAsync(userDto.UserName);
            if (user != null)
                return new OperationDetails(false, "User with such username already exists");

            user = new ApplicationUser();
            user = _mapper.Map<UserDTO, ApplicationUser>(userDto, user);

            IdentityResult userCreationResult = await _uow.UserManager.CreateAsync(user, userDto.Password);
            if (userCreationResult.Errors.Count() > 0)
                return new OperationDetails(false, userCreationResult.Errors.FirstOrDefault());
            
            var addingToRoleResult = await _uow.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            if (addingToRoleResult.Errors.Count() > 0)
                return new OperationDetails(false, addingToRoleResult.Errors.FirstOrDefault());

            try
            {
                UserProfile userProfile = _mapper.Map<UserDTO, UserProfile>(userDto);
                userProfile.Id = user.Id;

                _uow.UserProfileRepository.Create(userProfile);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }         

            return new OperationDetails(true, "User is registered successfuly");
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;

            ApplicationUser user = await _uow.UserManager.FindAsync(userDto.UserName, userDto.Password);
            if (user != null)
                claim = await _uow.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        public async Task<OperationDetails> UpdateUserEmail(string userId, string newEmail)
        {
            ApplicationUser user = await _uow.UserManager.FindByIdAsync(userId);
            if (user.Email != newEmail)
            {
                user.Email = newEmail;
                user.EmailConfirmed = false;

                var updatingResult = await _uow.UserManager.UpdateAsync(user);
                if (updatingResult.Errors.Count() > 0)
                    return new OperationDetails(false, updatingResult.Errors.FirstOrDefault());
            }
            else
                return new OperationDetails(false, "Email is equal to current email");

            return new OperationDetails(true, "Email is successfuly updated");
        }

        public async Task<OperationDetails> UpdateUserPassword(string userId, string oldPassword, string newPassword)
        {
            var passwordChangeResult = await _uow.UserManager.ChangePasswordAsync(userId, oldPassword, newPassword);
            if (passwordChangeResult.Errors.Count() > 0)
                return new OperationDetails(false, passwordChangeResult.Errors.FirstOrDefault());

            return new OperationDetails(true, "Password was successfuly changed");
        }

        public async Task<OperationDetails> UpdateUserProfileInfo(string userId, UserDTO userDto)
        {
            try
            {
                UserProfile userProfile = await _uow.UserProfileRepository.GetById(userId);
                
                userProfile = _mapper.Map<UserDTO, UserProfile>(userDto, userProfile);
                
                _uow.UserProfileRepository.Update(userProfile);
                await _uow.SaveAsync();
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "Profile was successfuly updated");
        }

        public async Task<OperationDetails> GiveAdminStatus(string userId)
        {
            ApplicationUser user = await _uow.UserManager.FindByIdAsync(userId);
            IList<string> userRoles = await _uow.UserManager.GetRolesAsync(userId);
            bool IsAdmin = userRoles.Contains("admin");
            if (!IsAdmin)
            {
                await _uow.UserManager.RemoveFromRolesAsync(userId, userRoles.ToArray());
                await _uow.UserManager.AddToRoleAsync(userId, "admin");

                var updatingResult = await _uow.UserManager.UpdateAsync(user);
                if (updatingResult.Errors.Count() > 0)
                    return new OperationDetails(false, updatingResult.Errors.FirstOrDefault());
            }
            else
                return new OperationDetails(false, "User already have admin status");

            return new OperationDetails(true, "Admin status was successfuly given to user");
        }

        public async Task<OperationDetails> DeleteUser(string userId)
        {
            try
            {
                UserProfile userProfile = await _uow.UserProfileRepository.GetById(userId);
                _uow.UserProfileRepository.Delete(userProfile);
                await _uow.SaveAsync();

                ApplicationUser user = await _uow.UserManager.FindByIdAsync(userId);
                await _uow.UserManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message);
            }

            return new OperationDetails(true, "User was successfuly deleted");
        }

        public async Task<OperationDetails> SetInitialData(UserDTO adminDTO, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _uow.RoleManager.FindByNameAsync(roleName);

                if (role != null)
                    continue;

                role = new ApplicationRole { Name = roleName };

                var roleCreationResult = await _uow.RoleManager.CreateAsync(role);
                if (roleCreationResult.Errors.Count() > 0)
                    return new OperationDetails(false, roleCreationResult.Errors.FirstOrDefault());
            }

            await Create(adminDTO);
            return new OperationDetails(true, "Initial data was successfuly created");
        }

        public void Dispose() => _uow.Dispose();
    }
}