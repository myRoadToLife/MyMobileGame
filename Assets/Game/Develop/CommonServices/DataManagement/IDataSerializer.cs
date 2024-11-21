namespace Game.Develop.CommonServices.DataManagement
{
    public interface IDataSerializer
    {
        string Serialize<TDAta>(TDAta data);
        TDAta Deserialize<TDAta>(string serializedData);
    }
}
