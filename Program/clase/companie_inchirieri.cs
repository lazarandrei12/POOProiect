namespace Program.clase;

public class companie_inchirieri
{
    public string nume_companie;
    public string adresa;
    public int cod_unic;
    public List<Car> flota = new List<Car>();
    public List<Inchirieri> inchiriate = new List<Inchirieri>();
    

    public companie_inchirieri(string nume_companie, string adresa, int cod_unic)
    {
        this.nume_companie = nume_companie;
        this.adresa = adresa;
        this.cod_unic = cod_unic;
        List<Car> flota = new List<Car>();
        List<Inchirieri> inchiriate = new List<Inchirieri>();
    }

    public void Adaugamasina(Car masina)
    {
        flota.Add(masina);
    }

    public void Adaugainchiriere(Inchirieri inchiriere)
    {
        inchiriate.Add(inchiriere);
        Console.WriteLine($"Client: {inchiriere.client.nume}, Masina: {inchiriere.masina.Marca} {inchiriere.masina.Model} {inchiriere.masina.NumarInmatriculare}, Durata: {inchiriere.InceputInchiriere}-{inchiriere.FinalInchiriere}");
        Console.WriteLine();
    }
    public override string ToString()
    {
        return $"{nume_companie}, {adresa}, {cod_unic}";
    }
   
}