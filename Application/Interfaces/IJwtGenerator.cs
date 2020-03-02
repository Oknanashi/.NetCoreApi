using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppEmployee user);
    }
}