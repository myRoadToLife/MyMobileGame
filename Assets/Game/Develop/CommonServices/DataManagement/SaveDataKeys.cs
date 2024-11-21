using System;
using System.Collections.Generic;
using Game.Develop.CommonServices.DataManagement.DataProviders;

namespace Game.Develop.CommonServices.DataManagement
{
    public static class SaveDataKeys
    {
        private static readonly Dictionary<Type, string> Keys = new Dictionary<Type, string>()
        {
            { typeof(PlayerData), "PlayerData" }
        };

        public static string GetKeyFor <TData>() where TData : ISaveData
            => Keys[typeof(TData)];
    }
}
