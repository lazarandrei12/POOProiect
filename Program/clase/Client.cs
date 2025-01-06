namespace Program.clase;

public class Client
{
    public string nume;
    public string cnp;
    public string username;
    public string password;
    List<Inchirieri> istoricInchirieri = new List<Inchirieri>();
    public Client(string nume, string cnp, string username, string password)
    {
        this.nume = nume;
        this.cnp = cnp;
        this.username = username;
        this.password = password;
    
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
    public bool IstoricAccident()
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
    
    public void AfiseazaClient()
    {
        Console.WriteLine($"nume: {nume}, cnp: {cnp}, username: {username}, password: {password}");
    }
}