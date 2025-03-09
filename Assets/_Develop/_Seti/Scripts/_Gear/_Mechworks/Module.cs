using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seti
{
    /// <summary>
    /// Parts Button의 기본 정보를 표시하는 Module
    /// </summary>
    public class Module : MonoBehaviour
    {
        // 필드
        [SerializeField]
        private Image partsIcon;
        [SerializeField]
        private TextMeshProUGUI partsName;
        [SerializeField]
        private TextMeshProUGUI partsGeneration;

        // 메서드
        public void SetModule(Parts parts)
        {
            partsIcon.sprite = parts.Icon;
            partsName.text = parts.Name;
            partsGeneration.text = parts.Generation;
        }
    }
}