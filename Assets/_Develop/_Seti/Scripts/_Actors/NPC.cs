using UnityEditor;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Abstract NPC
    /// </summary>
    public abstract class NPC : Actor
    {
        // 필드
        #region Variables
        [Header("AI : Type")]
        [SerializeField]
        protected Type_AI aIType;
        [SerializeField]
        protected Type_Interaction[] interactionType;
        #endregion

        // 속성
        public Type_AI AIType => aIType;
        public Type_Interaction[] InteractionType => interactionType;

        // 추상화
        public virtual void Interact(Actor actor)
        {
            //switch (aIType)
            //{
            //    case Type_AI.Storyteller:
            //        break;

            //    case Type_AI.Mechanic:
            //        break;

            //    case Type_AI.Designer:
            //        break;

            //    case Type_AI.Rider:
            //        break;
            //}

            Manager_UI.Instance.Selector(interactionType);
        }

        // 이벤트 메서드
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.TryGetComponent<Player>(out var player))
            {
                player.SetNPC(this);
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);

            if (other.TryGetComponent<Player>(out var player))
            {
                player.SetNPC(null);
            }
        }
    }
}