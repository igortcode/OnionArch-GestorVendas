using Microsoft.AspNetCore.Identity;

namespace Gestao.Data.Identity.Entities
{
    public class User : IdentityUser
    {
        public User(string userName, string email) : base(userName)  
        {
            Email = email;
            EmailConfirmed = true;
        }

        public void Atualizar(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }
    }
}
