using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Lower Selector
    /// </summary>
    public class Selector_Node : UI_Node
    {
        [Header("Info")]
        [SerializeField]
        private UI_Root root;
        [SerializeField]
        private Type_Interaction interactionType;

        public void Open_Root()
        {
            // 대화를 선택하면 셀렉터 닫기
            if (root is UI_Root_Scenario)
            {
                Manager_UI.Instance.Close();
                Manager_Initialize.Instance.Player.Condition.InteractionChange(Interaction.Dialogue);
            }

            Manager_UI.Instance.SelectorUI.Open_Root(root);
        }

        public void SetNode(Type_Interaction type)
        {
            nameOfUI = NameToKorean(type);
            interactionType = type;

            root = type switch
            {
                Type_Interaction.Trade => Manager_UI.Instance.Trade,
                Type_Interaction.Modify => Manager_UI.Instance.MacroMECH,
                Type_Interaction.Dialogue => Manager_UI.Instance.Scenario,
                _ => Manager_UI.Instance.Scenario
            };
        }

        private string NameToKorean(Type_Interaction type)
        {
            return type switch
            {
                Type_Interaction.Trade => "거래",
                Type_Interaction.Modify => "개조",
                Type_Interaction.Dialogue => "대화",
                _ => string.Empty
            };
        }
    }
}