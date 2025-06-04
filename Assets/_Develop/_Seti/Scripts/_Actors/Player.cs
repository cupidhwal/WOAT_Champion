using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Player
    /// </summary>
    [RequireComponent(typeof(Condition_Player))]
    public class Player : Actor
    {
        private Player_Look look;
        public Player_Look Player_Look
        {
            get
            {
                if (!look)
                {
                    look = GetComponent<Player_Look>();
                }
                return look;
            }
        }
    }
}