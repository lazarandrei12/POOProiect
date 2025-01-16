namespace Program.clase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
public class MasinaElectric : Masina
{
    public MasinaElectric (string marca, string model, int anDeFabricatie, int kilometraj, string numarInmatriculare, bool valabilitate, int costBaza) : base(marca, model, anDeFabricatie, kilometraj, numarInmatriculare, valabilitate, costBaza) {}
    
    public override string TypeDiscriminator { get; set; } = "MasinaElectric";
    public override double CostInchirierePeZi()
    {
        return CostBaza + 50;
    }
    
    public static MasinaElectric CitireMasinaDeLaTastatura()
    {
        Console.WriteLine("Introdu marca: ");
        string marca = Console.ReadLine();
        Console.WriteLine("Introdu modelul: ");
        string model = Console.ReadLine();
        Console.WriteLine("Introdu anul de fabricatie: ");
        int anDeFabricatie;
        while (!int.TryParse(Console.ReadLine(), out anDeFabricatie) || anDeFabricatie > DateTime.Now.Year)
        {
            Console.WriteLine("An invalid. Introdu un an inainte de cel curent: ");
        }
        Console.WriteLine("Introdu kilometrajul: ");
        int kilometraj;
        while (!int.TryParse(Console.ReadLine(), out kilometraj) || kilometraj < 0)
        {
            Console.WriteLine("Kilometraj invalid. Introdu un numar pozitiv: ");
        }

        Console.WriteLine("Introdu numarul de inmatriculare: ");
        string numarInmatriculare = Console.ReadLine();

        Console.WriteLine("Este masina valabila pentru inchiriere? (true/false): ");
        bool valabilitate;
        while (!bool.TryParse(Console.ReadLine(), out valabilitate))
        {
            Console.WriteLine("Valabilitate invalida. Introdu 'true' pentru DA / 'false' pentru NU: ");
        }

        Console.WriteLine("Introdu costul de baza: ");
        int costBaza;
        while (!int.TryParse(Console.ReadLine(), out costBaza) || costBaza < 0)
        {
            Console.WriteLine("Cost invalid. Introdu un numar pozitiv: ");
        }

        return new MasinaElectric(marca, model, anDeFabricatie, kilometraj, numarInmatriculare, valabilitate, costBaza);
    }
}