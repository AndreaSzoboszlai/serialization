using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SerializePeople
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person("A", DateTime.Now, Person.Genders.female);
            p.Serialize(@"C:\Users\szobo\Documents\temp.txt");
            p.DeSerialize(@"C:\Users\szobo\Documents\temp.txt");

            string fileName = @"C:\Users\szobo\Documents\tempFile.txt";

            // Use a BinaryFormatter or SoapFormatter.
            IFormatter formatter = new BinaryFormatter();
            //IFormatter formatter = new SoapFormatter();

            SerializeItem(fileName, formatter); // Serialize an instance of the class.
            DeserializeItem(fileName, formatter); // Deserialize the instance.
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void SerializeItem(string fileName, IFormatter formatter)
        {
            // Create an instance of the type and serialize it.
            Person person = new Person("Géza2", DateTime.Parse("1981.06.16."), Person.Genders.male); 

            FileStream stream = new FileStream(fileName, FileMode.Create);
            formatter.Serialize(stream, person);
            stream.Close();
        }


        public static void DeserializeItem(string fileName, IFormatter formatter)
        {
            FileStream s = new FileStream(fileName, FileMode.Open);
            Person person = (Person)formatter.Deserialize(s);
            Console.WriteLine(person.name);
            Console.WriteLine(person.birthday);
            Console.WriteLine(person.gender);
            Console.WriteLine(person.age);
        }
    }
}
