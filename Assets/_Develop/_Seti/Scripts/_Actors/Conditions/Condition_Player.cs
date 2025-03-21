using UnityEngine;
using UnityEngine.Events;

namespace Seti
{
    public class Condition_Player : Condition_Actor
    {
        // 필드
        #region Variables
        [Header("View Type")]
        [SerializeField]
        private Type_View viewType;
        public Type_View View => viewType;

        // 이벤트
        public UnityAction OnViewChange;
        #endregion

        public void ViewChange(Type_View type)
        {
            viewType = type;
            OnViewChange?.Invoke();
        }
    }
}