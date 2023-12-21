namespace School.Models;

public abstract class Evaluations
{
    private Activity activity;

    public Evaluations(Activity activity) {
        this.activity = activity;
    }

    public Activity Activity {
        get {
            return activity;
        }
    }

    public abstract int Note();

    public override string ToString()
    {
        Console.WriteLine("In Tostring/evaluation");
        Console.WriteLine("Activity: {0}", Activity);
        Console.WriteLine("Note(): {0}", Note());
        return String.Format("{0}: {1}/20", Activity, Note());
    }
}
