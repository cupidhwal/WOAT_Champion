using UnityEngine;

namespace Seti
{
    /// <summary>
    /// MacroMECH NPC
    /// </summary>
    public class NPC_MacroMECH : NPC
    {
        // 이벤트 메서드
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.TryGetComponent<Player>(out var player))
            {
                player.SetNPC(this);
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);

            if (other.TryGetComponent<Player>(out var player))
            {
                player.SetNPC(null);
            }
        }
    }
}