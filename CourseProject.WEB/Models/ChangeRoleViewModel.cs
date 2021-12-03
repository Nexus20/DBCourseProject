using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models {
    public class ChangeRoleViewModel {
        public ChangeRoleViewModel() {
            AllRoles = new List<RoleViewModel>();
            UserRoles = new List<string>();
        }

        public string UserId { get; set; }

        [Display(Name = "Login")]
        public string UserName { get; set; }

        public List<RoleViewModel> AllRoles { get; set; }

        public IList<string> UserRoles { get; set; }
    }
}
