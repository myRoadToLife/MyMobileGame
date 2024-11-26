using System;
using System.Collections.Generic;
using Game.Develop.CommonServices.Wallet;

namespace Game.Develop.CommonServices.DataManagement.DataProviders
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
    }
}
