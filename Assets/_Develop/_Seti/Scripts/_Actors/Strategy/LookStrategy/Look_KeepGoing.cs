using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Keep Going 기능
    /// </summary>
    public class Look_KeepGoing : Look_Base
    {
        public override void Look(Vector2 readValue = default)
        {
            headXRotation -= readValue.y * mouseSensitivity;
            headYRotation += readValue.x * mouseSensitivity;
            headXRotation = Mathf.Clamp(headXRotation, -50f, 50f);

            headTransform.localRotation = Quaternion.Euler(headXRotation, headYRotation, 0f);

            // body 동기화
            if (actor.Condition.CurrentStance == Stance.Board)
            {
                Quaternion targetBodyRotation = actor.CurrentGear.transform.localRotation * Quaternion.Euler(0f, DefineSync(), 0f);
                rb.MoveRotation(targetBodyRotation);
            }
        }
    }
}