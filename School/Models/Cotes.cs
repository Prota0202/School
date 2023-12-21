namespace School.Models;

public class Cotes : Evaluations
{
    private int note;

    public Cotes(Activity activity,int note) : base(activity) {
        SetNote(note);
    }

    public Cotes(Activity activity) : base(activity) {
        SetNote(0);
    }

    public void SetNote(int note) {
        this.note = note;
    }

    public override int Note()
    {
        Console.WriteLine("In Note/Cote");
        return note;
    }
}