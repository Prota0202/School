namespace School;

public class Config
{
    private Config() {}

    public static string RootDir {get;} = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"Stock");

}
