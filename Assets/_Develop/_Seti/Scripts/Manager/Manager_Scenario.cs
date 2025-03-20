using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Cinemachine;
using Noah;

namespace Seti
{
    /// <summary>
    /// 게임 스토리 총괄 디렉터
    /// </summary>
    public class Manager_Scenario : Singleton<Manager_Scenario>
    {
        // 필드
        #region Variables
        // 스토리
        [Header("Dialogue")]
        [SerializeField]
        private int currentIndex;
        [SerializeField]
        private List<string> dialogueList = new();

        // 연출
        [Header("Composition")]
        [SerializeField]
        private Composition currentComposition;
        [SerializeField]
        private List<CompositionsPerScene> compositionList;
        #endregion

        // 속성
        #region Properties
        public CinemachineCamera Cinemachine { get; private set; }
        public Composition CurrentComp => currentComposition;
        public GameObject TempTarget { get; private set; }
        public string CurrentDialogue { get; private set; }
        public bool IsDialogue { get; private set; } = false;
        public bool IsComposition { get; set; } = false;
        #endregion

        // 대화
        private void SetDialogue(int index)
        {
            currentIndex = index;
            CurrentDialogue = dialogueList[currentIndex];
            Manager_Data.Instance.GetDialogData();
        }
        public bool OpenDialogue(int index)
        {
            ScenarioData data = SaveLoadManager.Instance.scenarioSaveData;
            if (data == null) return false;

            //if (Manager_Data.Instance.DialogueData.CheckSeens[index])
            //    return false;

            //uiManager.OpenDialogueUI(index);
            //condition_Player.PlayerSetActive(false);

            return true;
        }
        public void NextDialogue()
        {
            if (IsComposition) return;

            //uiManager.NextDialogueUI();
        }

        // 연출
        public void CorStopper() => StopAllCoroutines();
        public void CorExcutor(IEnumerator cor) => StartCoroutine(cor);
        public void SelectComposition(int number, int order)
        {
            string number_order = number.ToString() + "/" + order.ToString();
            currentComposition = compositionList[currentIndex].compositions.FirstOrDefault(com => com.ID == number_order);

            foreach (var act in currentComposition.Actions)
            {
                act.Execute(currentComposition.Target);
            }
        }
        public void ReadyComposition()
        {
            // 마을 포탈
            //GameObject portals = StageManager.Instance.CurrentStage.transform.GetChild(0).gameObject;
            GameObject portals = gameObject;
            //switch (Manager_Data.Instance.deathCount)
            //{
            //    case 1:
            //        DisableComposition("Stage000", 1, portals);
            //        break;

            //    case 2:
            //        DisableComposition("Stage000", 2, portals);
            //        break;

            //    case 3:
            //        DisableComposition("Stage000", 3, portals);
            //        break;

            //    case 4:
            //        DisableComposition("Stage000", 4, portals);
            //        break;

            //    case 5:
            //        DisableComposition("Stage000", 5, portals);
            //        break;
            //}

            // 미니맵
            GameObject miniMap = FindAnyObjectByType<Mini_Map>().gameObject;
            DisableComposition("Stage001", 0, miniMap);
        }
        private void DisableComposition(string stageName, int dialogueIndex, GameObject target)
        {
            ScenarioData data = SaveLoadManager.Instance.scenarioSaveData;
            if (data == null) return;

            ScenarioProgress progress = data.dialogueDatas.FirstOrDefault(d => d.ScenarioName == stageName);
            if (!data.dialogueDatas.Contains(progress) || !progress.CheckSeens[dialogueIndex])
            {
                target.SetActive(false);
            }
        }

        // 기타 메서드
        #region Methods
        private void SwitchCurrentStage()
        {
            if (IsDialogue) return;

            //StageName = StageManager.Instance.CurrentStage.name.Replace("(Clone)", "").Trim();
            //switch (StageName)
            //{
            //    case "Stage_T":
            //        SetDialogue(0);
            //        OpenDialogue(0);
            //        break;

            //    case "Stage000":
            //        SetDialogue(1);
            //        break;

            //    case "Stage001":
            //        SetDialogue(2);
            //        OpenDialogue(0);
            //        break;

            //    case "Stage003":
            //        SetDialogue(3);
            //        break;

            //    case "Stage004":
            //        SetDialogue(4);
            //        break;
            //}
        }
        #endregion

        // 필수 요소
        #region Require
        protected override void Awake()
        {
            base.Awake();

            // 초기화
            Manager_Initialize.Instance.Set_Second += Initialize;
        }

        private void Initialize()
        {
            // 참조
            Cinemachine = FindAnyObjectByType<CinemachineCamera>();

            CurrentDialogue = dialogueList[currentIndex];
        }

        private void OnValidate()
        {
            for (int i = 0; i < compositionList.Count; i++)
            {
                compositionList[i].UpdateIndex(i);
            }
        }
        #endregion
    }
}