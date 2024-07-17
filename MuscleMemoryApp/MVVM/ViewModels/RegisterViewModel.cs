namespace MuscleMemoryApp.MVVM.ViewModels;

public class RegisterViewModel
{
    public List<string> sexes { get; set; }

    public RegisterViewModel()
    {
        sexes = new List<string>()
        {
            "male", "female"
        };
    }
}
