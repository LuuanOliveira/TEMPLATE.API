using Template.Infrastructure.Data.Context;
using Template.Infrastructure.Data.Extensions;
using Template.Shared.Kernel.Data;
using Template.Shared.Kernel.Mediator;
using System.Threading.Tasks;

namespace Template.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMediatorHandler _mediatorHandler;

        public UnitOfWork(DataContext dataContext, IMediatorHandler mediatorHandler)
        {
            _context = dataContext;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> CommitAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;

            if (success)
                await _mediatorHandler.PublishEvents(_context);

            return success;
        }
    }
}
