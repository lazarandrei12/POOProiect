using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Program.clase;

public class User
{
    private static string userFilePath = "users.json";
    [JsonIgnore]
    public string UsernameAdmin= "admin";
    [JsonIgnore]
    public string ParolaAdmin = "admin225200";
    public string UsernameClient { get; set; }
    public string ParolaClient { get; set; }
    public string Nume { get; set; }
    public string Cnp { get; set; }
    public bool Daune { get; set; } = false;
    public bool IsAdmin { get; set; }
    [JsonIgnore]
    public bool Logat { get; set; }
    [JsonIgnore]
    public static List<User> users = new List<User>();
    [JsonIgnore]
    public List<Inchirieri> IstoricInchirieri { get; set; } = new List<Inchirieri>();


    public User()
    {

    }

    public static void IncarcaUseriDinFisier()
    {
        if (File.Exists(userFilePath))
        {
            string json = File.ReadAllText(userFilePath);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            try
            {
                var useriIncarcati = JsonSerializer.Deserialize<List<UserDTO>>(json, options);
                if (useriIncarcati != null)
                {
                    users = useriIncarcati.Select(dto => new User
                    {
                        UsernameClient = dto.UsernameClient,
                        ParolaClient = dto.ParolaClient,
                        Nume = dto.Nume,
                        Cnp = dto.Cnp,
                        Daune = dto.Daune,
                        IsAdmin = dto.IsAdmin,
                        IstoricInchirieri = dto.IstoricInchirieri.Select(i => new Inchirieri(
                            new User { Nume = i.NumeClient, UsernameClient = i.UsernameClient },
                            new MasinaStandard(i.MarcaMasina, i.ModelMasina, 0, 0, i.NumarInmatriculare, true, 0),
                            DateOnly.Parse(i.DataInceput),
                            DateOnly.Parse(i.DataSfarsit),
                            i.PretTotal // Păstrează valoarea `pretTotal` din JSON
                        )).ToList()
                    }).ToList();
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Eroare la deserializarea utilizatorilor: {ex.Message}");
                users = new List<User>();
            }
        }
        else
        {
            users = new List<User>();
        }
    }

    public static void SalveazaUseriInFisier()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        var useriPentruSalvare = users.Select(u => new UserDTO
        {
            UsernameClient = u.UsernameClient,
            ParolaClient = u.ParolaClient,
            Nume = u.Nume,
            Cnp = u.Cnp,
            Daune = u.Daune,
            IsAdmin = u.IsAdmin,
            IstoricInchirieri = u.IstoricInchirieri.Select(i => new InchiriereDTO
            {
                NumeClient = i.User.Nume,
                UsernameClient = i.User.UsernameClient,
                MarcaMasina = i.masina.Marca,
                ModelMasina = i.masina.Model,
                NumarInmatriculare = i.masina.NumarInmatriculare,
                DataInceput = i.InceputInchiriere.ToString(),
                DataSfarsit = i.FinalInchiriere.ToString(),
                PretTotal = i.pretTotal
            }).ToList()
        }).ToList();
        
        string json = JsonSerializer.Serialize(useriPentruSalvare, options);
        File.WriteAllText(userFilePath, json);
    }
    public void Login()
    {
        IncarcaUseriDinFisier();
        Console.WriteLine("Introduceti username: ");
        string username = Console.ReadLine();
        Console.WriteLine("Introduceti parola: ");
        string parola = Console.ReadLine();

        if (username == UsernameAdmin && parola == ParolaAdmin)
        {
            Console.WriteLine("Ai intrat in cont de administrator");
            IsAdmin = true;
            return;
        }

        var userCurent = users.FirstOrDefault(u => 
            u.UsernameClient == username && u.ParolaClient == parola);

        if (userCurent != null)
        {
            Console.WriteLine($"Ai intrat in cont! {userCurent.UsernameClient}");
            this.UsernameClient = userCurent.UsernameClient;
            this.ParolaClient = userCurent.ParolaClient;
            this.Nume = userCurent.Nume;
            this.Cnp = userCurent.Cnp;
            this.IsAdmin = userCurent.IsAdmin;
            this.IstoricInchirieri = userCurent.IstoricInchirieri;
            this.Logat = true;
            return;
        }

        Console.WriteLine("Username sau parola incorecte ");
        Console.WriteLine("Vrei sa iti creezi cont? (Da/Nu)");
        string raspuns = Console.ReadLine();
        if (raspuns.ToLower() == "da")
        {
            SignUp();
        }
    }
    public void AdaugaIstoricInchirieri(Inchirieri inchirieri)
    {
        if (IstoricInchirieri == null)
        {
            IstoricInchirieri = new List<Inchirieri>();
        }
        IstoricInchirieri.Add(inchirieri);
    }
    private class UserDTO
    {
        public string UsernameClient { get; set; }
        public string ParolaClient { get; set; }
        public string Nume { get; set; }
        public string Cnp { get; set; }
        public bool Daune { get; set; }
        public bool IsAdmin { get; set; }
        public List<InchiriereDTO> IstoricInchirieri { get; set; } = new List<InchiriereDTO>();
    }
    public void SignUp()
    {
        IncarcaUseriDinFisier();

        Console.WriteLine("Creeare cont");
        Console.WriteLine("Introduceti username nou: ");
        string username = Console.ReadLine();
        Console.WriteLine("Introduceti parola: ");
        string parola = Console.ReadLine();
        
        foreach (var user in users)
        {
            if (user.UsernameClient == username)
            {
                Console.WriteLine("Username deja utilizat!");
                return;
            }
        }

        Console.WriteLine("Introduceti datele personale: ");
        Console.WriteLine("Numele dumneavoastra:");
        string nume = Console.ReadLine();
        string cnp = "";
        bool validCNP = false;
        
        while (!validCNP)
        {
            Console.WriteLine("CNP-ul dumneavoastra: ");
            cnp = Console.ReadLine();
            if (cnp.Length != 13)
            { 
                Console.WriteLine("CNP-ul dumneavoastra invalid");
            }
            else
            {
                validCNP = true;
                Console.WriteLine($"Cont creat\n Bun venit la PipepepuRent ");
                Logat = true;

                User newUser = new User
                {
                    UsernameClient = username,
                    ParolaClient = parola,
                    Nume = nume,
                    Cnp = cnp,
                    IsAdmin = false,
                };
                users.Add(newUser);
                
                SalveazaUseriInFisier();
                break;
            }
        }
    }


    public void DetaliiUser()
    {
        Console.WriteLine("Detalii cont");
        Console.WriteLine($"Username: {UsernameClient}");
        Console.WriteLine($"Nume: {Nume}");
        Console.WriteLine($"Numarul de inchirieri: {IstoricInchirieri.Count}");
        if (IstoricInchirieri.Count > 0)
        {
            Console.WriteLine("Istoric inchiriei:");
            foreach (var inchirierile in IstoricInchirieri)
            {
                inchirierile.AfiseazaDetalii();
            }
        }
        else
        {
            Console.WriteLine("Nu exista inchirieri pe acest nume");
        }
    }
}
