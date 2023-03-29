using FAQ.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace FAQ.DAL.BaseModels
{
    public abstract class BaseUserInheritancable : BaseInheritancable
    {
        public User? User { get; set; }
    }
}