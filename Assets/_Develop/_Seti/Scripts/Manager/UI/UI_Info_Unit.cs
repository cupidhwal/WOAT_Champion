using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seti
{
    /// <summary>
    /// Info Unit
    /// </summary>
    public class UI_Info_Unit : MonoBehaviour
    {
        // 필드
        #region Variables
        public TextMeshProUGUI specName;
        public TextMeshProUGUI previousValue;
        public TextMeshProUGUI presentValue;
        public Image changeIndicator;   // 값 증가/감소에 따라 색 변경
        #endregion

        // 메서드
        public void SetData(string specName, float previous, float present)
        {
            this.specName.text = $"{SpecToKorean(specName)}";
            previousValue.text = MathUtility.GetFormat(previous, 1);
            presentValue.text = MathUtility.GetFormat(present, 1);

            // 값이 증가하면 초록색, 감소하면 빨간색, 같으면 기본색
            if (present > previous)
                changeIndicator.color = Color.green;
            else if (present < previous)
                changeIndicator.color = Color.red;
            else
                changeIndicator.color = Color.white;
        }

        private string SpecToKorean(string spec)
        {
            return spec switch
            {
                "maxSpeed" => "최대 속력",
                "turnSpeed" => "턴 속력",
                "tiltSpeed" => "기울기 속력",
                "reverseSpeed" => "역주행 속력",
                "acceleration" => "초기 힘",
                "momentum" => "이동 힘",
                "downForce" => "다운포스",
                "brakeCoefficient" => "브레이크 계수",
                _ => spec
            };
        }
    }
}