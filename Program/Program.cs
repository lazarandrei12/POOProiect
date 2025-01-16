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
    static string userFilePath = "users.json";
    
    static string masinaFilePath = "masini.json";
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
            IsAdmin = false
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
        User user = new User();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Bun venit la PipepepuRent");
            Console.WriteLine("1.Vezi masini pt inchiriere");
            Console.WriteLine("2.Log in/Sign in");
            Console.WriteLine("3.Pentru Adminisrator");
            Console.WriteLine("4.Pentru Client");
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
                    int index1 = 1;
                    foreach (var masina in masini)
                    {
                        if (masina.Valabilitate)
                        {
                            Console.Write($"{index1}. ");
                            masina.AfiseazaDateMasina();
                            index1++;
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
                    Console.WriteLine("3.Inapoiere masina");
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
                            if (user.Daune)
                            {
                                Console.WriteLine("Accesul la inchirierea unei masini este interzis pentru dumneavoastra, deoarece ati cauzat daune in trecut.");
                                break;
                            }
                            Console.WriteLine("Selectati numarul masinii pe care vreti sa o inchiriati ");
                            int numarulMasinii;
                            if (int.TryParse(Console.ReadLine(), out numarulMasinii))
                            {
                                var MasiniDisponibile = masini.Where(x => x.Valabilitate).ToList();
                                if (numarulMasinii > 0 && numarulMasinii <= MasiniDisponibile.Count)
                                {
                                    var DeInchiriat = MasiniDisponibile[numarulMasinii - 1];
                                    
                                    Console.WriteLine("Introduceti data de inceput (format: YYYY-MM-DD): ");
                                    if (DateOnly.TryParse(Console.ReadLine(), out DateOnly dataInceput))
                                    {
                                        Console.WriteLine("Introduceti data de sfarsit (format: YYYY-MM-DD): ");
                                        if (DateOnly.TryParse(Console.ReadLine(), out DateOnly dataSfarsit))
                                        {
                                            var inchiriereNoua = new Inchirieri(user, DeInchiriat, dataInceput, dataSfarsit);
                                            
                                            DeInchiriat.Valabilitate = false;
                                            SalveazaMasiniInFisier(masini);
                                            
                                            companie1.AdaugaInchiriere(inchiriereNoua);
                                            
                                            user.AdaugaIstoricInchirieri(inchiriereNoua);
                                            Console.WriteLine("Istoric inainte de salvare:");
                                            foreach (var inchiriere in user.IstoricInchirieri)
                                            {
                                                Console.WriteLine(inchiriere);
                                            }
                                            User.SalveazaUseriInFisier();
                                            
                                            var inchirieriExistenteV2 = Inchirieri.IncarcaInchirieriDinFisier();
                                            inchirieriExistenteV2.Add(inchiriereNoua);
                                            Inchirieri.SalveazaInchirieriInFisier(inchirieriExistenteV2);
                                            
                                            Console.WriteLine("Vehicul inchiriat cu succes!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Format data invalid pentru data de sfarsit!");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Format data invalid pentru data de inceput!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Numar invalid de masina!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Va rugam introduceti un numar valid!");
                            }
                            break;
                        
                        case "3":
                            Console.WriteLine("Introduceti numarul de inmatriculare al masinii pe care doriti sa o returnati:");
                            string numarInmatriculare = Console.ReadLine();
                            
                            var inchiriereaGasita = user.IstoricInchirieri.FirstOrDefault(i =>
                                i.masina.NumarInmatriculare.Equals(numarInmatriculare, StringComparison.OrdinalIgnoreCase));

                            if (inchiriereaGasita == null)
                            {
                                Console.WriteLine("Nu aveti inchirierea acestei masini sau masina nu exista.");
                                break;
                            }

                            var masinaDeReturnat = masini.FirstOrDefault(m =>
                                m.NumarInmatriculare.Equals(numarInmatriculare, StringComparison.OrdinalIgnoreCase));

                            if (masinaDeReturnat == null)
                            {
                                Console.WriteLine("Masina cu acest numar de inmatriculare nu a fost gasita.");
                                break;
                            }

                            if (masinaDeReturnat.Valabilitate)
                            {
                                Console.WriteLine("Aceasta masina este deja returnata si disponibila pentru inchiriere.");
                                break;
                            }

                            masinaDeReturnat.Valabilitate = true;
                            SalveazaMasiniInFisier(masini);

                            Console.WriteLine("Masina a fost returnata cu succes.");
                            Console.WriteLine("A existat vreo dauna? (Da/Nu)");

                            string raspunsDauna = Console.ReadLine()?.ToLower();
                            if (raspunsDauna == "da")
                            {
                                user.Daune = true;
                                Console.WriteLine("Utilizatorul a fost marcat ca avand daune si nu mai poate inchiria alte masini.");
                            }
                            
                            User.SalveazaUseriInFisier();
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
                    User.IncarcaUseriDinFisier();
                    var Gasit = User.users.FirstOrDefault(u => u.UsernameClient.Equals(numele, StringComparison.OrdinalIgnoreCase));
                    if (Gasit != null)
                    {
                        Console.WriteLine("Detalii cont");
                        Console.WriteLine($"Username: {Gasit.UsernameClient}");
                        Console.WriteLine($"Nume: {Gasit.Nume}");
                        Console.WriteLine($"Numarul de inchirieri: {Gasit.IstoricInchirieri.Count}");

                        if (Gasit.IstoricInchirieri.Count > 0)
                        {
                            Console.WriteLine("Istoric inchirieri:");
                            foreach (var inchiriere in Gasit.IstoricInchirieri)
                            {
                                inchiriere.AfiseazaDetalii();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nu exista inchirieri pe acest nume");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Clientul nu a fost gasit");
                    }
                    break;
                case "7":
                    Console.WriteLine("Introduceti data pentru care doriti sa calculati costurile de inchiriere (format: YYYY-MM-DD): ");
                    string dataString = Console.ReadLine();

                    if (!DateOnly.TryParse(dataString, out DateOnly dataCautata))
                    {
                        Console.WriteLine("Format invalid pentru data!");
                        break;
                    }

                    var inchirieriExistente = Inchirieri.IncarcaInchirieriDinFisier();

                    var inchirieriActive = inchirieriExistente
                        .Where(i => i.InceputInchiriere <= dataCautata && dataCautata <= i.FinalInchiriere)
                        .ToList();

                    if (!inchirieriActive.Any())
                    {
                        Console.WriteLine($"Nu exista inchirieri active pentru data {dataCautata}.");
                        break;
                    }

                    double sumaTotala = 0;
                    foreach (var inchiriere in inchirieriActive)
                    {
                        int durata = (inchiriere.FinalInchiriere.DayNumber - inchiriere.InceputInchiriere.DayNumber);

                        if (durata > 0)
                        {
                            double costPeZi = inchiriere.pretTotal / durata;
                            sumaTotala += costPeZi;

                            Console.WriteLine($"Inchiriere: {inchiriere.User.Nume}, Masina: {inchiriere.masina.Marca}, Cost/zi: {costPeZi} lei.");
                        }
                        else
                        {
                            Console.WriteLine($"Inchirierea cu masina {inchiriere.masina.Marca} are o durata invalida.");
                        }
                    }

                    Console.WriteLine($"Suma totala a costurilor de inchiriere pe zi pentru data {dataCautata}: {sumaTotala} lei.");
                    break;
                case "8":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Optiune incorecta!");
                    break;
            }
        }
        static void SalveazaMasiniInFisier(List<Masina> masini)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var masiniPentruSalvare = masini.Select(masina =>
            {
                if (masina is MasinaStandard standard)
                {
                    return new
                    {
                        TypeDiscriminator = "MasinaStandard",
                        standard.Marca,
                        standard.Model,
                        standard.AnDeFabricatie,
                        standard.Kilometraj,
                        standard.NumarInmatriculare,
                        standard.Valabilitate,
                        standard.CostBaza
                    };
                }
                else if (masina is MasinaElectric electric)
                {
                    return new
                    {
                        TypeDiscriminator = "MasinaElectric",
                        electric.Marca,
                        electric.Model,
                        electric.AnDeFabricatie,
                        electric.Kilometraj,
                        electric.NumarInmatriculare,
                        electric.Valabilitate,
                        electric.CostBaza
                    };
                }

                throw new InvalidOperationException("Tip necunoscut de masina.");
            }).ToList();
            
            string json = JsonSerializer.Serialize(masiniPentruSalvare, options);
            File.WriteAllText("masini.json", json);
        }

        static List<Masina> IncarcaMasiniDinFisier()
        {
            if (File.Exists("masini.json"))
            {
                string json = File.ReadAllText("masini.json");
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                var masiniDTO = JsonSerializer.Deserialize<List<JsonElement>>(json, options);

                if (masiniDTO != null)
                {
                    var masini = new List<Masina>();
                    foreach (var masina in masiniDTO)
                    {
                        string typeDiscriminator = masina.GetProperty("TypeDiscriminator").GetString();
                        if (typeDiscriminator == "MasinaStandard")
                        {
                            masini.Add(masina.Deserialize<MasinaStandard>(options));
                        }
                        else if (typeDiscriminator == "MasinaElectric")
                        {
                            masini.Add(masina.Deserialize<MasinaElectric>(options));
                        }
                    }
                    return masini;
                }
            }
            return new List<Masina>();
        }
    }
}