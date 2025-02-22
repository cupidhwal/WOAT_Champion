using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Cinemachine;

namespace Seti
{
    /// <summary>
    /// 게임 스토리 총괄 디렉터
    /// </summary>
    public class StoryManager : Singleton<StoryManager>
    {
        // 필드
        #region Variables
        // 스토리
        [SerializeField]
        private int currentIndex;
        [SerializeField]
        private List<string> dialogueList = new();

        // 참조
        private UIManager uiManager;
        private Condition_Player condition_Player;

        // 연출
        [Header("Composition")]
        [SerializeField]
        private List<CompositionsPerScene> compositionList;
        #endregion

        // 속성
        #region Properties
        public Player Player { get; private set; }
        public CinemachineCamera Cinemachine { get; private set; }
        public string CurrentDialogue { get; private set; }
        public string StageName { get; private set; }
        public bool IsDialogue { get; private set; } = false;
        #endregion

        // 라이프 사이클
        private void Start()
        {
            // 참조
            //StageManager.Instance.stageEndEvent += CheckCurrentStage;
        }

        // 대화
        private void SetDialogue(int index)
        {
            currentIndex = index;
            CurrentDialogue = dialogueList[currentIndex];
            DataManager.Instance.GetDialogData();
        }
        public void OpenDialogue(int index)
        {
            uiManager.OpenDialogueUI(index);
        }
        public void NextDialogue()
        {
            uiManager.NextDialogueUI();
        }

        // 연출
        public void CorStopper() => StopAllCoroutines();
        public void CorExcutor(IEnumerator cor) => StartCoroutine(cor);
        public void SelectComposition(int number, int order)
        {
            string number_order = number.ToString() + "/" + order.ToString();
            var composition = compositionList[currentIndex].compositions.FirstOrDefault(com => com.ID == number_order);

            composition.Action.Execute(composition.Target);
        }

        // 기타 메서드
        #region Methods
        public void LoadDialogue(int index) => currentIndex = index;

        private void CheckCurrentStage()
        {
            /*StageName = StageManager.Instance.CurrentStage.name.Replace("Stage", "").Replace("(Clone)", "").Trim();
            switch (StageName)
            {
                case "_T":
                    SetDialogue(0);
                    OpenDialogue(0);
                    break;

                case "01":
                    SetDialogue(1);
                    OpenDialogue(0);
                    break;
            }*/
        }
        #endregion

        // 필수 요소
        #region Require
        protected override void Awake()
        {
            base.Awake();

            // 초기화
            InitializeOnAwake();
        }

        private void InitializeOnAwake()
        {
            Player = FindAnyObjectByType<Player>();
            if (!Player)
            {
                Debug.LogWarning("No Player, No Game.");
                return;
            }
            condition_Player = Player.Condition as Condition_Player;
            Cinemachine = FindAnyObjectByType<CinemachineCamera>();

            uiManager = FindAnyObjectByType<UIManager>();
            uiManager.dialogueUI.OnDialogueEnter += OnDisablePlayer;
            uiManager.dialogueUI.OnDialogueEnd += OnEnablePlayer;

            CurrentDialogue = dialogueList[currentIndex];
        }

        private void OnEnablePlayer()
        {
            condition_Player.PlayerSetActive(true);
            IsDialogue = false;
        }

        private void OnDisablePlayer()
        {
            condition_Player.PlayerSetActive(false);
            IsDialogue = true;
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