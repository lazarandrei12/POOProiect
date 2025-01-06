using System.Diagnostics;
using Console = System.Console;

namespace Program.clase;

public class User
{
    public string UsenameAdmin = "admin";
    public string ParolaAdmin = "admin225200";
    private string UsernameClient = "doru";
    private string PasswordClient = "doru225200";
    public bool admin;
    public static List<User> users = new List<User>();

    public void user()
    {
       
    }
    public void Login()
    {
        Console.WriteLine("Introduceti username: ");
        string username = Console.ReadLine();
        Console.WriteLine("Introduceti parola: ");
        string parola = Console.ReadLine();
        
        if (username == UsernameClient && parola == ParolaAdmin)
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
        Console.WriteLine("Creeati username: ");
        string username = Console.ReadLine();
        Console.WriteLine("Creeati parola: ");
        string parola = Console.ReadLine();
        
        foreach (var user in users)
        {
            if (user.UsernameClient == username)
            {
                Console.WriteLine("Username deja utilizat!");
                return;
            }
        }
        users.Add(new User { UsernameClient = username, PasswordClient = parola, admin = false });
        Console.WriteLine("Cont creat");
    }

    public void AdaugaUtilizatori(List<User> users)
    {
        users.Add(new User{UsenameAdmin="admin",ParolaAdmin="admin225200",admin=true});
        users.Add(new User{UsernameClient = "doru",ParolaAdmin="admin225200",admin=false});
    }

    
}
