namespace UserService.Model.DTOs
{
    public class ServiceResponse
    {
        public record GeneralResponse (bool flag, string message);
        public record LoginResponse (bool flag, string Token, string message);
    }
}
