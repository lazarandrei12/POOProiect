using System.Diagnostics;
using System.Collections.Generic;
using Console = System.Console;

namespace Program.clase;

public class User
{
    public string UsernameAdmin= "admin";
    public string ParolaAdmin = "admin225200";
    public string UsernameClient;
    public string ParolaClient;
    public string Nume;
    public string Cnp;
    public bool IsAdmin;
    public bool Logat;
    public static List<User> users = new List<User>();

    public User()
    {
        
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

    public void SignUp()
    {
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
            }
                

            User newUser = new User
            {
                UsernameClient = username, ParolaClient = parola, Nume = nume, Cnp = cnp, IsAdmin = false
            };
            users.Add(newUser);
        }
    }
    
}
