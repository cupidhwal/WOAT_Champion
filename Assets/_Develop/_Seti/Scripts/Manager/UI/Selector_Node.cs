using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Lower Selector
    /// </summary>
    public class Selector_Node : UI_Node
    {
        [SerializeField]
        private Type_Interaction interactionType;
        [SerializeField]
        private UI_Root root;

        public Type_Interaction Interaction => interactionType;

        public void Open_Root()
        {
            Manager_UI.Instance.SelectorUI.Open_Root(root);
        }

        public void SetNode(Type_Interaction type)
        {
            nameOfUI = NameToKorean(type);
            interactionType = type;
            root = SetRoot(type);
        }

        private UI_Root SetRoot(Type_Interaction type)
        {
            return type switch
            {
                Type_Interaction.Trade => Manager_UI.Instance.MacroMECH,
                Type_Interaction.Modify => Manager_UI.Instance.MacroMECH,
                Type_Interaction.Dialogue => Manager_UI.Instance.MacroMECH,
                _ => Manager_UI.Instance.MacroMECH
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