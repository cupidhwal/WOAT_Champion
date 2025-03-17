using TMPro;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Detail UI - Parts
    /// </summary>
    public class UI_Detail_Parts : UI_Detail
    {
        // 필드
        [SerializeField]
        private TextMeshProUGUI generation;
        
        [Header("Parts")]
        [SerializeField]
        private Parts parts;

        // 메서드
        public override void SetModule(Parts parts)
        {
            this.parts = parts;

            objectName.text = parts.Name;
            generation.text = parts.GenerationTag;
            description.text = parts.Description;
        }

        public override void UseModule()
        {
            //InitializeManager.Instance.Player.CurrentGear.
        }
    }
}