using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Seti
{
    /// <summary>
    /// Detail UI - Parts
    /// </summary>
    public class UI_Detail_Parts : UI_Detail
    {
        // 필드
        #region Variables
        [SerializeField]
        private TextMeshProUGUI generation;
        [SerializeField]
        private TextMeshProUGUI text_default;
        [SerializeField]
        private UI_Dynamic_Layout layout;
        [SerializeField]
        private Button button_Change;

        [Header("Parts")]
        [SerializeField]
        private Parts parts;

        // 임시
        private RidingGear gear;
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void Start()
        {
            layout.OnCheckUnit += ToggleText;
            layout.OnCheckUnit += ToggleButton;
        }

        private void OnDisable()
        {
            layout.OnCheckUnit -= ToggleText;
            layout.OnCheckUnit -= ToggleButton;

            ToggleText(true);
        }
        #endregion

        // 메서드
        // Scroll View에서 모듈 클릭
        public override void SetModule(Parts parts)
        {
            this.parts = parts;

            objectName.text = parts.Name;
            generation.text = parts.GenerationTag;


            // 임시
            gear = Manager_Initialize.Instance.Gear;
            layout.SetUnit(gear, parts);
        }

        // Detail Layout에서 교체 클릭
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

            // Detail Info 갱신
            layout.SetUnit(gear, parts);
        }

        // 토글
        private void ToggleText(bool flag) => text_default.gameObject.SetActive(!flag);
        private void ToggleButton(bool flag) => button_Change.gameObject.SetActive(flag);
    }
}