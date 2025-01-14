using System.Globalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.Design;
using Program.clase;

namespace Program;

class Program
{
    static string filePath = "masini.json";
    
    class MasinaJsonConverter : JsonConverter<Masina>
    {
        public override Masina Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (!root.TryGetProperty("TypeDiscriminator", out JsonElement typeElement))
            {
                throw new JsonException("Missing TypeDiscriminator property.");
            }

            string type = typeElement.GetString();
            switch (type)
            {
                case "MasinaStandard":
                    return JsonSerializer.Deserialize<MasinaStandard>(root.GetRawText(), options);
                case "MasinaElectric":
                    return JsonSerializer.Deserialize<MasinaElectric>(root.GetRawText(), options);
                default:
                    Console.WriteLine($"Unknown TypeDiscriminator: {type}");
                    throw new JsonException($"Unknown TypeDiscriminator: {type}");
            }
        }


        public override void Write(Utf8JsonWriter writer, Masina value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
    static void Main(string[] args)
    {
        CompanieInchirieri companie1 = new CompanieInchirieri("pipepepuRent", "timisoara", 12345);
        List<Masina> masini = IncarcaMasiniDinFisier();
        User User1 = new User()
        {
            UsernameClient = "relu",
            ParolaClient = "viru",
            Nume = "Ion",
            Cnp = "1234123412342",
            IsAdmin = false,
            Logat = false
        };
        User.users.Add(User1);
        User User2 = new User();
        User User3 = new User();
        Masina car1 = new MasinaElectric("Tesla", "Y", 2019, 10000, "CJ-15-MUE", true, 100);
        Masina car2 = new MasinaElectric("Dacia", "Sandero", 2021, 2414, "CJ-15-MIL", true, 80);
        Masina car3 = new MasinaStandard("Audi", "A5", 2015, 150000, "AG-23-SUJ", true, 150);
        Masina car4 = new MasinaStandard("BMW", "M3 Competition", 2020, 56000, "AG-23-SUJ", true, 230);
        companie1.AdaugaMasina(car1);
        companie1.AdaugaMasina(car2);
        companie1.AdaugaMasina(car3);
        companie1.AdaugaMasina(car4);
        SalveazaMasiniInFisier(masini);
        Inchirieri inchirirere1 = new Inchirieri(User1, car2, DateOnly.Parse("2024-10-5"), DateOnly.Parse("2024-10-15"), true);
        Inchirieri inchirirere2 = new Inchirieri(User1, car3, DateOnly.Parse("2024-10-18"), DateOnly.Parse("2025-1-25"), true);
        companie1.AdaugaInchiriere(inchirirere1);
        User1.AdaugaIstoricInchirieri(inchirirere1);
        companie1.AdaugaInchiriere(inchirirere2);
        inchirirere1.AfiseazaDetalii();
        inchirirere2.AfiseazaDetalii();
        User user = new User();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Bun venit la PipepepuRent");
            Console.WriteLine("1.Vezi masini pt inchiriere");
            Console.WriteLine("2.Log in/Sign in");
            Console.WriteLine("3.Pentru Adminisrator");
            Console.WriteLine("4.Petru Client");
            Console.WriteLine("5.Vizualizare istoric inchirieri ale companiei");
            Console.WriteLine("6.Vizualizare istoric inchirieri client");
            Console.WriteLine("7.Vizualizare castiguri");
            Console.WriteLine("8.Iesire");
            Console.WriteLine("Alegeti o optiune: ");
            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    Console.WriteLine();
                    Console.WriteLine("Masini pt inchiriere:");
                    var masiniFaraDuplicate = masini
                        .GroupBy(m => m.NumarInmatriculare)
                        .Select(g => g.First())
                        .ToList();
                    foreach (var masina in masini)
                    {
                        if (masina.Valabilitate)
                        {
                            masina.AfiseazaDateMasina();
                        }
                    }
                    break;
                case "2":
                    Console.WriteLine();
                    Console.WriteLine("1.Log in");
                    Console.WriteLine("2.Sign up");
                    Console.WriteLine("Alegeti o optiune: ");
                    string suboptiune1 = Console.ReadLine();
                    if (suboptiune1 == "1")
                    {
                        user.Login();
                    }
                    else if (suboptiune1 == "2")
                    {
                        user.SignUp();
                    }
                    else
                    {
                        Console.WriteLine("Optiune invalida");
                    }
                    break;
                case "3":
                    if (!user.IsAdmin)
                    {
                        Console.WriteLine("optiune valabila doar pentru admini;");
                        break;
                    }
                    List<MasinaStandard> masiniS = new List<MasinaStandard>();
                    List<MasinaElectric> masiniE = new List<MasinaElectric>();
                    bool exit1 = false;
                    while (!exit1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("1.Adaugare masina");
                        Console.WriteLine("2.Stergere masina");
                        Console.WriteLine("3.Iesire");
                        Console.WriteLine("Alegeti o optiune: ");
                        string suboptiune2 = Console.ReadLine();
                        switch (suboptiune2)
                        {
                            case "1":
                                Console.WriteLine("Doresti sa adaugi o masina standard sau electric?");
                                string TipMasina = Console.ReadLine();
                                if (TipMasina.ToLower() == "standard")
                                {
                                    masini.Add(MasinaStandard.CitireMasinaDeLaTastatura());
                                    SalveazaMasiniInFisier(masini);
                                }
                                else if (TipMasina.ToLower() == "electric")
                                {
                                    masini.Add(MasinaElectric.CitireMasinaDeLaTastatura());
                                    SalveazaMasiniInFisier(masini);
                                }
                                else
                                {
                                    Console.WriteLine("Tipul de masina nu este valid");
                                }

                                break;
                            case "2":
                                Console.WriteLine("Introduceti numarul de inmatriculare al masinii de sters:");
                                string numarInmatriculare = Console.ReadLine();
    
                                var masinaDeSters = masini.FirstOrDefault(m => 
                                    string.Equals(m.NumarInmatriculare, numarInmatriculare, StringComparison.OrdinalIgnoreCase));
    
                                if (masinaDeSters != null)
                                {
                                    masini.Remove(masinaDeSters);
                                    SalveazaMasiniInFisier(masini);
                                    Console.WriteLine($"Masina cu numarul de inmatriculare {numarInmatriculare} a fost stearsa.");
                                }
                                else
                                {
                                    Console.WriteLine($"Nu s-a gasit masina cu numarul de inmatriculare {numarInmatriculare}.");
                                }
                                break;
                            case "3":
                                exit1 = true;
                                break;
                        }
                    }
                    break;
                case "4":
                    if (!user.Logat)
                    {
                        Console.WriteLine("Trebuie sa fiti logat pentru a efectua urmatoarele actiuni!");
                        break;
                    }
                    
                    Console.WriteLine();
                    Console.WriteLine("1.Vizualizare masini disponibile pentru inchiriat");
                    Console.WriteLine("2.Inchiriere masina selectata");
                    Console.WriteLine("3.Inapoiere masină");
                    Console.WriteLine("Alegeti o optiune: ");
                    string optiune3 = Console.ReadLine();
                    switch (optiune3)
                    {
                        case "1":
                            Console.WriteLine();
                            Console.WriteLine("Masini pt inchiriere:");
                            int index = 1;
                            foreach (var masina in masini)
                            {
                                if (masina.Valabilitate)
                                {
                                    Console.Write($"{index}. ");
                                    masina.AfiseazaDateMasina();
                                    index++;
                                }
                            }
                            break;
                        case "2":
                            Console.WriteLine("Selectati numarul masinii pe care vreti sa o inchiriati ");
                            int numarulMasinii;
                            if (int.TryParse(Console.ReadLine(), out numarulMasinii))
                            {
                                var MasiniDisponibile = companie1.flota.Where(x => x.Valabilitate).ToList();
                                if (numarulMasinii > 0 && numarulMasinii <= MasiniDisponibile.Count)
                                {
                                    var DeInchiriat = MasiniDisponibile[numarulMasinii - 1];
                                    DeInchiriat.Valabilitate = false;
                                    Console.WriteLine("Vehicul inchiriat");
                                }
                                else
                                {
                                    Console.WriteLine("Numar invalid ");
                                }
                            }
                            else
                                {
                                    Console.WriteLine("Dati un numar valid");
                                }
                            break;
                        case "3":
                            Console.WriteLine("Doriti sa returnati masina inchiriata?");
                            string Confirmare  = Console.ReadLine();
                            if (Confirmare.ToLower() == "da")
                            {
                                var MasinaInchriata = companie1.flota.FirstOrDefault(x => !x.Valabilitate);
                                if (MasinaInchriata != null)
                                {
                                    MasinaInchriata.Valabilitate = true;
                                    Console.WriteLine("Masina a fost returnata cu succes!");
                                }
                                else
                                {
                                    Console.WriteLine("Nu ati inchirirat nicio masina.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Inapoiere invalida!");
                            }
                            break;
                        default:
                            Console.WriteLine("Optiune invalida");
                            break;
                            
                    }
                    break;
                case "5":
                    Console.WriteLine();
                    Console.WriteLine("ISTORIC INCHIRIERI ALE COMPANIEI: ");
                    int i = 1;
                    foreach (var inchiriere in companie1.inchiriate)
                    {
                        Console.WriteLine($"{i}. {companie1.inchiriate[i - 1]}");
                        i++;
                    }
                    break;
                case "6":
                    Console.WriteLine("Introdu numele userului: ");
                    string numele = Console.ReadLine();
                    var Gasit = User.users.FirstOrDefault(u => u.UsernameClient.ToLower() == numele.ToLower());
                    if (Gasit != null)
                    {
                        Gasit.DetaliiUser();
                    }
                    else
                    {
                       Console.WriteLine("Clientul nu a fost gasit");
                    }
                       
                    break;
                case "7":
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("optiune incorecta");
                    break;
            }
        }
        static void SalveazaMasiniInFisier(List<Masina> masini)
        {
            List<Masina> masiniFaraDuplicate = masini
                .GroupBy(m => m.NumarInmatriculare)
                .Select(g => g.First()) // Take the first car in each group of duplicates
                .ToList();

            var options = new JsonSerializerOptions
            {
                Converters = { new MasinaJsonConverter() },
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(masiniFaraDuplicate, options);
            File.WriteAllText(filePath, json);
        }

        static List<Masina> IncarcaMasiniDinFisier()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions
                {
                    Converters = { new MasinaJsonConverter() },
                    WriteIndented = true
                };
                return JsonSerializer.Deserialize<List<Masina>>(json, options) ?? new List<Masina>();
            }
            return new List<Masina>();
        }
    }
}