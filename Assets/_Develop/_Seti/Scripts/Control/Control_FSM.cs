using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Control - By Finite State Machine
    /// </summary>
    public class Control_FSM : IControl
    {
        public void OnEnter(Actor actor)
        {
            if (!actor.TryGetComponent<Controller_FSM>(out var controller))
            {
                controller = actor.gameObject.AddComponent<Controller_FSM>();
            }

            // ��������� �ʱ�ȭ ȣ��
            controller.SetBehaviours(actor);
        }

        public void OnExit(Actor actor)
        {
            if (actor.TryGetComponent<Controller_FSM>(out var controller))
                Object.DestroyImmediate(controller);
        }
    }
}