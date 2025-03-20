using UnityEngine;

namespace Seti
{
    /// <summary>
    /// MacroMECH NPC
    /// </summary>
    public class NPC_MacroMECH : NPC
    {
        // 이벤트 메서드
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.SetNPC(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.SetNPC(null);
            }
        }
    }
}