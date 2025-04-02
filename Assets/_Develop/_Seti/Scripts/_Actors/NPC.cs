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

        // 상호작용
        public void Interact()
        {
            // 대화
            if (FSM_Scenario.Instance.CurrentState is State_Scenario_Dialogue)
            {
                UI_Dialogue dialogue = Manager_UI.Instance.Scenario.UI_Options[0].GetComponent<UI_Dialogue>();
                dialogue.DrawNextDialogue();

                Scenario_Unit_Actor unit = GetComponent<Scenario_Unit_Actor>();
                unit.Next();
            }
            else
            {
                Manager_UI.Instance.Selector(interactionType);
            }
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