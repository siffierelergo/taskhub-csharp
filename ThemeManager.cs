public enum VisualTheme
{
    Light,
    Dark,
    SystemDefault
}

public class ThemeSettings
{
    public VisualTheme CurrentTheme { get; private set; }

    public void ApplyTheme(VisualTheme theme)
    {
        CurrentTheme = theme;
        // Aici vei adăuga logica pentru schimbarea resurselor de culoare (XAML/WinForms)
        Console.WriteLine($"Tema aplicată: {theme}");
    }
}