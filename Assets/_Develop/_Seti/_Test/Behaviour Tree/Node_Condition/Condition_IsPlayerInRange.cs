using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Behaviour Tree�� Condition - �÷��̾���� �Ÿ� Ȯ��
    /// </summary>
    public class Condition_IsPlayerInRange : Node
    {
        private Transform enemy;
        private Transform player;
        private float detectionRange;

        public Condition_IsPlayerInRange(Transform enemy, Transform player, float detectionRange)
        {
            this.enemy = enemy;
            this.player = player;
            this.detectionRange = detectionRange;
        }

        public override bool Execute()
        {
            return Vector3.Distance(enemy.position, player.position) < detectionRange;
        }
    }
}