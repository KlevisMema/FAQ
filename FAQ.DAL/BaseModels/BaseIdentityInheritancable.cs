using Microsoft.AspNetCore.Identity;

namespace FAQ.DAL.BaseModels
{
    public abstract class BaseIdentityInheritancable : IdentityUser
    {
        public DateTime? Created { get; set; }
        public DateTime? Edited { get; set; }
        public DateTime? Deleted { get; set; }

        public bool IsAdmin { get; set; }
    }
}