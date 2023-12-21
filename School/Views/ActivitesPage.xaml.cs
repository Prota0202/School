namespace School.Views;
using System.Linq;
using School.Models;
using System.Collections.ObjectModel;

public partial class ActivitesPage : ContentPage
{
    private TeachersPage teachersPage;

    public ObservableCollection<Teachers> teachersList { get; set; }
    public ObservableCollection<Activity> activityList { get; set; } = new ObservableCollection<Activity>();

    public ActivitesPage() : this(new TeachersPage()) { }

    public ActivitesPage(TeachersPage teachersPage)
    {
        InitializeComponent();
        TeachersPage = teachersPage;
        teachersList = TeachersPage.teachersList;

        BindingContext = this;
        LoadActivityList();
    }

    private void LoadActivityList()
    {
        activityList.Clear();
        foreach (var activity in Activity.LoadAll(teachersList))
        {
            activityList.Add(activity);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        TeachersPage.LoadTeacherList();
        if (TeacherPicker != null)
        {
            TeacherPicker.ItemsSource = teachersList.Select(teacher => teacher.DisplayName).ToList();
            TeacherPicker.IsVisible = true;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        TeachersPage.LoadTeacherList();
    }

    private void OnAddActivityClicked(object sender, EventArgs e)
    {
        string activityName = ActivityNameEntry.Text;
        string CodeName = ActivityCodeEntry.Text;
        int ects;

        if (int.TryParse(ActivityEctsEntry.Text, out ects) && TeacherPicker.SelectedItem != null)
        {
            string selectedTeacherDisplayName = TeacherPicker.SelectedItem.ToString();
            Teachers teacher2link = teachersList.FirstOrDefault(teacher => teacher.DisplayName == selectedTeacherDisplayName);

            if (teacher2link != null)
            {
                Activity newactivity = new Activity(activityName, CodeName, ects, teacher2link);
                newactivity.Save();
                activityList.Add(newactivity);

                DisplayAlert("Success", "Activity added successfully", "OK");
                ActivityNameEntry.Text = string.Empty;
                ActivityCodeEntry.Text = string.Empty;
                ActivityEctsEntry.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Error", "Teachers not selected", "OK");
            }
        }
        else
        {
            DisplayAlert("Error", "Invalid ECTS input", "OK");
        }
    }
}
