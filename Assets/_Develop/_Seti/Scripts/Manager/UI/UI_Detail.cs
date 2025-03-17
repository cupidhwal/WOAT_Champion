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

        // 추상화
        public abstract void SetModule(Parts parts);    // 모듈 정보 입력
        public abstract void UseModule();               // 버튼에 연결
    }
}