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
        private GameObject UIObject;

        // 메서드
        public void Set(UI_Target target)
        {
            UIName.text = target.UIName;
            UIObject = target.gameObject;
        }
        public void Open() => Manager_UI.Instance.Open(UIObject);
    }
}