using Program.clase;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        companie_inchirieri companie1 = new companie_inchirieri("pipepepuRent", "timisoara", 12345);
        client client1 = new client("viorel", "1234567891023", "sefulBanilor", "rece12");
        client client2 = new client("mircea", "225678457896","cing","431fedf");
        car car1 = new masina_electric("tesla", "y", 2019, 15, "CJ-15-MUI", true, 40);
        car car2 = new masina_electric("dacia", "bb", 2021, 2414, "CJ-15-MUI", false, 70);
        car car3 = new masina_standard("audee", "a5", 2000, 1500, "PL-3123-ME", true, 150);
        Inchirieri inchirirere1 = new Inchirieri(client1, car2,"15/10/2024","13/11/2024",true);
        Inchirieri inchirirere2 = new Inchirieri(client2, car3,"11/1/2023","12/10/2023",false);
        inchirirere1.duratainchirirere();
        inchirirere2.afiseazapret();
        inchirirere2.duratainchirirere();
        inchirirere2.afiseazapret();
        
    }
    
}