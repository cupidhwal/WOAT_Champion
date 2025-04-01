using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Seti
{
    /// <summary>
    /// Move Behaviour
    /// </summary>
    [System.Serializable]
    public class Interact : IBehaviour
    {
        // 필드
        #region Variables
        // 전략 관리
        private Actor actor;
        #endregion

        // 인터페이스
        public void Initialize(Actor actor)
        {
            this.actor = actor;
        }
        public Type GetBehaviourType() => typeof(Interact);

        // 이벤트 핸들러
        public void OnInteractStarted(InputAction.CallbackContext context)
        {
            string path = context.control.path;
            switch (path)
            {
                case "/Keyboard/escape":
                    ESC();
                    break;

                case "/Keyboard/g":
                    OnInteraction();
                    break;

                case "/Keyboard/r":
                    OnRide();
                    break;

                case "/Keyboard/4":
                    //Debug.Log("Magic 4");
                    break;
            }
        }

        // 메서드
        void ESC()
        {
            Manager_UI.Instance.Close();
        }

        void OnInteraction()
        {
            //if (StoryManager.Instance.IsDialogue)
            //{
            //    StoryManager.Instance.NextDialogue();
            //    return;
            //}

            //if (player.CurrentTeller != null && player.CurrentTeller.CanDialogue)
            //{
            //    player.CurrentTeller.StoryEnter();
            //    return;
            //}

            // 대화
            if (Manager_UI.Instance.Scenario.UI_Parts[0].activeSelf)
            {
                UI_Dialogue dialogue = Manager_UI.Instance.Scenario.UI_Parts[0].GetComponent<UI_Dialogue>();
                dialogue.DrawNextDialogue();
            }
            else
            {
                if (actor.CurrentNPC != null)
                {
                    actor.CurrentNPC.Interact();
                    return;
                }
            }
        }

        void OnRide()
        {
            if (actor.CurrentGear)
            {
                actor.Condition.StanceChange(Stance.Normal);
                actor.Controller_Animator.ActivateLayer(0);

                actor.CurrentGear.RideOff(actor);
                actor.SetGear(null);
                return;
            }

            if (actor.NearGear)
            {
                actor.SetGear(actor.NearGear);
                actor.NearGear.RideOn(actor);

                actor.Controller_Animator.ActivateLayer(1);
                actor.Condition.StanceChange(Stance.Board);
                return;
            }
        }
    }
}