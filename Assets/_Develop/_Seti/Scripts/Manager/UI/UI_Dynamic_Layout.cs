using System;
using System.Reflection;
using UnityEngine;

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
        [SerializeField]
        private int columnLimit;

        private int columnCount_Left;
        private int columnCount_Right;

        private string[] targetFields_Board = {
        "maxSpeed", "turnSpeed", "tiltSpeed", "reverseSpeed",
        "acceleration", "momentum", "downForce", "brakeCoefficient"
        };
        #endregion

        // 메서드
        public void GetUnit(RidingGear gear, Parts parts)
        {
            Type gearType;
            if (gear is RidingGear_Board)
            {
                gearType = typeof(RidingGear_Board);
            }
            else
            {
                gearType = typeof(RidingGear_Boots);
            }

            foreach (string fieldName in targetFields_Board)
            {
                FieldInfo field = gearType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

                if (field != null)
                {
                    // 현재 장착된 값과 새로운 값 가져오기
                    int rand = UnityEngine.Random.Range(-1, 2);
                    float oldValue = (float)field.GetValue(gear);
                    float newValue = (float)field.GetValue(gear) + rand;

                    //// UI 오브젝트 생성
                    //GameObject infoUnit = Instantiate(infoUnitPrefab, detailViewParent);
                    //InfoUnitUI unitUI = infoUnit.GetComponent<InfoUnitUI>();
                    //unitUI.SetSpecData(fieldName, oldValue, newValue);

                    AddUnit(fieldName, oldValue, newValue);
                }
            }
        }

        // 임시 매개변수
        public void AddUnit(string specName, float previous, float present)
        {
            if (columnCount_Right == columnLimit) return;

            GameObject newItem = Instantiate(layoutUnit);

            // 왼쪽 컬럼이 가득 차지 않았다면 왼쪽에 추가
            if (columnCount_Left < columnLimit)
            {
                newItem.transform.SetParent(layoutLeft, false);
                columnCount_Left++;
            }
            else if (columnCount_Right < columnLimit)
            {
                newItem.transform.SetParent(layoutRight, false);
                columnCount_Right++;
            }

            UI_Info_Unit unit = newItem.GetComponent<UI_Info_Unit>();
            unit.SetData(specName, previous, present);
        }
    }
}