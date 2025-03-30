namespace UserService.Model.DTOs
{
    public class LoginResponse
    {
        public ServiceResponse.LoginResponse response { get; set; }
        public User user { get; set; }
    }
}
