using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels.Account
{
    public class LoginVm
    {
        public string EmailOrUserName {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Reminder { get; set; }

    }
}
