namespace School.Views;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using School.Models;

public partial class StudentsPage : ContentPage
{
	public ObservableCollection<Students> studentsList {get; set;} = new ObservableCollection<Students>();
	public StudentsPage()
	{
		InitializeComponent();

		BindingContext = this;

		LoadStudentsList();
	}

	public void LoadStudentsList()
	{
		studentsList.Clear();

		foreach (var ens in Students.LoadAll())
		{
			studentsList.Add(ens);
		}
	}

	private void OnAddStudentClicked(object sender, EventArgs e){
		string firstname = FirstnameEntry.Text;
		string lastname = LastnameEntry.Text; 

		Students newstudent = new Students(firstname,lastname);
		newstudent.Save();
		studentsList.Add(newstudent);

		DisplayAlert("Succes","Student was successfully added !","ok");

		FirstnameEntry.Text = string.Empty;
		LastnameEntry.Text = string.Empty;
	}
}