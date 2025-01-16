using System.Runtime.ConstrainedExecution;
using System.Threading.Channels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Program.clase;

public class InchiriereDTO
{
    public string NumeClient { get; set; }
    public string UsernameClient { get; set; }
    public string MarcaMasina { get; set; }
    public string ModelMasina { get; set; }
    public string NumarInmatriculare { get; set; }
    public string DataInceput { get; set; }
    public string DataSfarsit { get; set; }
    public double PretTotal { get; set; }
}
public class Inchirieri
{
    public User User;
    public Masina masina;
    public DateOnly InceputInchiriere;
    public DateOnly FinalInchiriere;
    public double pretTotal;
    public Inchirieri(User user, Masina masina, DateOnly inceputInchiriere, DateOnly finalInchiriere)
    {
        this.User = user;
        this.masina = masina;
        this.InceputInchiriere = inceputInchiriere;
        this.FinalInchiriere = finalInchiriere;
        this.pretTotal = masina.CostInchirierePeZi() * DurataInchirirere();
    }
    
    public int DurataInchirirere()
    {
        return FinalInchiriere.DayNumber - InceputInchiriere.DayNumber;
    }
    
    public double AfiseazaPret()
    {
        return pretTotal;
    }

    public override string ToString()
    {
        return $"Client: {User.Nume}, Masina: {masina.Marca} {masina.Model} {masina.NumarInmatriculare}, Perioada inchirierii: {InceputInchiriere}-{FinalInchiriere}";
    }

    public void ValidareDate(DateOnly InceputInchiriere, DateOnly FinalInchiriere)
    {
        if (FinalInchiriere < InceputInchiriere)
        {
            throw new ArgumentException("data de final trb dupa cea de inceput");
        }
    }

    public void AfiseazaDetalii()
    {
        Console.WriteLine($"Masina închiriată: {masina.Marca} {masina.Model} {masina.NumarInmatriculare}");
        Console.WriteLine($"Durata închirierii este de: {DurataInchirirere()} de zile");
        Console.WriteLine($"Pretul total este de: {pretTotal} lei");
        Console.WriteLine();
    }
    
    private static string inchirieriFilePath = "inchirieri.json";

    public static List<Inchirieri> IncarcaInchirieriDinFisier()
    {
        if (File.Exists(inchirieriFilePath))
        {
            string json = File.ReadAllText(inchirieriFilePath);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            try
            {
                var inchirieriDTO = JsonSerializer.Deserialize<List<InchiriereDTO>>(json, options);
                if (inchirieriDTO != null)
                {
                    return inchirieriDTO.Select(dto => new Inchirieri(
                        new User { Nume = dto.NumeClient, UsernameClient = dto.UsernameClient },
                        new MasinaStandard(dto.MarcaMasina, dto.ModelMasina, 0, 0, dto.NumarInmatriculare, false, 0),
                        DateOnly.Parse(dto.DataInceput),
                        DateOnly.Parse(dto.DataSfarsit)
                    )).ToList();
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Eroare la încărcarea închirierilor: {ex.Message}");
            }
        }
        return new List<Inchirieri>();
    }

    public static void SalveazaInchirieriInFisier(List<Inchirieri> inchirieri)
    {
        var inchirieriDTO = inchirieri.Select(i => new InchiriereDTO
        {
            NumeClient = i.User.Nume,
            UsernameClient = i.User.UsernameClient,
            MarcaMasina = i.masina.Marca,
            ModelMasina = i.masina.Model,
            NumarInmatriculare = i.masina.NumarInmatriculare,
            DataInceput = i.InceputInchiriere.ToString(),
            DataSfarsit = i.FinalInchiriere.ToString(),
            PretTotal = i.pretTotal
        }).ToList();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(inchirieriDTO, options);
        File.WriteAllText(inchirieriFilePath, json);
    }
    
}