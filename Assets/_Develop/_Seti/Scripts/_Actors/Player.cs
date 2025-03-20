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
    }
}