using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Behaviour Tree�� Action - �⺻ �ൿ
    /// </summary>
    public class Action_Idle : Node
    {
        public override bool Execute()
        {
            Debug.Log("AI: ��� ���� ����...");
            return true;
        }
    }
}