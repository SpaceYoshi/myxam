using System.Text;
using System.Text.Json;

namespace MyXamLibrary;

public enum PayloadType
{
    // Client <-> Server
    AddAgenda,
    AddEvent,
    // Client -> server
    RequestAvailableAgendas,
    SubscribeToAgenda,
    UnsubscribeFromAgenda,
    // Server -> Client
    AvailableAgendas
}

public static class Payload
{
    public static PayloadType GetPayloadType(byte typeByte)
    {
        var type = (PayloadType)typeByte;
        if (!Enum.IsDefined(typeof(PayloadType), typeByte))
            throw new ArgumentOutOfRangeException("Unexpected type value: " + typeByte);
        return type;
    }

    public static byte[] GetPayload(PayloadType type, byte[] payload)
    {
        var length = (uint)(payload.Length + 5);
        var package = new byte[length];

        BitConverter.GetBytes(length).CopyTo(package, 0);
        package[4] = Convert.ToByte(type);
        payload.CopyTo(package, 5);

        return package;
    }

    public static void SendJson(PayloadType type, object serializableObject, Stream stream, JsonSerializerOptions jsonOptions)
    {
        var jsonString = JsonSerializer.Serialize(serializableObject, jsonOptions);
        var jsonBytes = Encoding.UTF8.GetBytes(jsonString);
        var payload = GetPayload(type, jsonBytes);
        stream.Write(payload);
    }
}
