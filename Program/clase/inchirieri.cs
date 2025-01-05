using System.Runtime.ConstrainedExecution;
using System.Threading.Channels;

namespace Program.clase;

public class Inchirieri
{
    public client client;
    public Car masina;
    public DateTime InceputInchiriere;
    public DateTime FinalInchiriere;
    public double pretTotal;
    public bool accident;
    public Inchirieri(client client, Car masina, DateTime inceputInchiriere, DateTime finalInchiriere,bool accident)
    {
        this.client = client;
        this.masina = masina;
        this.InceputInchiriere = inceputInchiriere;
        this.FinalInchiriere = finalInchiriere;
        this.accident = accident;

        afiseazapret();
    }
    
    public int duratainchirirere()
    {
        return (FinalInchiriere - InceputInchiriere).Days;
    }
    
    public double afiseazapret()
    {
        return pretTotal * duratainchirirere();
    }

    public override string ToString()
    {
        return $"Client: {client},masina:{masina},inceputInchiriere:{InceputInchiriere},data de final:{FinalInchiriere},accident:{accident}";
    }

    public void ValdareDate(DateTime InceputInchirire, DateTime FinalInchirire)
    {
        if (FinalInchiriere < InceputInchirire)
        {
            throw new ArgumentException("data de final trb dupa cea de inceput");
        }
    }

    public void afiseazadetalii()
    {
        Console.WriteLine($"masina inchiriata:{masina.Marca}{masina.Model}");
        Console.WriteLine($"durata inchirierii este de:{duratainchirirere()} zile");
        Console.WriteLine($"Pretul total este de:{masina.CostInchirierePeZi()} lei");
        Console.WriteLine($"stare: {masina.afiseazavalabilitate()}");
    }
}