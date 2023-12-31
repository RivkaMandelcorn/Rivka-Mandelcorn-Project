using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace DataBaseC_Project
{
    enum GenderEnum
    {
        Male, Female
    }
    class Person
    {
        public string firstname {  get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public int weight {  get; set; }
        public GenderEnum gender { get; set; }
    }
    class Program
    {
        public static void createCSV()
        {
            string csvFilePath = "../../../DB/dataset.csv";
            string[] firstNames = { "Sara", "Rivka", "Rachel", "Lea", "Avraham", "Ytzchok", "Yaakov","John", "Jane", "Michael", "David" };
            string[] lastNames = { "Cohen", "Levi", "Ysrael", "Mandel", "Smith", "Johnson", "Williams", "Brown", "Doe", "Miller" };
            GenderEnum[] genders = { GenderEnum.Male, GenderEnum.Female };

            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("First Name,Last Name,Age,Weight (lbs),Gender");

            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                string firstName = firstNames[random.Next(firstNames.Length)];
                string lastName = lastNames[random.Next(lastNames.Length)];
                int age = random.Next(18, 71);
                int weight = random.Next(15,300);
                GenderEnum gender = genders[random.Next(genders.Length)];
                csvData.AppendLine($"{firstName},{lastName},{age},{weight},{gender}");
            }

            File.WriteAllText(csvFilePath, csvData.ToString());

            Console.WriteLine("Dataset generated successfully!");
        }
    static void Main()
    {
            createCSV();
            List<string> lines = File.ReadAllLines("../../../DB/dataset.csv").Skip(1).ToList();
            List<Person> allPeople = new List<Person>();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] values = lines[i].Split(',');
                string firstname = values[0];
                string lastname = values[1];
                int age = int.Parse(values[2]);
                int weight = int.Parse(values[3]);
                string genderString = values[4];

                GenderEnum gender;
                Enum.TryParse(genderString, out gender);

                Person person = new Person()
                {
                    firstname = firstname,
                    lastname = lastname,
                    weight = weight,
                    age = age,
                    gender = gender
                };
                if (i < 5)
                    Console.WriteLine("firstname:"+firstname+",lastname:"+lastname+",weight:"+weight+",age:"+age+",gender:"+gender);

                allPeople.Add(person);
            }

            double averageAge = allPeople.Average(person=>person.age);
            int peopleCount = allPeople.Count(person => person.weight >= 120 && person.weight <= 140);
            double averageAgeInRange = allPeople.Where(person => person.weight >= 120 && person.weight <= 140).Average(person => person.age);


            Console.WriteLine($"Average Age of all people: {averageAge}");
            Console.WriteLine($"Total number of people weighing between 120lbs and 140lbs: {peopleCount}");
            Console.WriteLine($"Average Age of people weighing between 120lbs and 140lbs: {averageAgeInRange}");

        }

    }
}
