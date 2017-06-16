using System.ComponentModel.DataAnnotations;

namespace WallE.Platform.Models
{
    public class LogOnModel
    {
        [Required(ErrorMessage = "* 必填")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "* 必填")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "密码输入错误！", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "记住密码 ?")]
        public bool RememberMe { get; set; }

        public string Tip { get; set; }
    }

}
