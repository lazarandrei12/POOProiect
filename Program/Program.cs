using System.Globalization;
using Program.clase;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        CompanieInchirieri companie1 = new CompanieInchirieri("pipepepuRent", "timisoara", 12345);
        Client client1 = new Client("viorel", "1234567891023", "sefulBanilor", "rece12");
        Client client2 = new Client("mircea", "225678457896", "cing", "431fedf");
        Masina car1 = new MasinaElectric("tesla ", "y ", 2019, 15, "CJ-15-MUI", true, 40);
        Masina car2 = new MasinaElectric("dacia ", "bb ", 2021, 2414, "CJ-15-MUI", true, 70);
        Masina car3 = new MasinaStandard("audee ", "a5 ", 2000, 1500, "AG-23-MEC", true, 150);
        companie1.AdaugaMasina(car1);
        companie1.AdaugaMasina(car2);
        companie1.AdaugaMasina(car3);
        DateOnly datainceput1 = DateOnly.Parse("2024-10-5");
        DateOnly datafinal1 = DateOnly.Parse("2024-10-15");
        DateOnly datainceput2 = DateOnly.Parse("2024-10-18");
        DateOnly datafinal2 = DateOnly.Parse("2025-1-25");
        Inchirieri inchirirere1 = new Inchirieri(client1, car2, DateOnly.Parse("2024-10-5"), DateOnly.Parse("2024-10-15"), true);
        Inchirieri inchirirere2 = new Inchirieri(client2, car3, DateOnly.Parse("2024-10-18"), DateOnly.Parse("2025-1-25"), false);
        companie1.AdaugaInchiriere(inchirirere1);
        companie1.AdaugaInchiriere(inchirirere2);
        inchirirere1.AfiseazaDetalii();
        inchirirere2.AfiseazaDetalii();

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
                    Console.WriteLine("3.Log in administrator");
                    Console.WriteLine("Alegeti o optiune: ");
                    string suboptiune = Console.ReadLine();
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    Console.WriteLine();
                    Console.WriteLine("ISTORIC INCHIRIERI ALE COMPANIEI: ");
                    foreach (var inchiriere in companie1.inchiriate)
                    {
                        for(int i = 1; i < companie1.inchiriate.Count; i++)
                            Console.WriteLine($"{i}. {inchiriere.AfiseazaDetalii()}");
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
