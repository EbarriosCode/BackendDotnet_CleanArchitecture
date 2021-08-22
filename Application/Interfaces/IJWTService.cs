using Countries.Common.Classes;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        Task<UserToken> BuildToken(UserInfo userInfo);
        JwtSecurityToken DecodeJwt(string jwt);
    }
}
