using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Develop.CommonUI
{
    public class IconWithText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;
        
        public void SetText(string text) => _text.text = text;
        
        public void SetIcon(Sprite icon) => _icon.sprite = icon;
        
    }
}
