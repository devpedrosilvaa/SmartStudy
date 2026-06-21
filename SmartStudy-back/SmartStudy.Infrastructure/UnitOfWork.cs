using SmartStudy.Application.Interfaces.Repositories;
using SmartStudy.Infrastructure.Persistence;

namespace SmartStudy.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContextDb _context;

        public UnitOfWork(ApplicationContextDb context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
