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
}
