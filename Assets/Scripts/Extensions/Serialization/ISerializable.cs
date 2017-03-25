public interface ISerializable
{
	void Serialize(ISerializer pSerializer);
	void Deserialize(ISerializer pSerializer);
}