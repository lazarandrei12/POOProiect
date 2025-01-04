using System.Text.RegularExpressions;

namespace Program.clase;

public class car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int YearOfFabrication { get; set; }
    public int Mileage { get; set; }
    public string PlateNumber { get; set; }
    public car (string brand, string model, int yearOfFabrication, int mileage, string plateNumber)
    {
        this.Brand = brand;
        this.Model = model;
        this.YearOfFabrication = yearOfFabrication;
        this.Mileage = mileage;
        this.PlateNumber = plateNumber;
        
        if (IsValidPlateNumber(plateNumber))
        {
            Console.WriteLine("Valid plate number.");
        }
        else
        {
            throw new Exception("Invalid plate number.");
        }
    }
    static bool IsValidPlateNumber(string plateNumber)
    {
        HashSet<string> validPrefixes = new HashSet<string>
        {
            "B", "AB", "AG", "AR", "BC", "BH", "BN", "BR", "BT", "BV", "BZ",
            "CJ", "CL", "CS", "CT", "CV", "DB", "DJ", "GJ", "GL", "GR", "HD",
            "HR", "IF", "IL", "IS", "MH", "MM", "MS", "NT", "OT", "PH", "SB",
            "SJ", "SM", "SV", "TL", "TM", "TR", "VL", "VN", "VS"
        };

        string pattern = @"^([A-Z]{1,2})-(\d{2})-([A-Z]{3})$";
        Match match = Regex.Match(plateNumber, pattern);

        if (!match.Success)
        {
            return false;
        }

        string prefix = match.Groups[1].Value;
        string numbers = match.Groups[2].Value;
        string suffix = match.Groups[3].Value;

        if (!validPrefixes.Contains(prefix))
        {
            return false;
        }

        return true;
    }

}