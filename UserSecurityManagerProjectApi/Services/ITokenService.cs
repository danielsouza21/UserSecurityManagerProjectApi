using UserSecurityManagerProjectApi.Models;

namespace UserSecurityManagerProjectApi.Services
{
    public interface ITokenService
    {
        string GenerateSetPasswordToken(ApplicationUser user);
        string GenerateLoginToken(ApplicationUser user);
    }
}
