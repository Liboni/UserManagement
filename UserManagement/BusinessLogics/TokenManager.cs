
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Linq;

    using UserManagement.Data;

    public class TokenManager
    {
        private readonly ApplicationDbContext context;

        public TokenManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public string SaveToken(Token token)
        {
            string id = Guid.NewGuid().ToString();
            context.Tokens.Add(new Token
                    {
                        ExpiryDate = token.ExpiryDate,
                        Id = id,
                        UserToken = token.UserToken,
                        UserId = token.User.Id,
                        DateCreated = DateTime.Now
                    });
            context.SaveChanges();
            return id;
        }

        public Token GetTokenById(string id)
        {
            return String.IsNullOrEmpty(id)
                       ? new Token()
                       : context.Tokens.Where(a => a.Id.Equals(id))
                           .Select(
                               a => new Token
                                        {
                                            User = a.User,
                                            UserToken = a.UserToken,
                                            ExpiryDate = a.ExpiryDate,
                                            Id = a.Id,
                                            UserId = a.UserId
                                        })
                           .FirstOrDefault();
        }
    }
}
