using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializePeople
{
    [Serializable]
    public class Person : IDeserializationCallback, ISerializable
    {
        protected string name;
        protected DateTime birthDate;
        [NonSerialized]
        private int age;
        private Gender gender;

        public Person()
        {

        }
        public Person(string name, DateTime birthDate, Gender gender)
        {
            this.name = name;
            this.Gender = gender;
            this.birthDate = birthDate;
            Age = DateTime.Today.Year - birthDate.Year;
        }

        public Person(
           SerializationInfo info,
           StreamingContext context)
        {
            name = (string)info.GetValue("name", typeof(string));
            birthDate = (DateTime)info.GetValue("birthDate", typeof(DateTime));
            gender = (Gender)info.GetValue("gender", typeof(Gender));
        }

        public int Age { get => age; set => age = value; }

        public Gender Gender { get => gender; set => gender = value; }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
            }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }

        }
        public override string ToString()
        {
            return $"Name: {name} Age: {Age} Gender: {Gender}";

        }

        public void Serialize(string output)
        {
            if (File.Exists(output))
            {
                File.Delete(output);
            }
            using (Stream stream = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
            }
        }

        public static Person Deserialize(string path)
        {
            Person person;
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                person = formatter.Deserialize(stream) as Person;
            }
            return person;
        }

        public override bool Equals(Object obj)
        {
            if (obj is Person)
            {
                var that = obj as Person;
                return this.Age == that.Age && this.Name == that.Name && this.Gender == that.Gender && that.BirthDate == that.BirthDate;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, birthDate, Age, Gender, BirthDate, Name);
        }

        public void OnDeserialization(object sender)
        {
            Age = DateTime.Today.Year - birthDate.Year;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", name, typeof(string));
            info.AddValue("birthDate", birthDate, typeof(DateTime));
            info.AddValue("gender", gender, typeof(Gender));
        }
    }
}
