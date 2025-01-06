namespace Program.clase;

public class CompanieInchirieri
{
    public string NumeCompanie;
    public string Adresa;
    public int CodUnic;
    public List<Masina> flota = new List<Masina>();
    public List<Inchirieri> inchiriate = new List<Inchirieri>();
    public List<Client> clienti = new List<Client>();
    

    public CompanieInchirieri(string nume_companie, string adresa, int cod_unic)
    {
        this.NumeCompanie = nume_companie;
        this.Adresa = adresa;
        this.CodUnic = cod_unic;
        List<Masina> flota = new List<Masina>();
        List<Inchirieri> inchiriate = new List<Inchirieri>();
        List<Client> clienti = new List<Client>();
    }

    public void AdaugaMasina(Masina masina)
    {
        flota.Add(masina);
    }

    public void AdaugaInchiriere(Inchirieri inchiriere)
    {
        inchiriate.Add(inchiriere);
        Console.WriteLine($"Client: {inchiriere.client.nume}, Masina: {inchiriere.masina.Marca} {inchiriere.masina.Model} {inchiriere.masina.NumarInmatriculare}, Durata: {inchiriere.InceputInchiriere}-{inchiriere.FinalInchiriere}");
        Console.WriteLine();
    }

    public void AdaugaClient(Client client)
    {
        clienti.Add(client);
    }
    public override string ToString()
    {
        return $"{NumeCompanie}, {Adresa}, {CodUnic}";
    }
   
}