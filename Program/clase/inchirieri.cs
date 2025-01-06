using System.Runtime.ConstrainedExecution;
using System.Threading.Channels;

namespace Program.clase;

public class Inchirieri
{
    public client client;
    public Car masina;
    public DateOnly InceputInchiriere;
    public DateOnly FinalInchiriere;
    public double pretTotal;
    public bool accident;
    public Inchirieri(client client, Car masina, DateOnly inceputInchiriere, DateOnly finalInchiriere,bool accident)
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
        return FinalInchiriere.DayNumber - InceputInchiriere.DayNumber;
    }
    
    public double afiseazapret()
    {
        return pretTotal * duratainchirirere();
    }

    public override string ToString()
    {
        return $"Client: {client},masina:{masina},inceputInchiriere:{InceputInchiriere},data de final:{FinalInchiriere},accident:{accident}";
    }

    public void ValdareDate(DateOnly InceputInchirire, DateOnly FinalInchirire)
    {
        if (FinalInchiriere < InceputInchirire)
        {
            throw new ArgumentException("data de final trb dupa cea de inceput");
        }
    }

    public void afiseazadetalii()
    {
        Console.WriteLine($"Masina închiriată:{masina.Marca} {masina.Model} {masina.NumarInmatriculare}");
        Console.WriteLine($"Durata închirierii este de: {duratainchirirere()} de zile");
        Console.WriteLine($"Pretul total este de: {masina.CostInchirierePeZi()*duratainchirirere()} lei");
        if (!masina.afiseazavalabilitate())
        {
            Console.WriteLine($"Masina {masina.Marca} {masina.Model} {masina.NumarInmatriculare} este deja închiriată");
        }
        else
        {
            Console.WriteLine($"Masina {masina.Marca} {masina.Model} {masina.NumarInmatriculare} este valabilă pentru închiriere");
        }
        Console.WriteLine();
    }
}