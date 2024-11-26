namespace Game.Develop.CommonServices.DataManagement
{
    public interface IDataSerializer
    {
        TDAta Deserialize<TDAta>(string serializedData);
        string Serialize<TDAta>(TDAta data);
    }
}
