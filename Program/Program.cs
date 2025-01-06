using System.Globalization;
using Program.clase;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        companie_inchirieri companie1 = new companie_inchirieri("pipepepuRent", "timisoara", 12345);
        client client1 = new client("viorel", "1234567891023", "sefulBanilor", "rece12");
        client client2 = new client("mircea", "225678457896", "cing", "431fedf");
        Car car1 = new masina_electric("tesla ", "y ", 2019, 15, "CJ-15-MUI", true, 40);
        Car car2 = new masina_electric("dacia ", "bb ", 2021, 2414, "CJ-15-MUI", false, 70);
        Car car3 = new masina_standard("audee ", "a5 ", 2000, 1500, "AG-23-MEC", true, 150);
        companie1.Adaugamasina(car1);
        companie1.Adaugamasina(car2);
        companie1.Adaugamasina(car3);
        DateOnly datainceput1 = DateOnly.Parse("2024-10-5");
        DateOnly datafinal1 = DateOnly.Parse("2024-10-15");
        DateOnly datainceput2 = DateOnly.Parse("2024-10-18");
        DateOnly datafinal2 = DateOnly.Parse("2025-1-25");
        Inchirieri inchirirere1 = new Inchirieri(client1, car2, datainceput1, datafinal1, true);
        Inchirieri inchirirere2 = new Inchirieri(client2, car3, datainceput2, datafinal2, false);
        companie1.Adaugainchiriere(inchirirere1);
        companie1.Adaugainchiriere(inchirirere2);
        inchirirere1.afiseazadetalii();
        inchirirere2.afiseazadetalii();

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
