namespace Program.clase;

public class companie_inchirieri
{
    public string nume_companie;
    public string adresa;
    public int cod_unic;
    List<car> cars = new List<car>();
    List<Inchirieri> inchiriaza = new List<Inchirieri>();
    

    public companie_inchirieri(string nume_companie, string adresa, int cod_unic)
    {
        this.nume_companie = nume_companie;
        this.adresa = adresa;
        this.cod_unic = cod_unic;
        List<car> cars = new List<car>();
        List<Inchirieri> inchiriaza = new List<Inchirieri>();
    }

    public override string ToString()
    {
        return $"{nume_companie}, {adresa}, {cod_unic}";
    }
   
}