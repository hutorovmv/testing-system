using System;
using System.Threading.Tasks;
using TestingSystem.DAL.Interfaces;
using TestingSystem.DAL.Identity;
using TestingSystem.DAL.Repositories;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IUserProfileRepository UserProfileRepository { get; }
        ITestRepository TestRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        ITestResultRepository TestResultRepository { get; }
        ITestAnswerRepository TestAnswerRepository { get; }

        Task<int> SaveAsync();
    }
}
