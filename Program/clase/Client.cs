namespace Program.clase;

public class Client
{
    public string nume;
    public string cnp;
    List<Inchirieri> istoricInchirieri = new List<Inchirieri>();
    public Client(string nume, string cnp)
    {
        this.nume = nume;
        this.cnp = cnp;
       
    
        try
        {
            if (cnp.Length != 13)
            {
                throw new ArgumentException("CNP-ul este scris gresit");
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public bool istoricaccident()
    {
        foreach (Inchirieri inchiriat in istoricInchirieri)
        {
            if (inchiriat.accident)
            {
                return false;
            }
        }
        return true;
    }
    
    public void afiseazaclient()
    {
        Console.WriteLine($"nume: {nume}, cnp: {cnp}");
    }
}