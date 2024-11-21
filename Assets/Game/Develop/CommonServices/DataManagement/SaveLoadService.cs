namespace Game.Develop.CommonServices.DataManagement
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataRepository _repository;

        public SaveLoadService(IDataSerializer serializer, IDataRepository repository)
        {
            _serializer = serializer;
            _repository = repository;
        }

        public void Save <TData>(TData data) where TData : ISaveData
        {
            string serializeDat = _serializer.Serialize(data);
            _repository.Write(SaveDataKeys.GetKeyFor<TData>(), serializeDat);
        }

        public bool TryLoad <TData>(out TData data) where TData : ISaveData
        {
            string key = SaveDataKeys.GetKeyFor<TData>();

            if (_repository.Exists(key) == false)
            {
                data = default(TData);
                return false;
            }

            string serializedData = _repository.Read(key);
            data = _serializer.Deserialize<TData>(serializedData);
            return true;
        }
    }
}
