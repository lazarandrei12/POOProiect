namespace Program.clase;
using System.Collections.Generic;
public class Client
{
    public string Nume;
    public string Cnp;
    public List<Inchirieri> IstoricInchirieri = new List<Inchirieri>();
    public Client(string nume, string cnp)
    {
        this.Nume = nume;
        this.Cnp = cnp;
        List<Inchirieri> IstoricInchirieri = new List<Inchirieri>();
    
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
    public bool PoateInchiria()
    {
        return !IstoricInchirieri.Any(i => i.Daune);
    }
    
    public void AdaugaIstoricInchirieri(Inchirieri inchirieri)
    {
        IstoricInchirieri.Add(inchirieri);
    }    
    public void afiseazaclient()
    {
        Console.WriteLine($"nume: {Nume}, cnp: {Cnp}");
    }
}