using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Program.clase;

public abstract class Masina
{
    private List<string> numereInmatriculate = new List<string>();
    public string Marca { get; private set; }
    public string Model { get; private set; }
    public int AnDeFabricatie { get; private set; }
    public int Kilometraj { get; private set; }
    public string NumarInmatriculare { get; private set; }
    public bool Valabilitate { get; set; }
    public int CostBaza { get; private set; }
    [JsonIgnore] 
    public virtual string TypeDiscriminator { get; set; }

    public Masina (string marca, string model, int anDeFabricatie, int kilometraj, string numarInmatriculare, bool valabilitate, int costBaza)
    {
        
        this.Marca = marca;
        this.Model = model;
        this.AnDeFabricatie = anDeFabricatie;
        this.Kilometraj = kilometraj;
        this.NumarInmatriculare = numarInmatriculare;
        this.Valabilitate = valabilitate;
        this.CostBaza = costBaza;
        if (numereInmatriculate.Contains(numarInmatriculare))
        {
            throw new ArgumentException("Acest numar de inmatriculare este inregistrat deja...");
        }
        
        if (!ValabilitateNumarInmatriculare(numarInmatriculare))
        {
            throw new ArgumentException("Numar de inmatriculare invalid...");
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
        try
        {
            if (!Valabilitate)
            {
                throw new ArgumentException($"Masina {Marca}{Model} este momentan inchiriata");
            }

            Valabilitate = false;
            Console.WriteLine($"masina {Marca}{Model} a fost inchiriata");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public void ReturneazaMasina()
    {
        Valabilitate = true;
    }

    public abstract double CostInchirierePeZi();

    public void AfiseazaDateMasina()
    {
        Console.WriteLine($"Marca: {Marca}, Model: {Model}, An de fabricatie: {AnDeFabricatie}, Kilometraj: {Kilometraj}, Numar Inmatriculare: {NumarInmatriculare}, Pret: {CostInchirierePeZi()}");
        
        if (!AfiseazaValabilitate())
        {
            Console.WriteLine($"Masina {Marca} {Model} {NumarInmatriculare} este deja închiriată");
        }
        else
        {
            Console.WriteLine("Este valabilă pentru închiriere");
        }
        Console.WriteLine();
    }

    public bool AfiseazaValabilitate()
    {
        return Valabilitate;
    }
    
}