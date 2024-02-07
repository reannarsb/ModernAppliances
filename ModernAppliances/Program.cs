using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<Appliance> appliances = ReadDataFromFile("appliances.txt");
        DisplayMenu(appliances);
    }

    static List<Appliance> ReadDataFromFile(string filename)
    {
        List<Appliance> appliances = new List<Appliance>();
        try
        {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] data = line.Split(';');
                string itemNumber = data[0];
                string brand = data[1];
                Appliance appliance = null;
                if (itemNumber.StartsWith("1"))
                {
                    appliance = CreateRefrigerator(data);
                }
                else if (itemNumber.StartsWith("2"))
                {
                    appliance = CreateVacuum(data);
                }
                else if (itemNumber.StartsWith("3"))
                {
                    appliance = CreateMicrowave(data);
                }
                else if (itemNumber.StartsWith("4") || itemNumber.StartsWith("5"))
                {
                    appliance = CreateDishwasher(data);
                }
                if (appliance != null)
                {
                    appliances.Add(appliance);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
        return appliances;
    }

    static Refrigerator CreateRefrigerator(string[] data)
    {
        Refrigerator refrigerator = new Refrigerator();
        refrigerator.ItemNumber = data[0];
        refrigerator.Brand = data[1];
        refrigerator.Quantity = int.Parse(data[2]);
        refrigerator.Wattage = int.Parse(data[3]);
        refrigerator.Color = data[4];
        refrigerator.Price = double.Parse(data[5]);
        refrigerator.NumberOfDoors = int.Parse(data[6]);
        refrigerator.Height = int.Parse(data[7]);
        refrigerator.Width = int.Parse(data[8]);
        return refrigerator;
    }

    static Vacuum CreateVacuum(string[] data)
    {
        Vacuum vacuum = new Vacuum();
        vacuum.ItemNumber = data[0];
        vacuum.Brand = data[1];
        vacuum.Quantity = int.Parse(data[2]);
        vacuum.Wattage = int.Parse(data[3]);
        vacuum.Color = data[4];
        vacuum.Price = double.Parse(data[5]);
        vacuum.Grade = data[6];
        vacuum.BatteryVoltage = int.Parse(data[7]);
        return vacuum;
    }

    static Microwave CreateMicrowave(string[] data)
    {
        Microwave microwave = new Microwave();
        microwave.ItemNumber = data[0];
        microwave.Brand = data[1];
        microwave.Quantity = int.Parse(data[2]);
        microwave.Wattage = int.Parse(data[3]);
        microwave.Color = data[4];
        microwave.Price = double.Parse(data[5]);
        microwave.Capacity = double.Parse(data[6]);
        microwave.RoomType = data[7];
        return microwave;
    }

    static Dishwasher CreateDishwasher(string[] data)
    {
        Dishwasher dishwasher = new Dishwasher();
        dishwasher.ItemNumber = data[0];
        dishwasher.Brand = data[1];
        dishwasher.Quantity = int.Parse(data[2]);
        dishwasher.Wattage = int.Parse(data[3]);
        dishwasher.Color = data[4];
        dishwasher.Price = double.Parse(data[5]);
        dishwasher.SoundRating = data[6];
        dishwasher.Feature = data[7];
        return dishwasher;
    }

    static void DisplayMenu(List<Appliance> appliances)
    {
        int option;
        do
        {
            Console.WriteLine("Welcome to Modern Appliances!");
            Console.WriteLine("How may we assist you?");
            Console.WriteLine("1 - Check out appliance");
            Console.WriteLine("2 - Find appliances by brand");
            Console.WriteLine("3 - Display appliances by type");
            Console.WriteLine("4 - Produce random appliance list");
            Console.WriteLine("5 - Save & exit");
            Console.Write("Enter option: ");

            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        PurchaseAppliance(appliances);
                        break;
                    case 2:
                        FindAppliancesByBrand(appliances);
                        break;
                    case 3:
                        DisplayAppliancesByType(appliances);
                        break;
                    case 4:
                        ProduceRandomApplianceList(appliances);
                        break;
                    case 5:
                        SaveDataToFile("appliances.txt", appliances);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid option. Please enter a number.");
            }
        } while (option != 5);
    }

    static void PurchaseAppliance(List<Appliance> appliances)
    {
        Console.WriteLine("Enter the item number of an appliance:");
        string itemNumber = Console.ReadLine();
        Appliance appliance = appliances.FirstOrDefault(a => a.ItemNumber == itemNumber);
        if (appliance != null)
        {
            if (appliance.Quantity > 0)
            {
                appliance.Quantity--;
                Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
            }
            else
            {
                Console.WriteLine("The appliance is not available to be checked out.");
            }
        }
        else
        {
            Console.WriteLine("No appliances found with that item number.");
        }
    }

    static void FindAppliancesByBrand(List<Appliance> appliances)
    {
        Console.WriteLine("Enter brand to search for:");
        string brand = Console.ReadLine();
        var matchingAppliances = appliances.Where(a => a.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase));
        if (matchingAppliances.Any())
        {
            Console.WriteLine("\nMatching Appliances:");
            foreach (var appliance in matchingAppliances)
            {
                Console.WriteLine(appliance);
            }
        }
        else
        {
            Console.WriteLine("No appliances found with that brand.");
        }
    }

    static void DisplayAppliancesByType(List<Appliance> appliances)
    {
        Console.WriteLine("Appliance Types");
        Console.WriteLine("1 - Refrigerators");
        Console.WriteLine("2 - Vacuums");
        Console.WriteLine("3 - Microwaves");
        Console.WriteLine("4 - Dishwashers");
        Console.WriteLine("\nEnter type of appliance:");
        if (int.TryParse(Console.ReadLine(), out int type))
        {
            var matchingAppliances = appliances.Where(a => GetApplianceType(a) == type);
            if (matchingAppliances.Any())
            {
                Console.WriteLine("\nMatching Appliances:");
                foreach (var appliance in matchingAppliances)
                {
                    Console.WriteLine(appliance);
                }
            }
            else
            {
                Console.WriteLine("No appliances found for the selected type.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static int GetApplianceType(Appliance appliance)
    {
        if (appliance is Refrigerator)
        {
            return 1;
        }
        else if (appliance is Vacuum)
        {
            return 2;
        }
        else if (appliance is Microwave)
        {
            return 3;
        }
        else if (appliance is Dishwasher)
        {
            return 4;
        }
        return 0;
    }

    static void ProduceRandomApplianceList(List<Appliance> appliances)
    {
        Console.WriteLine("Enter number of random appliances to display:");
        if (int.TryParse(Console.ReadLine(), out int count))
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int index = rand.Next(appliances.Count);
                Console.WriteLine(appliances[index]);
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static void SaveDataToFile(string filename, List<Appliance> appliances)
    {
        try
        {
            List<string> lines = new List<string>();
            foreach (var appliance in appliances)
            {
                lines.Add(appliance.ToString());
            }
            File.WriteAllLines(filename, lines);
            Console.WriteLine("Appliances data saved to file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
        }
    }
}

class Appliance
{
    public string ItemNumber { get; set; }
    public string Brand { get; set; }
    public int Quantity { get; set; }
    public int Wattage { get; set; }
    public string Color { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return $"ItemNumber: {ItemNumber}\nBrand: {Brand}\nQuantity: {Quantity}\nWattage: {Wattage}\nColor: {Color}\nPrice: {Price}";
    }
}

class Refrigerator : Appliance
{
    public int NumberOfDoors { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    public override string ToString()
    {
        return base.ToString() + $"\nNumberOfDoors: {NumberOfDoors}\nHeight: {Height}\nWidth: {Width}";
    }
}

class Vacuum : Appliance
{
    public string Grade { get; set; }
    public int BatteryVoltage { get; set; }

    public override string ToString()
    {
        return base.ToString() + $"\nGrade: {Grade}\nBatteryVoltage: {BatteryVoltage}";
    }
}

class Microwave : Appliance
{
    public double Capacity { get; set; }
    public string RoomType { get; set; }

    public override string ToString()
    {
        return base.ToString() + $"\nCapacity: {Capacity}\nRoomType: {RoomType}";
    }
}

class Dishwasher : Appliance
{
    public string SoundRating { get; set; }
    public string Feature { get; set; }

    public override string ToString()
    {
        return base.ToString() + $"\nSoundRating: {SoundRating}\nFeature: {Feature}";
    }
}


