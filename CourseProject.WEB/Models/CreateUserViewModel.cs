using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models {
    public class CreateUserViewModel {

        [Display(Name = "Login")]
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
