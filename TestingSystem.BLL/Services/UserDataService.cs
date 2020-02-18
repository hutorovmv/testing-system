using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using AutoMapper;
using TestingSystem.BLL.Interfaces;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.BLL.DTO;
using TestingSystem.DAL.Interfaces;
using TestingSystem.DAL.Extensions;
using TestingSystem.Models.Entities;
using TestingSystem.BLL.Utils;

namespace TestingSystem.BLL.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserDataService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserInfo(string userId)
        {
            ApplicationUser user = await _uow.UserManager.FindByIdAsync(userId);
            UserProfile profile = await _uow.UserProfileRepository.GetById(user.Id);

            if (user == null || profile == null)
                return null;

            UserDTO accountData = _mapper.Map<ApplicationUser, UserDTO>(user);
            accountData.Role = user != null ? _uow.UserManager.GetRoles(user.Id).FirstOrDefault() : null;
            accountData = _mapper.Map<UserProfile, UserDTO>(profile, accountData);

            return accountData;
        }

        public async Task<PagedList<UserDTO>> UserProfilesWithFullName(string name, int pageSize, int pageIndex)
        {
            PagedList<UserProfile> profiles = await _uow.UserProfileRepository.GetAllWithFullName(name, pageSize, pageIndex);
            PagedList<UserDTO> userDtosPagedList = profiles.ConvertPagedList<UserProfile, UserDTO>(_mapper);
            return userDtosPagedList;
        }

        public async Task<PagedList<UserDTO>> UserProfilesWithProperties(string firstName, string lastName,
            string contactEmail, DateTime? birthFrom, DateTime? birthTo,
            int pageSize, int pageIndex)
        {
            PagedList<UserProfile> profiles = await _uow.UserProfileRepository.GetAllWithProperties(firstName,
                lastName, contactEmail, birthFrom, birthTo, pageSize, pageIndex);
            PagedList<UserDTO> userDtosPagedList = profiles.ConvertPagedList<UserProfile, UserDTO>(_mapper);
            return userDtosPagedList;
        }

        public async Task<PagedList<UserDTO>> UserProfilesWithPropertiesExtended(string firstName, string lastName,
            string contactEmail, DateTime? birthFrom, DateTime? birthTo,
            int pageSize, int pageIndex)
        {
            IEnumerable<string> ids = await _uow.UserProfileRepository.GetAllWithPropertiesIds(firstName, lastName, contactEmail, birthFrom, birthTo);
            List<UserDTO> userDtos = new List<UserDTO>();
            foreach (var id in ids)
            {
                userDtos.Add(await GetUserInfo(id));
            }
            return userDtos.ToPagedList(pageSize, pageIndex);
        }

        public async Task<string> GetIdByUserName(string userName)
        {
            ApplicationUser user = await _uow.UserManager.FindByNameAsync(userName);
            return user.Id;
        }

        public async Task<OperationDetails> IsUserEmailConfirmed(string userName)
        {
            ApplicationUser user = await _uow.UserManager.FindByNameAsync(userName);

            if (await _uow.UserManager.IsEmailConfirmedAsync(user.Id))
                return new OperationDetails(true, "User has confirmed his email");

            return new OperationDetails(false, "User has not confirmed his email");
        }

        public void Dispose() => _uow.Dispose();
    }
}
