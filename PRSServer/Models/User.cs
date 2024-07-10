using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSServer.Models
{

    [Index(nameof(Username), IsUnique = true)]

    public class User
    {

        public int Id { get; set; }

        [StringLength(30)]
        public string Username { get; set; } = string.Empty;

        [StringLength(30)]
        public string Password { get; set; } = string.Empty;

        [StringLength(30)]
        public string Firstname{ get; set; } = string.Empty;

        [StringLength(30)]
        public string Lastname { get; set; } = string.Empty;

        [StringLength(12)]
        public string? Phone { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Email { get; set; } = string.Empty;

        [Column(TypeName = "bit")]
        public bool IsReviewer { get; set; }

        [Column(TypeName = "bit")]
        public bool IsAdmin { get; set; }
    }
}
