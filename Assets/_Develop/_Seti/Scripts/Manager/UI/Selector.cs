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
        public void Set(UI_Node node)
        {
            UIName.text = node.UIName;
            UI = node;
        }
        public void Open()
        {
            if (UI is Selector_Node node)
            {
                node.Open_Root();
            }

            else
            {
                // Test
                Manager_UI.Instance.Test.Test($"Target: {UI.gameObject.name}");

                Manager_UI.Instance.Open(UI.gameObject);
            }
        }
    }
}