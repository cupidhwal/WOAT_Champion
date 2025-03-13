using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Abstract NPC
    /// </summary>
    public abstract class NPC : MonoBehaviour
    {
        // 필드
        #region Variables
        [Header("AI : Type")]
        [SerializeField]
        protected Type_AI aiType;
        [SerializeField]
        protected Type_Quest questType;
        #endregion

        // 추상화
        public abstract void Interact();
    }
}