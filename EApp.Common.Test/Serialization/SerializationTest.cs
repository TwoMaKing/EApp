using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EApp.Common.Serialization;
using System.Text;

namespace EApp.Common.Test.Serialization
{
    [TestClass]
    public class SerializationTest
    {
        Person p = new Person(1, "Jason", "xzjcool@live.com");

        [TestMethod]
        public void UnicodeSerializtion_Serialize_Test()
        {
            UnicodeSerialization unicodeSerialize = new UnicodeSerialization();
            byte[] result = unicodeSerialize.Serialize<Person>(p);
            Assert.IsNotNull(result, "序列化失败");
        }

        [TestMethod]
        public void UnicodeSerializtion_DeSerialize_Test()
        {
            UnicodeSerialization unicodeSerialize = new UnicodeSerialization();
            byte[] bytes = unicodeSerialize.Serialize<Person>(p);
            Person p1 = unicodeSerialize.DeSerialize<Person>(bytes);
            Assert.IsNotNull(p1, "序列化失败");
        }

        [TestMethod]
        public void XmlSerializtion_Serialize_Test()
        {
            XMLSerialization xmlSerialize = new XMLSerialization();
            byte[] result = xmlSerialize.Serialize<Person>(p);
            Assert.IsNotNull(result, "序列化失败");
        }

        [TestMethod]
        public void XmlSerializtion_DeSerialize_Test()
        {
            XMLSerialization xmlSerialize = new XMLSerialization();
            byte[] bytes = xmlSerialize.Serialize<Person>(p);
            Person p2 = xmlSerialize.DeSerialize<Person>(bytes);
            Assert.IsNotNull(p2, "序列化失败");
        }

        [TestMethod]
        public void JSONSerializtion_Serialize_Test()
        {
            JSONSerialization jsonSerialize = new JSONSerialization();
            byte[] bytes = jsonSerialize.Serialize<Person>(p);
            Assert.IsNotNull(bytes, "序列化失败");
        }

        [TestMethod]
        public void JSONSerializtion_DeSerialize_Test()
        {
            JSONSerialization jsonSerialize = new JSONSerialization();
            byte[] bytes = jsonSerialize.Serialize<Person>(p);
            Person p3 = jsonSerialize.DeSerialize<Person>(bytes);
            Assert.IsNotNull(bytes, "序列化失败");
        }

        [Serializable]
        public class Person
        {
            private int _id;
            private string _name;
            private string _email;

            public int Id
            {
                get
                {
                    return _id;
                }
                set
                {
                    _id = value;
                }
            }

            public string Name
            {
                get
                {
                    return _name;
                }
                set
                {
                    _name = value;
                }
            }

            public string Email
            {
                get { return _email; }
                set { _email = value; }
            }

            public Person() { }

            public Person(int id, string name, string email)
            {
                this._id = id;
                this._name = name;
                this._email = email;
            }
        }
    }
}
