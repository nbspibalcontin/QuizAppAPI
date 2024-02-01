using System.ComponentModel.DataAnnotations;

namespace QuizApp.Request.User
{
    public class RoleRequest
    {
        [Required(ErrorMessage = "RoleName is required")]
        [StringLength(256, ErrorMessage = "RoleName length cannot exceed 256 characters")]
        public string RoleName { get; set; }
    }
}
