using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Behaviour Tree�� Sequence
    /// </summary>
    public class Node_Sequence : Node
    {
        // �ʵ�
        private List<Node> children = new();

        // �������̵�
        public override bool Execute()
        {
            foreach (var child in children)
            {
                if (!child.Execute()) return false; // �ϳ��� �����ϸ� �ߴ�
            }
            return true; // ��� �ൿ�� �����ϸ� ����
        }

        // �޼���
        public void AddChild(Node node) => children.Add(node);
    }
}