namespace Program.clase;

public class masina_standard : Car
{
    public masina_standard(string marca, string model, int anDeFabricatie, int kilometraj, string numarInmatriculare, bool valabilitate, int costBaza) : base(marca, model, anDeFabricatie, kilometraj, numarInmatriculare, valabilitate, costBaza) {}
    
    public override double CostInchirierePeZi()
    {
        return CostBaza;
    }
}