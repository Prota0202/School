using System.Collections.ObjectModel;
using School.Models;
using System.Linq;

namespace School.Views;

public partial class BulletinPage : ContentPage
{
    public ObservableCollection<Students> studentsList { get; set; } = new ObservableCollection<Students>();
    private StudentsPage studentsPage = new StudentsPage();

    public BulletinPage()
    {
        InitializeComponent();
        BindingContext = this;
        
        foreach (var student in Students.LoadAll())
        {
            studentsList.Add(student);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        studentsPage.LoadStudentsList();

        StudentPicker.ItemsSource = studentsList.Select(student => student.DisplayName).ToList();
        StudentPicker.IsVisible = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        studentsPage.LoadStudentsList();
    }

    private void OnAddBulletinClicked(object sender, EventArgs e)
    {
        if (StudentPicker.SelectedItem is null)
        {
            DisplayAlert("Error", "Please select a student", "OK");
            return;
        }

        string selectedStudent = StudentPicker.SelectedItem.ToString();
        var studentToLink = studentsList.FirstOrDefault(student => student.DisplayName == selectedStudent);

        if (studentToLink is null)
        {
            DisplayAlert("Error", "Student not found", "OK");
            return;
        }

        string selectedBulletin = studentToLink.Bulletin();
        Console.WriteLine("Bulletin: " + selectedBulletin);
        // Handle displaying the bulletin as required
    }
}
