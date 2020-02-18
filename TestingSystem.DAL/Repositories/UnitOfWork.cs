using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using TestingSystem.DAL.Interfaces;
using TestingSystem.DAL.Context;
using TestingSystem.DAL.Identity;
using TestingSystem.DAL.Repositories;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public ApplicationUserManager UserManager { get; }
        public ApplicationRoleManager RoleManager { get; }
        public IUserProfileRepository UserProfileRepository { get; }
        public ITestRepository TestRepository { get; }
        public IQuestionRepository QuestionRepository { get; }
        public IAnswerRepository AnswerRepository { get; }
        public ITestResultRepository TestResultRepository { get; }
        public ITestAnswerRepository TestAnswerRepository { get; }

        public UnitOfWork(ApplicationContext context,
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            IUserProfileRepository userProfileRepository,
            ITestRepository testRepository,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            ITestResultRepository testResultRepository,
            ITestAnswerRepository testAnswerRepository)
        {
            _context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            UserProfileRepository = userProfileRepository;
            TestRepository = testRepository;
            QuestionRepository = questionRepository;
            AnswerRepository = answerRepository;
            TestResultRepository = testResultRepository;
            TestAnswerRepository = testAnswerRepository;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                UserManager.Dispose();
                RoleManager.Dispose();
                UserProfileRepository.Dispose();
                TestRepository.Dispose();
                QuestionRepository.Dispose();
            }
            disposed = true;
        }
    }
}
