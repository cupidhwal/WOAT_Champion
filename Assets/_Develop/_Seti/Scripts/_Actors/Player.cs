using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Player
    /// </summary>
    [RequireComponent(typeof(Condition_Player))]
    public class Player : Actor
    {
        // 필드
        #region Variables
        [Header("View Type")]
        [SerializeField]
        private ViewType viewType;
        public ViewType View => viewType;
        #endregion

        // 상호작용
        [SerializeField]
        private NPC NPC;
        public NPC CurrentNPC => NPC;
        public void SetNPC(NPC npc) => NPC = npc;
    }
}