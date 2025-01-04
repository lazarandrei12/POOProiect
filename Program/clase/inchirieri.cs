using System.Runtime.ConstrainedExecution;

namespace Program.clase;

public class Inchirieri
{
    public client client;
    public car masina;
    public DateTime IncepuInchiriere;
    public DateTime FinalInchiriere;
    public bool daune;
    public double pretTotal;
    public bool accident;

    public Inchirieri(client client, car masina, DateTime incepuInchiriere, DateTime finalInchiriere)
    {
        this.client = client;
        this.masina = masina;
        this.IncepuInchiriere = incepuInchiriere;
        this.FinalInchiriere = finalInchiriere;
        this.daune = false;
    }

    public int duratainchirirere()
    {
        return (FinalInchiriere - IncepuInchiriere).Days;
    }
    
    public double afiseazapret()
    {
        return pretTotal * duratainchirirere();
    }


}