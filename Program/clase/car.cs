using System.Text.RegularExpressions;

namespace Program.clase;

public abstract class car
{
    static private HashSet<string> numereInmatriculate = new HashSet<string>();
    public string Marca { get; private set; }
    public string Model { get; private set; }
    public int AnDeFabricatie { get; private set; }
    public int Kilometraj { get; private set; }
    public string NumarInmatriculare { get; private set; }
    public bool Valabilitate { get; private set; }
    public int CostBaza { get; private set; }
    public car (string marca, string model, int anDeFabricatie, int kilometraj, string numarInmatriculare, bool valabilitate, int costBaza)
    {
        this.Marca = marca;
        this.Model = model;
        this.AnDeFabricatie = anDeFabricatie;
        this.Kilometraj = kilometraj;
        this.NumarInmatriculare = numarInmatriculare;
        this.Valabilitate = valabilitate;
        this.CostBaza = costBaza;
        
        if (!ValabilitateNumarInmatriculare(numarInmatriculare))
        {
            throw new ArgumentException("Numar de inmatriculare invalid...");
        }

        if (numereInmatriculate.Contains(numarInmatriculare))
        {
            throw new ArgumentException("Acest numar de inmatriculare este inregistrat deja...");
        }
        
        numereInmatriculate.Add(numarInmatriculare);
    }
    private static bool ValabilitateNumarInmatriculare(string numarInmatriculare)
    {
        HashSet<string> PrefixJudet = new HashSet<string>
        {
            "B", "AB", "AG", "AR", "BC", "BH", "BN", "BR", "BT", "BV", "BZ",
            "CJ", "CL", "CS", "CT", "CV", "DB", "DJ", "GJ", "GL", "GR", "HD",
            "HR", "IF", "IL", "IS", "MH", "MM", "MS", "NT", "OT", "PH", "SB",
            "SJ", "SM", "SV", "TL", "TM", "TR", "VL", "VN", "VS"
        };

        string exempluNrInmatriculare = @"^([A-Z]{1,2})-(\d{2})-([A-Z]{3})$";
        Match match = Regex.Match(numarInmatriculare, exempluNrInmatriculare);

        if (!match.Success)
        {
            return false;
        }

        string prefix = match.Groups[1].Value;
        return PrefixJudet.Contains(prefix);
    }
    
    public void InchiriazaMasina()
    {
        if (!Valabilitate)
        {
            throw new ArgumentException("The car is currently being rented out.");
        }

        Valabilitate = false;
    }

    public void ReturneazaMasina()
    {
        Valabilitate = true;
    }

    public abstract decimal CostInchirierePeZi();
    
}