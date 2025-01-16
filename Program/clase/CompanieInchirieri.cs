namespace Program.clase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
public class CompanieInchirieri
{
    public string NumeCompanie;
    public string Adresa;
    public int CodUnic;
    public List<Masina> flota = new List<Masina>();
    public List<Inchirieri> inchiriate = new List<Inchirieri>();
    

    public CompanieInchirieri(string nume_companie, string adresa, int cod_unic)
    {
        this.NumeCompanie = nume_companie;
        this.Adresa = adresa;
        this.CodUnic = cod_unic;
        List<Masina> flota = new List<Masina>();
        List<Inchirieri> inchiriate = new List<Inchirieri>();
    }

    public void AdaugaMasina(Masina masina)
    {
        flota.Add(masina);
    }

    public void AdaugaInchiriere(Inchirieri inchiriere)
    {
        inchiriate.Add(inchiriere);
    }
    
    public override string ToString()
    {
        return $"{NumeCompanie}, {Adresa}, {CodUnic}";
    }
   
}