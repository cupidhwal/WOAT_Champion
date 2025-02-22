using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Behaviour Tree의 Controller
    /// </summary>
    public class Controller_BT : MonoBehaviour
    {
        private Node root;

        public Transform player;
        public float detectionRange = 5.0f;
        public float moveSpeed = 3.0f;

        void Start()
        {
            // 최상위 Selector 노드
            Node_Selector selector = new();

            // 플레이어 추적 Sequence
            Node_Sequence chaseSequence = new();
            chaseSequence.AddChild(new Condition_IsPlayerInRange(transform, player, detectionRange));
            chaseSequence.AddChild(new Action_MoveToPlayer(transform, player, moveSpeed));

            // Selector에 추가 (우선순위: 추적 > Idle)
            selector.AddChild(chaseSequence);
            selector.AddChild(new Action_Idle());

            root = selector;
        }

        void Update()
        {
            root.Execute();
        }
    }
}