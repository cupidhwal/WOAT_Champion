using UnityEngine;

namespace Seti
{
    /// <summary>
    /// MacroMECH NPC
    /// </summary>
    public class NPC_MacroMECH : NPC
    {
        // 필드
        #region Variables
        [SerializeField]
        protected Player player;
        #endregion

        // 메서드
        public override void Interact()
        {
            Manager_UI.Instance.Selector(aiType);
        }

        // 이벤트 메서드
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                player = other.GetComponent<Player>();
                player.SetNPC(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                player.SetNPC(null);
                player = null;
            }
        }
    }
}