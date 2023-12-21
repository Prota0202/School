namespace School.Views;
using School.Models;
using System.Collections.ObjectModel;
using System;
using Microsoft.Maui.Controls;

public partial class TeachersPage : ContentPage
{
	public ObservableCollection<Teachers> teachersList {get; set;} = new ObservableCollection<Teachers>();

	public TeachersPage()
	{
		InitializeComponent();

		BindingContext = this;

		LoadTeachersList();
		DisplayTotalSalary();
	}
	public void LoadTeachersList()
	{
		teachersList.Clear();

		foreach (var ens in Teachers.LoadAll())
		{
			teachersList.Add(ens);
		}
	}

	public string TotalSalary(){
		int totalsalary = 0;
		if(teachersList == null){
			return "0 de cout total";
		}
		foreach(var enseignant in teachersList){
			totalsalary += enseignant.Salary;
		}
		return String.Format("Le cout mensuelle est de {0}€ \nLe cout annuel est de {1}€",totalsalary,totalsalary*12);
	}

	private void DisplayTotalSalary(){
		SalaryTotalLabel.Text = TotalSalary();
	}
	private void OnAddTeacherClicked(object sender, EventArgs e)
        {
            string firstname = FirstnameEntry.Text;
            string lastname = LastnameEntry.Text;
			Console.WriteLine("Hi");

            if (int.TryParse(SalaryEntry.Text, out int salary))
            {
                Teachers enseignant = new Teachers(firstname, lastname, salary);
				teachersList.Add(enseignant);
				enseignant.Save();
				Console.WriteLine("Teacher added: " + enseignant.DisplayName);
        		Console.WriteLine("Total teachers in list: " + teachersList.Count);

				DisplayAlert("Success", "Teacher added successfully. It worked!", "OK");

				FirstnameEntry.Text = string.Empty;
				LastnameEntry.Text = string.Empty;
        		SalaryEntry.Text = string.Empty;
				DisplayTotalSalary();
            }
            else
            {
                DisplayAlert("Error", "Invalid salary input", "OK");
            }
        }
}