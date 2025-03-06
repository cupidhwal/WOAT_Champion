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
                case "/Keyboard/r":
                    OnRide();
                    break;

                case "/Keyboard/g":
                    OnInteraction();
                    break;

                case "/Keyboard/3":
                    //Debug.Log("Magic 3");
                    break;

                case "/Keyboard/4":
                    //Debug.Log("Magic 4");
                    break;
            }
        }

        // 메서드
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

            //if (player.CurrentNPC != null)
            //{
            //    player.CurrentNPC.Switch_TradeUI();
            //    return;
            //}
        }

        void OnRide()
        {
            if (actor.CurrentGear)
            {
                actor.CurrentGear.RideOff();
                actor.SetGear(null);
                return;
            }

            if (actor.NearGear)
            {
                actor.SetGear(actor.NearGear);
                actor.NearGear.RideOn(actor);
                return;
            }
        }
    }
}