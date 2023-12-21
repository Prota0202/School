using School.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Maui.Controls.Compatibility.Platform;

namespace School.Models;

public class Activity
{
    private Teachers teachers;
    private int ects;
    private string name;
    private string code;
    public string Filename { get; set; }

    public Activity(string name, string code, int ECTS, Teachers teachers) {
        this.code = code;
        this.teachers = teachers;
        this.name = name;
        this.ects = ECTS;
        string activitenamestockfile = code + name;
        Filename = $"{activitenamestockfile}.notes.txt";
    }

    public string Code {
        get { return code; }
    }

    public string Name {
        get { return name; }
    }

    public int ECTS {
        get { return ects; }
    }

    public Teachers Teachers {
        get { return teachers; }
    }

    public override string ToString() {
        return $"[{Code}] {Name} ({Teachers.DisplayName})";
    }

    public void Save() {
        string content = $"{name}\n{code}\n{ECTS}\n{teachers}";
        File.WriteAllText(Path.Combine(Config.RootDir, "Activites", Filename), content);
    }

    public static Activity Load(string Filename, ObservableCollection<Teachers> TeachersList) {
        Filename = Path.Combine(Config.RootDir, "Activites", Filename);

        if (!File.Exists(Filename))
            throw new FileNotFoundException("Unable to find file on local storage.", Filename);

        var content = File.ReadAllText(Filename);
        var tokens = content.Split('\n');
        Teachers teachers2link = null;

        foreach(Teachers elem in TeachersList) {
            if (Equals(tokens[3], elem.ToString())) {
                teachers2link = elem;
                break;
            }
        }

        if (teachers2link == null) {
            throw new Exception($"Teachers not found: {tokens[3]}");
        }

        return new Activity(tokens[0], tokens[1], Convert.ToInt32(tokens[2]), teachers2link) {
            Filename = Path.GetFileName(Filename),
        };
    }

    public static IEnumerable<Activity> LoadAll(ObservableCollection<Teachers> TeachersList) {
        string activiteDirection = Path.Combine(Config.RootDir, "Activites");

        return Directory
            .EnumerateFiles(activiteDirection, "*notes.txt")
            .Select(filename => Load(Path.GetFileName(filename), TeachersList))
            .OrderByDescending(note => note.ECTS);
    }
}
