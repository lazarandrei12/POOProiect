namespace Program.clase;

public class MasinaElectric : Masina
{
    public MasinaElectric (string marca, string model, int anDeFabricatie, int kilometraj, string numarInmatriculare, bool valabilitate, int costBaza) : base(marca, model, anDeFabricatie, kilometraj, numarInmatriculare, valabilitate, costBaza) {}
    
    public override double CostInchirierePeZi()
    {
        return CostBaza + 50;
    }
}