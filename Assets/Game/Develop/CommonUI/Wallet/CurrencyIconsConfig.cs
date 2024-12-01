using System;
using System.Collections.Generic;
using System.Linq;
using Game.Develop.CommonServices.Wallet;
using UnityEngine;

namespace Game.Develop.CommonUI.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Common/Wallet/NewCurrencyIconsConfig", fileName = "CurrencyIconsConfig")]
    public class CurrencyIconsConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyIconConfig> _configs;
        
        public Sprite GetSpriteFor(CurrencyTypes type) => _configs.First(config => config.CurrencyType == type).Sprite;
        
        [Serializable]
        private class CurrencyIconConfig
        {
            [field: SerializeField] public CurrencyTypes CurrencyType { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
            
        }
    }
}
