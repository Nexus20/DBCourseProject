using CourseProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CourseProject.DAL.Identity;

public class ApplicationUserManager : UserManager<User> {
    public ApplicationUserManager(IUserStore<User> store,
        IOptions<IdentityOptions> options,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators, 
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer lookupNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<User>> logger) 
        : base(store, options, passwordHasher, userValidators, passwordValidators, lookupNormalizer, errors, services, logger) {
    }
}