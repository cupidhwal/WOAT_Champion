using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Behaviour Tree�� Controller
    /// </summary>
    public class Controller_BT : MonoBehaviour
    {
        private Node root;

        public Transform player;
        public float detectionRange = 5.0f;
        public float moveSpeed = 3.0f;

        void Start()
        {
            // �ֻ��� Selector ���
            Node_Selector selector = new();

            // �÷��̾� ���� Sequence
            Node_Sequence chaseSequence = new();
            chaseSequence.AddChild(new Condition_IsPlayerInRange(transform, player, detectionRange));
            chaseSequence.AddChild(new Action_MoveToPlayer(transform, player, moveSpeed));

            // Selector�� �߰� (�켱����: ���� > Idle)
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