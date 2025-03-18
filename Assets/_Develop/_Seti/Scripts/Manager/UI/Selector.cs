using UnityEngine;
using TMPro;

namespace Seti
{
    /// <summary>
    /// UI Selector
    /// </summary>
    public class Selector : MonoBehaviour
    {
        // 필드
        [Header("UI : Target")]
        [SerializeField]
        private TextMeshProUGUI UIName;
        [SerializeField]
        private UI_Node UI;

        // 메서드
        public void Set(Selector_Node node)
        {
            UIName.text = node.UIName;
            UI = node;
        }
        public void Set(UI_Target target)
        {
            UIName.text = target.UIName;
            UI = target;
        }
        public void Open()
        {
            switch (UI)
            {
                case Selector_Node node:
                    node.Open_Root();
                    break;

                case UI_Target:
                    Manager_UI.Instance.Open(UI.gameObject);
                    break;
            }
        }
    }
}