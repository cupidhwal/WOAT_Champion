using TMPro;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Pair with Target UI
    /// </summary>
    public abstract class UI_Detail : MonoBehaviour
    {
        // 필드
        [Header("Core")]
        [SerializeField]
        protected TextMeshProUGUI objectName;
        [SerializeField]
        protected TextMeshProUGUI description;

        // 추상화
        public abstract void SetModule(Parts parts);
        public abstract void UseModule();
    }
}