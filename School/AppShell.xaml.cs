using School.Views;

namespace School;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Console.WriteLine("STARTING....");
		Directory.CreateDirectory(Config.RootDir);
	}
}
