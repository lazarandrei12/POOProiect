namespace Program.clase;

public class masina_electric : car
{
    public masina_electric (string marca, string model, int anDeFabricatie, int kilometraj, string numarInmatriculare, bool valabilitate, int costBaza) : base(marca, model, anDeFabricatie, kilometraj, numarInmatriculare, valabilitate, costBaza) {}
    
    public override decimal CostInchirierePeZi()
    {
        return CostBaza + 50;
    }
}