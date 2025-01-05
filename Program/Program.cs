using Program.clase;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        companie_inchirieri companie1 = new companie_inchirieri("pipepepuRent", "timisoara", 12345);
        client client1 = new client("viorel", "1234567891023", "sefulBanilor", "rece12");
        client client2 = new client("mircea", "225678457896","cing","431fedf");
        Car car1 = new masina_electric("tesla ", "y ", 2019, 15, "CJ-15-MUI", true, 40);
        Car car2 = new masina_electric("dacia ", "bb ", 2021, 2414, "CJ-15-MUI", false, 70);
        Car car3 = new masina_standard("audee ", "a5 ", 2000, 1500, "AG-23-MEC", true, 150);
        companie1.Adaugamasina(car1);
        companie1.Adaugamasina(car2);
        companie1.Adaugamasina(car3);
        DateTime datainceput1 = DateTime.Parse("2024-10-5");
        DateTime datafinal1 = DateTime.Parse("2024-10-15");
        DateTime datainceput2 = DateTime.Parse("2024-10-18");
        DateTime datafinal2 = DateTime.Parse("2025-1-25");
        Inchirieri inchirirere1 = new Inchirieri(client1, car2,datainceput1,datafinal1,true);
        Inchirieri inchirirere2 = new Inchirieri(client2, car3,datainceput2,datafinal2,false);
        companie1.Adaugainchiriere(inchirirere1);
        companie1.Adaugainchiriere(inchirirere2);
        inchirirere1.afiseazadetalii();
        inchirirere2.afiseazadetalii();
        
        
    }
    
}