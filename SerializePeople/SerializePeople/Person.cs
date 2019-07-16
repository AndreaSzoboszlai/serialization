using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializePeople
{
    [Serializable]
    class Person : IDeserializationCallback, ISerializable
    {
        [NonSerialized]
        public int age;

        public string name { get; set; }
        public DateTime birthday { get; set; }
        internal Genders gender { get; set; }

        public Person(string name, DateTime birthday, Genders gender)
        {
            this.name = name;
            this.birthday = birthday;
            this.gender = gender;
            CalculateAge();
        }

        private void CalculateAge()
        {
            age = (DateTime.Today).Year - birthday.Year;
        }

        public void Serialize(string output)
        {
            DateTime parsedDate = DateTime.Parse("1991.06.16.");
            Person person = new Person("Géza", parsedDate, Genders.male);
            //File.Create(output);
            Stream stream = File.Open(output, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, person);
            stream.Close();
            // Create file to save the data 
            // Create and use a BinaryFormatter object to perform the serialization 
            // Close the file 
        }

        public void DeSerialize(string output)
        {
            Stream stream = new FileStream(output, FileMode.Open, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            Person newPerson = (Person)formatter.Deserialize(stream);

            Console.WriteLine(newPerson.name);
            Console.WriteLine(newPerson.gender);
            Console.WriteLine(newPerson.age);
            Console.ReadKey();
        }

        public override string ToString()
        {
            return "Person: " + name + " " + birthday + " " + gender;
        }

        public void OnDeserialization(object sender)
        {
            age = (DateTime.Today).Year - birthday.Year;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", name);
            info.AddValue("birthday", birthday);
            info.AddValue("gender", gender);
        }

        public Person(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            name = (string)info.GetValue("name", typeof(string));
            birthday = (DateTime)info.GetValue("birthday", typeof(DateTime));
            gender = (Genders)Enum.Parse(typeof(Genders), info.GetString("gender"));
        }

        public enum Genders
        {
            male = 0,
            female = 1
        }
    }
}
