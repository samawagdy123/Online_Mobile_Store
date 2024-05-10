using System.ComponentModel.DataAnnotations;

namespace Online_mobile_store.Models
{
    public class UserLoginView
    {

        [Required(ErrorMessage = "Please enter an email address")]
       
        public string ? Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a password")]

        public string ? Password { get; set; }
        public User? user {  get; set; }= new User();
    }
}
