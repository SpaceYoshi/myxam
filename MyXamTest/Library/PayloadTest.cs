using MyXamLibrary;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyXamTest.Library;

public class PayloadTest
{
    [Fact]
    public void GetPayloadTypeTest()
    {
        var type = Payload.GetPayloadType(0);
        Assert.Equal(PayloadType.AddAgenda, type);
        type = Payload.GetPayloadType(1);
        Assert.Equal(PayloadType.AddEvent, type);
        type = Payload.GetPayloadType(2);
        Assert.Equal(PayloadType.RequestAvailableAgendas, type);
        type = Payload.GetPayloadType(3);
        Assert.Equal(PayloadType.SubscribeToAgenda, type);
        type = Payload.GetPayloadType(4);
        Assert.Equal(PayloadType.UnsubscribeFromAgenda, type);
        type = Payload.GetPayloadType(5);
        Assert.Equal(PayloadType.AvailableAgendas, type);
        Assert.Throws<ArgumentOutOfRangeException>(() => Payload.GetPayloadType(6));
    }

    [Fact]
    public void GetPayloadTest()
    {
        var payload = Payload.GetPayload(PayloadType.AddAgenda, new byte[] { 1, 2, 3 });
        Assert.Equal(new byte[] { 3, 0, 0, 0, 0, 1, 2, 3 }, payload);
    }

    //[Fact]
    //public void SendJsonTest()
    //{
    //    var stream = new MemoryStream();
    //    JsonSerializerOptions jsonOptions = new()
    //    {
    //        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    //    };
    //    Payload.SendJson(PayloadType.AddAgenda, new { Id = 1, Name = "Test" }, stream, jsonOptions);
    //    var expected = new byte[] { };
    //    Assert.Equal(expected, stream.ToArray());
    //}
    
}