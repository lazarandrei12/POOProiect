using System.Diagnostics;
using Console = System.Console;

namespace Program.clase;

public class User
{
    public string UsenameAdmin = "admin";
    public string ParolaAdmin = "admin225200";
    private string UsernameClient = "doru";
    private string PasswordClient = "doru225200";
    public string nume;
    public string cnp;
    public bool admin;
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
        
        if (username == UsenameAdmin && parola == ParolaAdmin)
        {
            Console.WriteLine("Ai intrat in cont de administrator");
            admin = true;
            return;
        }
        foreach (var user in users)
        {
            if (user.UsernameClient == username && user.PasswordClient == parola)
            {
                Console.WriteLine("Ai intrat in cont! ");
                admin = false;
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
    }

    public void SignUp()
    {
        Console.WriteLine("Creeare cont");
        Console.WriteLine("Introduceti username: ");
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

            try
            {
                if (cnp.Length != 13)
                {
                    Console.WriteLine("CNP-ul dumneavoastra invalid");
                }
                else
                {
                    validCNP = true;
                    Console.WriteLine("Cont creat");
                }
                

                User newUser = new User
                    { UsernameClient = username, PasswordClient = parola, nume = nume, cnp = cnp, admin = false };
                users.Add(newUser);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
    
}
