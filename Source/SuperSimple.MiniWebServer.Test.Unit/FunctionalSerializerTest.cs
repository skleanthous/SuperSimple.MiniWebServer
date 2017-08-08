namespace SuperSimple.MiniWebServer.Test.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class MyRandomType
    {
        public int Property { get; set; }
        public string myField;

        public MyRandomType(int prop, string field)
        {
            Property = prop;
            myField = field;
        }
    }

    public class FunctionalSerializerTest
    {

        [Fact]
        public void StringSerializedFromJsonNet_GetAsType_CorrectlyReturnsObject()
        {
            // Arrange
            var original = new MyRandomType(5, "asdQWE123");
            var serializedString = JsonConvert.SerializeObject(original);
            var request = new Request("POST", "path", "base", "query", serializedString);

            // Act
            var deserialized = request.GetContentAs<MyRandomType>();

            // Asseert
            deserialized.Should().NotBeNull();
            deserialized.Property.Should().Be(original.Property);
            deserialized.myField.Should().Be(original.myField);
        }
    }
}
