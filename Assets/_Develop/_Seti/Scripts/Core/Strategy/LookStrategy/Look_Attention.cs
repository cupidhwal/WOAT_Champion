using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 주목 기능
    /// </summary>
    public class Look_Attention : Look_Base
    {
        public override void Look(Vector2 readValue = default)
        {
            
        }

        // 주목 기능
        //private void OnAttentionEnter(Vector3 gearDir, Quaternion gearRot)
        //{
        //    if (Vector3.Angle(headTransform.forward, gearDir) > 10)
        //        headTransform.rotation = Quaternion.Slerp(headTransform.rotation, gearRot, 0.1f);
        //    else
        //        isAttention = true;
        //}

        //private void OnAttentionStay(Quaternion gear)
        //{
        //    headTransform.rotation = gear;
        //}

        //private void OnAttentionExit(Quaternion head, Quaternion body)
        //{
        //    // 주목 기능이 해제될 경우 각 축의 회전값 초기화
        //    InitializeRotation(0f, 0f, 0f);

        //    if (Vector3.Angle(headTransform.forward, rb.transform.forward) > 2.5)
        //        headTransform.rotation = Quaternion.Slerp(head, body, 0.1f);
        //    else
        //        isAttention = false;
        //}
    }
}