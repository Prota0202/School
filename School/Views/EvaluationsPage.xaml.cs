using System.Diagnostics;
using System.Collections.ObjectModel;
using School.Models;
using System.Linq;
namespace School.Views;

public partial class EvaluationsPage : ContentPage
{
    public ObservableCollection<Students> studentsList { get; set; } = new ObservableCollection<Students>();
    public ObservableCollection<Models.Activity> ActivityList { get; set; } = new ObservableCollection<Models.Activity>();

    private StudentsPage studentsPage = new StudentsPage();
    private ActivitesPage ActivityPage = new ActivitesPage();
    private TeachersPage teachersPage = new TeachersPage();

    public EvaluationsPage()
    {
        InitializeComponent();
        BindingContext = this;
        this.teachersPage = teachersPage;

        foreach (var elem in Students.LoadAll())
        {
            studentsList.Add(elem);
        }

        foreach (var activity in Models.Activity.LoadAll(teachersPage.teachersList))
        {
            ActivityList.Add(activity);
        }
    }

    // ... [OnAppearing and OnDisappearing Methods] ...

    private void OnAddCoteClicked(object sender, EventArgs e)
    {
        if (StudentsPicker.SelectedItem is null || ActivityPicker.SelectedItem is null || CotePicker.SelectedItem is null)
        {
            DisplayAlert("Error", "Please select all fields", "OK");
            return;
        }

        string studentSelected = StudentsPicker.SelectedItem.ToString();
        string activitySelected = ActivityPicker.SelectedItem.ToString();
        string coteSelected = CotePicker.SelectedItem.ToString();

        var activity2link = ActivityList.FirstOrDefault(a => a.Code == activitySelected);
        if (activity2link is null)
        {
            DisplayAlert("Error", "Activity not found", "OK");
            return;
        }

        if (!int.TryParse(coteSelected, out int parsedCote))
        {
            DisplayAlert("Error", "Invalid cote format", "OK");
            return;
        }

        Cotes cotesAdded = new Cotes(activity2link);
        cotesAdded.SetNote(parsedCote);

        var student2link = studentsList.FirstOrDefault(s => s.DisplayName == studentSelected);
        if (student2link is null)
        {
            DisplayAlert("Error", "Student not found", "OK");
            return;
        }

        student2link.Add(cotesAdded);
        student2link.Save();
        DisplayAlert("Success", "Cote added successfully", "OK");
    }

    // ... [OnAddAppreciationClicked Method with similar checks and improvements] ...
}
