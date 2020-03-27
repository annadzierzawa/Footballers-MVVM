using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piłkarze
{
    class FootballersRepository
    {
        private static string file = "File.txt";
        public static  Footballer CreateStringFromFilrFormat(string sfootballer)
        {
            string firstname;
            string surname;
            uint weight, age;
            var properties = sfootballer.Split('|');
            if (properties.Length == 4)
            {
                surname = properties[1];
                firstname = properties[0];
                age = uint.Parse(properties[2]);
                weight = uint.Parse(properties[3]);
                return new Footballer(surname, firstname, age,weight);
            }
            throw new Exception("Wrong File Format!!!");
        }
        public static Footballer[] ReadFromFile()
        {
            Footballer[] footballers = null;
            if (File.Exists(file))
            {
                var sfootballers = File.ReadAllLines(file);
                var n = sfootballers.Length;
                if (n > 0)
                {
                    footballers = new Footballer[n];
                    for (int i = 0; i < n; i++)
                        footballers[i] = FootballersRepository.CreateStringFromFilrFormat(sfootballers[i]);
                    return footballers;
                }
            }
            return footballers;
        }
        public static string ToFileFormat(Footballer footballer)
        {
            return $"{footballer.Surname}|{footballer.FirstName}|{footballer.Age}|{footballer.Weight}";
        }
        public static void WriteToFile(Footballer[] footballers)
        {
            using (StreamWriter stream = new StreamWriter(file))
            {
                foreach (var p in footballers)
                    stream.WriteLine(ToFileFormat(p));
                stream.Close();
            }
        }
    }
}
