using System.Text.Json.Serialization;

public class JsonUtility
{
    JsonConverter<ItemData> itemDataConverter = new JsonConverter<ItemData>();
}

[JsonSerializable(typeof(ItemData))]
public class ItemData
{
    int code;
    string name;
    int damage;
}