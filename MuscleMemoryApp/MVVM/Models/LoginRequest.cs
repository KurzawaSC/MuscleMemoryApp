namespace MuscleMemoryApp.MVVM.Models;

public class LoginRequest(string _email, string _password)
{
    public string email { get; set; } = _email;
    public string password { get; set; } = _password;
}
