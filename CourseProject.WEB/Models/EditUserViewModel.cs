using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models {
    public class EditUserViewModel {
        public string Id { get; set; }

        [Display(Name = "Login")]
        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
