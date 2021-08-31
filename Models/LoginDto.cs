using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cookBook.Models
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}