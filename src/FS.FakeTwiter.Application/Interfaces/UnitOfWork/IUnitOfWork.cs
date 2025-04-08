using FS.FakeTwitter.Application.Interfaces;
using System.Threading.Tasks;
namespace FS.FakeTwitter.Domain.Interfaces;

public interface IUnitOfWork
{
    ITweetRepository Tweets { get; }
    IFollowRepository Follows { get; }
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync();
}
