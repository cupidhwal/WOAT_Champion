using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace Seti
{
    /// <summary>
    /// Dynamic Layout Group
    /// </summary>
    public class UI_Dynamic_Layout : MonoBehaviour
    {
        // 필드
        #region Variables
        [Header("Layout : Setting")]
        [SerializeField]
        private GameObject layoutUnit;
        [SerializeField]
        private Transform layoutLeft;
        [SerializeField]
        private Transform layoutRight;
        private readonly GameObject[] layoutUnitArray = new GameObject[8];

        private DataComparison_Board comparison_Board;
        private DataComparison_Boots comparison_Boots;

        // 이벤트
        public UnityAction<bool> OnCheckUnit;

        // 리플렉션
        private readonly string[] targetFields_Board = {
        "maxSpeed", "turnSpeed", "tiltSpeed", "reverseSpeed",
        "acceleration", "momentum", "downForce", "brakeCoefficient"
        };
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void Start()
        {
            // Unit UI Setting
            int half = layoutUnitArray.Length / 2;
            for (int i = 0; i < layoutUnitArray.Length; i++)
            {
                if (i < half)
                    layoutUnitArray.SetValue(layoutLeft.GetChild(i).gameObject, i);
                else
                    layoutUnitArray.SetValue(layoutRight.GetChild(i - half).gameObject, i);
            }
        }

        private void OnDisable()
        {
            AllOff();
        }
        #endregion

        // 메서드
        // Detail Layout에 Unit View
        public void SetUnit(RidingGear gear, Parts parts)
        {
            AllOff();
            switch (gear)
            {
                case RidingGear_Board:
                    Comparison_Board(gear, parts);
                    break;

                case RidingGear_Boots:
                    Comparison_Boots(gear, parts);
                    break;
            }
        }

        private void Comparison_Board(RidingGear gear, Parts parts)
        {
            comparison_Board ??= new();

            for (int i = 0; i < targetFields_Board.Length; i++)
            {
                Type compType = typeof(DataComparison_Board);
                string fieldName = targetFields_Board[i];

                // "old"와 "new" 필드명을 동적으로 생성
                string oldFieldName = "old" + char.ToUpper(fieldName[0]) + fieldName[1..];
                string newFieldName = "new" + char.ToUpper(fieldName[0]) + fieldName[1..];

                // 필드 찾기
                FieldInfo oldField = compType.GetField(oldFieldName, BindingFlags.Public | BindingFlags.Instance);
                FieldInfo newField = compType.GetField(newFieldName, BindingFlags.Public | BindingFlags.Instance);

                if (oldField != null && newField != null)
                {
                    comparison_Board.SetValue(gear, parts);

                    float oldValue = (float)oldField.GetValue(comparison_Board);
                    float newValue = (float)newField.GetValue(comparison_Board);

                    // Unit UI Setting
                    if (oldValue == newValue) continue;

                    UI_Info_Unit unit = StepOn();
                    if (unit != null)
                        unit.SetData(fieldName, oldValue, newValue);
                }
            }

            bool isAnyOn = Array.Exists(layoutUnitArray, element => element.activeSelf);
            OnCheckUnit?.Invoke(isAnyOn);
        }

        private void Comparison_Boots(RidingGear gear, Parts parts)
        {

        }

        // 기타
        private UI_Info_Unit StepOn()
        {
            for (int i = 0; i < layoutUnitArray.Length; i++)
            {
                if (!layoutUnitArray[i].activeSelf)
                {
                    layoutUnitArray[i].SetActive(true);
                    return layoutUnitArray[i].GetComponent<UI_Info_Unit>();
                }
            }
            return null;
        }
        private void AllOff()
        {
            for (int i = 0; i < layoutUnitArray.Length; i++)
            {
                layoutUnitArray[i].SetActive(false);
            }
        }
    }
}