namespace MuscleMemoryApp.MVVM.Models;

public class User
{
    public string UserName { get; set; } = default!;
    public DateOnly? DateOfBirth { get; set; } = default(DateOnly)!;
    public string? Nationality { get; set; } = default!;
    public double? Weight { get; set; } = default!;
    public double? Height { get; set; } = default!;
}
