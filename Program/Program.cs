using System.Globalization;
using Program.clase;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        companie_inchirieri companie1 = new companie_inchirieri("pipepepuRent", "timisoara", 12345);
        client client1 = new client("Belg David", "1234567891023", "belg", "theking");
        client client2 = new client("Baloshin Darius", "2256784578961", "linganguli", "pasoptist");
        client client3 = new client("Laser Andrei", "2954828585000", "firmirelu", "rece12");
        Car car1 = new masina_electric("Tesla", "Y", 2018, 86000, "CJ-15-MIL", true, 130);
        Car car4 = new masina_standard("BMW", "M3 Competition", 2020, 16000, "TM-21-BLG", true, 230);
        Car car2 = new masina_electric("Dacia", "Solenza", 2003, 343000, "CJ-99-MUE", true, 70);
        Car car3 = new masina_standard("Audi", "A5 ", 2015, 50000, "AG-33-SUJ", true, 100);
        companie1.Adaugamasina(car1);
        companie1.Adaugamasina(car2);
        companie1.Adaugamasina(car3);
        companie1.Adaugamasina(car4);
        Inchirieri inchirirere1 = new Inchirieri(client1, car2, DateOnly.Parse("2024-10-5"), DateOnly.Parse("2024-10-15"), true);
        Inchirieri inchirirere2 = new Inchirieri(client2, car4, DateOnly.Parse("2024-10-18"), DateOnly.Parse("2025-1-25"), false);
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
                default:
                    Console.WriteLine("optiune incorecta");
                    break;
            }
        }
    } 
}
