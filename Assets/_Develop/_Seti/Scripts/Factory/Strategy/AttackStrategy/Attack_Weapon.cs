using UnityEngine;

namespace Seti
{
    public class Attack_Weapon : Attack_Base
    {
        // �߻�ȭ
        #region Abstract
        public override void Attack()
        {
            base.Attack();
            Debug.Log("���� ���");
        }
        #endregion
    }
}