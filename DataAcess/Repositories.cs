using System;
using DataAcess.Models;
using DataAcess.Models.Conference;
using DataAcess.Models.ConferenceMember;

namespace DataAcess
{
    public class Repositories : IDisposable
    {
        private readonly ApplicationContext _context = new ApplicationContext();

        private GenericRepository<UserProfile> _userProfileRepository;
        private GenericRepository<ConferenceModel> _conferenceRepository;
        private GenericRepository<ConferenceCategoryModel> _conferenceCategoryRepository;
        private GenericRepository<MemberModel> _membersRepository;
        private GenericRepository<MemberWorkModel> _membersWorksRepository;

        public GenericRepository<MemberWorkModel> MembersWorksRepository
        {
            get { return _membersWorksRepository ?? (_membersWorksRepository = new GenericRepository<MemberWorkModel>(_context)); }
        }

        public GenericRepository<MemberModel> MembersRepository
        {
            get { return _membersRepository ?? (_membersRepository = new GenericRepository<MemberModel>(_context)); }
        }

        public GenericRepository<UserProfile> UserProfileRepository
        {
            get { return _userProfileRepository ?? (_userProfileRepository = new GenericRepository<UserProfile>(_context)); }
        }

        public GenericRepository<ConferenceModel> ConferenceRepository
        {
            get { return _conferenceRepository ?? (_conferenceRepository = new GenericRepository<ConferenceModel>(_context)); }
        }

        public GenericRepository<ConferenceCategoryModel> ConferenceCategoryRepository
        {
            get { return _conferenceCategoryRepository ?? (_conferenceCategoryRepository = new GenericRepository<ConferenceCategoryModel>(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}