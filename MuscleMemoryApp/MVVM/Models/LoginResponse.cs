namespace MuscleMemoryApp.MVVM.Models;

public class LoginResponse
{
    public string tokenType { get; set; } = default!;
    public string accessToken { get; set; } = default!;
    public int expiresIn { get; set; } = default!;
    public string refreshToken { get; set; } = default!;
}
