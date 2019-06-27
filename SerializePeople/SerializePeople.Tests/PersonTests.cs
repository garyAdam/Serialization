using NUnit.Framework;
using SerializePeople;
namespace Tests
{
    public class PersonTests
    {
        private Person person;
        [SetUp]
        public void Setup()
        {
            person = new Person("John", new System.DateTime(1994, 2, 13), Gender.Male);
        }

        [Test]
        public void CreateTest()
        {
            Assert.AreEqual(25, person.Age);
        }

        [Test]
        public void ToStringTest()
        {
            Assert.AreEqual("Name: John Age: 25 Gender: Male", person.ToString());
        }

        [Test]
        public void SerializeTest()
        {
            person.Serialize("Person.bin");
            FileAssert.Exists("Person.bin");
        }

        [Test]
        public void DeserializeTest()
        {
            person.Serialize("Person.bin");
            Person person2 = Person.Deserialize("Person.bin");
            Assert.AreEqual(person, person2);
        }
    }
}