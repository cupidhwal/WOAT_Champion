using TMPro;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Detail UI - Parts
    /// </summary>
    public class UI_Detail_Parts : UI_Detail
    {
        // 필드
        [SerializeField]
        private TextMeshProUGUI generation;
        [SerializeField]
        private UI_Dynamic_Layout layout;

        [Header("Parts")]
        [SerializeField]
        private Parts parts;

        // 임시
        private RidingGear gear;

        // 메서드
        public override void SetModule(Parts parts)
        {
            this.parts = parts;

            objectName.text = parts.Name;
            generation.text = parts.GenerationTag;



            gear = Manager_UI.Instance.MacroMECH.Gear;
            layout.GetUnit(gear, parts);
        }

        // 버튼에 연결
        public override void UseModule()
        {
            if (parts is Receiver)
            {
                gear.Parts_Change_Receiver(parts as Receiver);
            }
            else if (parts is Transducer)
            {
                gear.Parts_Change_Transducer(parts as Transducer);
            }
            else
            {
                if (gear is RidingGear_Board board)
                {
                    board.Parts_Change_Propulsor(parts as Propulsor_Kinetic);
                }
                else if (gear is RidingGear_Boots boots)
                {
                    boots.Parts_Change_Propulsor(parts as Propulsor_Electronic);
                }
            }
        }
    }
}