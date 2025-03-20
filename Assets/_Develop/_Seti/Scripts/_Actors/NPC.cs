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
        protected Type_AI aiType;
        [SerializeField]
        protected Type_Interaction[] interactionType;
        #endregion

        // 속성
        public Type_AI AiType => aiType;
        public Type_Interaction[] InteractionType => interactionType;

        // 추상화
        public virtual void Interact(Actor actor)
        {
            actor.Condition.InteractionChange(Interaction.Choice);
            Manager_UI.Instance.Selector(interactionType);
        }
    }
}