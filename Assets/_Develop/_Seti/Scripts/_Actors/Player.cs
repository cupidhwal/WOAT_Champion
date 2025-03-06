using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Player
    /// </summary>
    public class Player : Actor
    {
        // View type
        public enum ViewType
        {
            Follow_Person,
            QuaterView,
        }

        // 필드
        #region Variables
        [Header("View Type")]
        [SerializeField]
        private ViewType viewType;
        public ViewType View => viewType;
        #endregion

        // 상호작용
        [SerializeField]
        private Storyteller_NPC storyteller;
        public Storyteller_NPC CurrentTeller => storyteller;
        public void SetTeller(Storyteller_NPC teller) => storyteller = teller;

        // 오버라이드
        protected override Condition_Actor CreateState() => gameObject.AddComponent<Condition_Player>();
    }
}