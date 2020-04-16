using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piłkarze.Model
{
    public class Model
    {
        private static string fileName = "File.txt";

        public List<Footballer> footballers { get; set; }

        public Model()
        {
            if (File.Exists(fileName))
            {
                var sfootballers = File.ReadAllText(fileName);
                footballers = JsonConvert.DeserializeObject<List<Footballer>>(sfootballers);
            }

            if (footballers == null)
            {
                footballers = new List<Footballer>();
            }
        }
        public Footballer addFootballer(string name, string surname,uint age, uint weight)
        {
            var id = footballers.Count()+1;
            var footballer = new Footballer(id, name, surname, age, weight);
            footballers.Add(footballer);
            WriteToFile();
            return footballer;
        }
        public void WriteToFile() 
        {
            string json = JsonConvert.SerializeObject(footballers, Formatting.Indented);
            using (StreamWriter stream = new StreamWriter(fileName))
            {
                stream.Write(json);
                stream.Close();
            }
        }

        internal void updateFootballer(int id, string firstname, string surname, uint age, uint weight)
        {
            var indexOfFootballer = footballers.FindIndex(x => x.Id == id);
            footballers[indexOfFootballer].FirstName = firstname;
            footballers[indexOfFootballer].Surname = surname;
            footballers[indexOfFootballer].Age = age;
            footballers[indexOfFootballer].Weight = weight;
            WriteToFile();
        }

        internal void deleteFootballer(int id)
        {
            var indexOfFootballer = footballers.FindIndex(x => x.Id == id);
            footballers.RemoveAt(indexOfFootballer);
            WriteToFile();
        }
    }
}
