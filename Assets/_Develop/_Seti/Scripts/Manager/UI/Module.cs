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
        private UI_Target format;

        [Header("Parts : Info")]
        [SerializeField]
        private Parts parts;
        [SerializeField]
        private Image partsIcon;
        [SerializeField]
        private TextMeshProUGUI partsName;
        [SerializeField]
        private TextMeshProUGUI partsGeneration;

        // 메서드
        public void SetModule(Parts parts)
        {
            format = GetComponentInParent<UI_Target>();

            this.parts = parts;
            partsIcon.sprite = parts.Icon;
            partsName.text = parts.Name;
            partsGeneration.text = parts.GenerationTag;
        }

        // Scroll View에서 모듈 클릭
        public void GetModuleInfo() => format.SetTarget(parts);
    }
}