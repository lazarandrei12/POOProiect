namespace Program.clase;

public class client
{
    public string nume;
    public string cnp;
    public string username;
    public string password;
    List<Inchirieri> istoricInchirieri = new List<Inchirieri>();
    public bool istoricaccident()
    {
     return istoricInchirieri.All(accident => accident.accident);   
    }
    public void afiseazaclient()
    {
        Console.WriteLine($"nume: {nume}, cnp: {cnp}, username: {username}, password: {password}");
    }
}