namespace School.Models;

public class Students : Persons
{
    private List<Evaluations> evaluations;

    public string Filename {get; set;}

    public Students(string firstname, string lastname) : base(firstname, lastname) {
        evaluations = new List<Evaluations>();
        string namestockfile = lastname + firstname;
        Filename = $"{namestockfile}.notes.txt";
    }

    public void Add(Evaluations evaluation) {
        Console.WriteLine("eval: {0}", evaluation);
        evaluations.Add(evaluation);
        Console.WriteLine("méthode add");
        Console.WriteLine("evaluation de add : "+evaluations);
    } //quand on ajoute evaluation on doit utiliser ayoub.add(16) 

    public double Average() {
        int total = 0;
        int ects = 0;
        foreach(var evaluation in evaluations) {
            total += evaluation.Note() * evaluation.Activity.ECTS;
            ects += evaluation.Activity.ECTS;
        }
        return total/ects;
    }

    public string Bulletin() {
        var lines = new List<String>
        {
            String.Format("Bulletin de {0}", DisplayName)
        };

        foreach(var evaluation in evaluations) {
            lines.Add(evaluation.ToString());
        }
        Console.WriteLine("coucou2");
        Console.WriteLine("jy crois pas trop "+evaluations.Count());
        lines.Add(String.Format("Moyenne: {0}", Average()));
        Console.WriteLine("méthode bulletin");

        return String.Join("\n", lines);
    }

    public override string ToString()
    {
        return String.Format("{0}",DisplayName);
    }

    public void Save(){
        Console.WriteLine("evaluation de save : "+evaluations);
        if(evaluations.Count > 0) {
            Console.WriteLine("yop");
            Console.WriteLine("truc: {0}", evaluations[0]);
        }
        
        string evaluationsContent = string.Join("\n", evaluations.Select(evaluation => evaluation.ToString()));
        Console.WriteLine("evaluation content : "+evaluationsContent);
        string content = String.Format("{0}\n{1}\n{2}", Firstname, Lastname,evaluationsContent);
        File.WriteAllText(System.IO.Path.Combine(Config.RootDir,"Students", Filename), content);
        Console.WriteLine("méthode save");
        Console.WriteLine(evaluations);
    }

    public static Students Load(string filename){
        filename = System.IO.Path.Combine(Config.RootDir,"Students", filename);

        if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);
        
        var content = File.ReadAllText(filename);
        var tokens = content.Split('\n');

        return
                new(tokens[0], tokens[1])
                {
                    Filename = Path.GetFileName(filename),
                };
    }

    public static IEnumerable<Students> LoadAll(){
        string appDataPath = FileSystem.AppDataDirectory;
        string etudiantsdir =  System.IO.Path.Combine(Config.RootDir, "Students");

        return Directory
                    .EnumerateFiles(etudiantsdir ,"*.notes.txt")
                    .Select(filename => Students.Load(Path.GetFileName(filename)))
                    .OrderByDescending(note => note.DisplayName);
    }
}
