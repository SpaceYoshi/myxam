namespace MyXamServer;

public enum PayloadType
{
    Agenda,
    Event
}

public class PayloadUtility
{
    public static PayloadType GetPayloadType(byte typeByte)
    {
        var type = typeByte switch
        {
            0 => PayloadType.Agenda,
            1 => PayloadType.Event,
            _ => throw new ArgumentOutOfRangeException("Unexpected type value: " + typeByte)
        };
        return type;
    }
}
