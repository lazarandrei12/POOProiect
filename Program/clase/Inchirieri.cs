using System.Runtime.ConstrainedExecution;
using System.Threading.Channels;

namespace Program.clase;

public class Inchirieri
{
    public Client client;
    public Masina masina;
    public DateOnly InceputInchiriere;
    public DateOnly FinalInchiriere;
    public double pretTotal;
    public bool accident;
    public Inchirieri(Client client, Masina masina, DateOnly inceputInchiriere, DateOnly finalInchiriere,bool accident)
    {
        this.client = client;
        this.masina = masina;
        this.InceputInchiriere = inceputInchiriere;
        this.FinalInchiriere = finalInchiriere;
        this.accident = accident;

        AfiseazaPret();
    }
    
    public int DurataInchirirere()
    {
        return FinalInchiriere.DayNumber - InceputInchiriere.DayNumber;
    }
    
    public double AfiseazaPret()
    {
        return pretTotal * DurataInchirirere();
    }

    public override string ToString()
    {
        return $"Client: {client.nume}, Masina: {masina.Marca} {masina.Model} {masina.NumarInmatriculare}, Perioada inchirierii: {InceputInchiriere}-{FinalInchiriere}, Accident: {accident}";
    }

    public void ValidareDate(DateOnly InceputInchiriere, DateOnly FinalInchiriere)
    {
        if (FinalInchiriere < InceputInchiriere)
        {
            throw new ArgumentException("data de final trb dupa cea de inceput");
        }
    }

    public void AfiseazaDetalii()
    {
        Console.WriteLine($"Masina închiriată:{masina.Marca} {masina.Model} {masina.NumarInmatriculare}");
        Console.WriteLine($"Durata închirierii este de: {DurataInchirirere()} de zile");
        Console.WriteLine($"Pretul total este de: {masina.CostInchirierePeZi()*DurataInchirirere()} lei");
        if (!masina.AfiseazaValabilitate())
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