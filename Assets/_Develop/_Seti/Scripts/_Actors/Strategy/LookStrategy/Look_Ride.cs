using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Ride 상태의 시야 제어
    /// </summary>
    public class Look_Ride : Look_Base
    {
        public override void Look(Vector2 readValue = default)
        {
            // body 동기화
            Quaternion targetBodyRotation = actor.CurrentGear.transform.localRotation * Quaternion.Euler(0f, DefineSync(), 0f);
            rb.MoveRotation(targetBodyRotation);

            // head 동기화
            Quaternion targetHeadRotation = Quaternion.Euler(0f, -DefineSync(), 0f);
            headTransform.localRotation = Quaternion.Slerp(headTransform.localRotation, targetHeadRotation, 10f * Time.deltaTime);
        }
    }
}