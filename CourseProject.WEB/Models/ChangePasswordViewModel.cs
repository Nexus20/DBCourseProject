﻿using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models {
    public class ChangePasswordViewModel {
        public string Id { get; set; }

        public string UserName { get; set; }

        [Display(Name = "New password")]
        public string NewPassword { get; set; }
    }
}
