using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

namespace Seti
{
    /// <summary>
    /// 게임 스토리 총괄 디렉터
    /// </summary>
    public class Manager_Scenario : Singleton<Manager_Scenario>
    {
        // 필드
        #region Variables
        // 시나리오
        [Header("Scenario : Common")]
        [SerializeField]
        private Scenario_Unit_Common designer;
        [SerializeField]
        private Scenario_Unit_Common mechanic;

        // 연출
        [Header("Composition")]
        [SerializeField]
        private Composition currentComposition;
        [SerializeField]
        private CompositionsPerScene[] compositionList;
        #endregion

        // 속성
        #region Properties
        public Scenario_Unit_Common Designer => designer;
        public Scenario_Unit_Common Mechanic => mechanic;




        public CinemachineCamera Cinemachine { get; private set; }
        public Composition CurrentComp => currentComposition;
        public GameObject TempTarget { get; private set; }
        public string CurrentDialogue { get; private set; }
        public bool IsDialogue { get; private set; } = false;
        public bool IsComposition { get; set; } = false;
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void Start()
        {
            Initialize();
        }
        #endregion

        // 이벤트 메서드
        private void OnValidate()
        {
            for (int i = 0; i < compositionList.Length; i++)
            {
                compositionList[i].UpdateIndex(i);
            }
        }

        // 대화
        public void OpenDialogue(ScenarioData data)
        {
            
        }
        public void NextDialogue()
        {
            
        }

        private void Initialize()
        {
            // 참조
            Cinemachine = FindAnyObjectByType<CinemachineCamera>();
        }
        public void CorStopper() => StopAllCoroutines();
        public void CorExcutor(IEnumerator cor) => StartCoroutine(cor);
    }
}