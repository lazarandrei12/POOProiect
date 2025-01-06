using System.Globalization;
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
        Inchirieri inchirirere2 = new Inchirieri(client2, car3, DateOnly.Parse("2024-10-18"), DateOnly.Parse("2025-1-25"), false);
        companie1.AdaugaInchiriere(inchirirere1);
        companie1.AdaugaInchiriere(inchirirere2);
        inchirirere1.AfiseazaDetalii();
        inchirirere2.AfiseazaDetalii();
        User user = new User();
        
        while (true)
        {
            Console.WriteLine("Bun venit la PipepepuRent");
            Console.WriteLine("1.Vezi masini pt inchiriere");
            Console.WriteLine("2.Log in/Sign in");
            Console.WriteLine("3.Pentru Adminisrator");
            Console.WriteLine("4.Petru Client");
            Console.WriteLine("5.Vizualizare istoric inchirieri ale companiei");
            Console.WriteLine("6.Vizualizare istoric inchirieri client");
            Console.WriteLine("7.Vizualizare castiguri");
            Console.WriteLine("Alegeti o optiune: ");
            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
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
                    Console.WriteLine("1.Log in");
                    Console.WriteLine("2.Sign up");
                    Console.WriteLine("Alegeti o optiune: ");
                    string suboptiune = Console.ReadLine();
                    if (suboptiune == "1")
                    {
                        user.Login();
                    }
                    else if (suboptiune == "2")
                    {
                        user.SignUp();
                    }
                    else
                    {
                        Console.WriteLine("Optiune invalida");
                    }
                    break;
                case "3":
                    break;
                case "4":
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
                    break;
                case "7":
                    break;
                default:
                    Console.WriteLine("optiune incorecta");
                    break;
            }
        }
    } 
}
