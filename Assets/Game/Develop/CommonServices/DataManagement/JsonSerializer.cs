using Newtonsoft.Json;

namespace Game.Develop.CommonServices.DataManagement
{
    public class JsonSerializer : IDataSerializer
    {
        public TDAta Deserialize <TDAta>(string serializedData)
        {
            return JsonConvert.DeserializeObject<TDAta>(serializedData, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
        }

        public string Serialize <TDAta>(TDAta data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
        }
    }
}
