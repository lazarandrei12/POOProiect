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

    private static void IncarcaUseriDinFisier()
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
                        IsAdmin = dto.IsAdmin,
                    }).ToList();
                }
            }
            catch (JsonException)
            {
                users = new List<User>();
            }
        }
    }

    private static void SalveazaUseriInFisier()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        var useriPentruSalvare = users.Select(u => new
        {
            u.UsernameClient,
            u.ParolaClient,
            u.Nume,
            u.Cnp,
            u.IsAdmin
        }).ToList();
        
        string json = JsonSerializer.Serialize(useriPentruSalvare, options);
        File.WriteAllText(userFilePath, json);
    }
    public void Login()
    {
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

        foreach (var user in users)
        {
            if (user.UsernameClient == username && user.ParolaClient == parola)
            {
                Console.WriteLine($"Ai intrat in cont! {user.UsernameClient} ");
                IsAdmin = false;
                Logat = true;
                return;
            }
        }

        Console.WriteLine("Username sau parola incorecte ");
        Console.WriteLine("Vrei sa iti creezi cont? (Da/Nu)");
        string raspuns = Console.ReadLine();
        if (raspuns.ToLower() == "da")
        {
            SignUp();
        }

        if (raspuns.ToLower() == "nu")
        {
            return;
        }
    }

    public bool PoateInchiria()
    {
        return !IstoricInchirieri.Any(i => i.Daune);
    }

    public void AdaugaIstoricInchirieri(Inchirieri inchirieri)
    {
        IstoricInchirieri.Add(inchirieri);
    }
    private class UserDTO
    {
        public string UsernameClient { get; set; }
        public string ParolaClient { get; set; }
        public string Nume { get; set; }
        public string Cnp { get; set; }
        public bool IsAdmin { get; set; }
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
