namespace MyXamServer;

public enum PayloadType
{
    // Client <-> Server
    Agenda,
    Event,
    // Client -> server
    AvailableAgendasRequest,
    AgendaRequest
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

}
