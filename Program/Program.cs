using System.Globalization;
using System.Collections.Generic;
using Program.clase;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        CompanieInchirieri companie1 = new CompanieInchirieri("pipepepuRent", "timisoara", 12345);
        Client client1 = new Client("Belg David", "1234567891023");
        Client client2 = new Client("Basholin Darius", "2256784517896");
        Client client3 = new Client("Laser Andrew", "5423224517896");
        Masina car1 = new MasinaElectric("Tesla", "Y", 2019, 10000, "CJ-15-MUE", true, 100);
        Masina car2 = new MasinaElectric("Dacia", "Sandero", 2021, 2414, "CJ-15-MIL", false, 80);
        Masina car3 = new MasinaStandard("Audi", "A5", 2015, 150000, "AG-23-SUJ", true, 150);
        Masina car4 = new MasinaStandard("BMW", "M3 Competition", 2020, 56000, "AG-23-SUJ", true, 230);
        companie1.AdaugaMasina(car1);
        companie1.AdaugaMasina(car2);
        companie1.AdaugaMasina(car3);
        companie1.AdaugaMasina(car4);
        Inchirieri inchirirere1 = new Inchirieri(client1, car2, DateOnly.Parse("2024-10-5"), DateOnly.Parse("2024-10-15"), true);
        Inchirieri inchirirere2 = new Inchirieri(client2, car3, DateOnly.Parse("2024-10-18"), DateOnly.Parse("2025-1-25"), true);
        companie1.AdaugaInchiriere(inchirirere1);
        client1.AdaugaIstoricInchirieri(inchirirere1);
        companie1.AdaugaInchiriere(inchirirere2);
        inchirirere1.AfiseazaDetalii();
        inchirirere2.AfiseazaDetalii();
        User user = new User();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Bun venit la PipepepuRent");
            Console.WriteLine("1.Vezi masini pt inchiriere");
            Console.WriteLine("2.Log in/Sign in");
            Console.WriteLine("3.Pentru Adminisrator");
            Console.WriteLine("4.Petru Client");
            Console.WriteLine("5.Vizualizare istoric inchirieri ale companiei");
            Console.WriteLine("6.Vizualizare istoric inchirieri client");
            Console.WriteLine("7.Vizualizare castiguri");
            Console.WriteLine("8.Iesire");
            Console.WriteLine("Alegeti o optiune: ");
            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    Console.WriteLine();
                    Console.WriteLine("Masini pt inchiriere:");
                    foreach (var masina in companie1.flota)
                    {
                        if (masina.Valabilitate == true)
                        {
                            masina.AfiseazaDateMasina();
                        }
                    }
                    break;
                case "2":
                    Console.WriteLine();
                    Console.WriteLine("1.Log in");
                    Console.WriteLine("2.Sign up");
                    Console.WriteLine("Alegeti o optiune: ");
                    string suboptiune1 = Console.ReadLine();
                    if (suboptiune1 == "1")
                    {
                        user.Login();
                    }
                    else if (suboptiune1 == "2")
                    {
                        user.SignUp();
                    }
                    else
                    {
                        Console.WriteLine("Optiune invalida");
                    }
                    break;
                case "3":
                    List<MasinaStandard> masiniS = new List<MasinaStandard>();
                    List<MasinaElectric> masiniE = new List<MasinaElectric>();
                    bool exit1 = false;
                    while (!exit1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("1.Adaugare masina");
                        Console.WriteLine("2.Stergere masina");
                        Console.WriteLine("3.Iesire");
                        Console.WriteLine("Alegeti o optiune: ");
                        string suboptiune2 = Console.ReadLine();
                        switch (suboptiune2)
                        {
                            case "1":
                                Console.WriteLine("Doresti sa adaugi o masina standard sau electric?");
                                string TipMasina = Console.ReadLine();
                                if (TipMasina.ToLower() == "standard")
                                {
                                    MasinaStandard.CitireMasinaDeLaTastatura();
                                }
                                else if (TipMasina.ToLower() == "electric")
                                {
                                    MasinaElectric.CitireMasinaDeLaTastatura();
                                }
                                else
                                {
                                    Console.WriteLine("Tipul de masina nu este valid");
                                }

                                break;
                            case "2":
                                Console.WriteLine("Doresti sa adaugi o masina standard sau electric?");
                                TipMasina = Console.ReadLine();
                                if (TipMasina.ToLower() == "standard")
                                {
                                    MasinaStandard.StergeMasina(masiniS);
                                }
                                else if (TipMasina.ToLower() == "electric")
                                {
                                    MasinaElectric.StergeMasina(masiniE);
                                }
                                else
                                {
                                    Console.WriteLine("Tipul de masina nu este valid");
                                }
                                break;
                            case "3":
                                exit1 = true;
                                break;
                        }
                    }
                    break;
                case "4":
                    Console.WriteLine();
                    Console.WriteLine("1.Vizualizare masini disponibile pentru inchiriat");
                    Console.WriteLine("2.Inchiriere masina selectata");
                    Console.WriteLine("3.Inapoiere mașină");
                    break;
                case "5":
                    Console.WriteLine();
                    Console.WriteLine("ISTORIC INCHIRIERI ALE COMPANIEI: ");
                    int i = 1;
                    foreach (var inchiriere in companie1.inchiriate)
                    {
                        Console.WriteLine($"{i}. {companie1.inchiriate[i - 1]}");
                        i++;
                    }
                    break;
                case "6":
                    Console.WriteLine();
                    Console.WriteLine($"ISTORIC INCHIRIERI CLIENT {client1.Nume} :");
                    i = 1;
                    foreach (var inchiriere in client1.IstoricInchirieri)
                    {
                        Console.WriteLine($"{i}. {client1.IstoricInchirieri[i - 1]}");
                        i++;
                    }
                    break;
                case "7":
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("optiune incorecta");
                    break;
            }
        }
    } 
}
