using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Behaviour Tree�� Selector
    /// </summary>
    public class Node_Selector : Node
    {
        // �ʵ�
        private List<Node> children = new();

        // �������̵�
        public override bool Execute()
        {
            foreach (var child in children)
            {
                if (child.Execute()) return true; // �ϳ��� �����ϸ� ���� ����
            }
            return false; // ��� �ൿ�� �����ϸ� Idle ����
        }

        // �޼���
        public void AddChild(Node node) => children.Add(node);
    }
}