using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Seti
{
    /// <summary>
    /// 대화창 구현 클래스
    /// 대화 데이터 파일 읽기
    /// 대화 데이터 UI 적용
    /// </summary>
    public class UI_Dialogue : UI_Target
    {
        #region Variables
        // UI
        private UI_Root_Scenario scenario;
        public GameObject dialogueSwitch;
        public TextMeshProUGUI dialogueText;
        public TextMeshProUGUI nameText;
        public Button nextButton;

        // 대사
        private Queue<Dialogue> dialogues;

        // 대화 관련
        public UnityAction OnDialogueEnter;
        public UnityAction OnDialogueEnd;

        [Header("Dialogue : Properties")]
        [SerializeField]
        private int lengthLimit = 30;
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void Awake()
        {
            // 참조
            if (!scenario)
                scenario = GetComponentInParent<UI_Root_Scenario>();

            dialogues = new Queue<Dialogue>();
        }

        private void OnEnable()
        {
            Initialize();

            OnDialogueEnd += Manager_UI.Instance.CloseAll;
            scenario.Dialogue.OnScenarioEvent += StartDialogue;
        }

        private void OnDisable()
        {
            Initialize();

            OnDialogueEnd -= Manager_UI.Instance.CloseAll;
            scenario.Dialogue.OnScenarioEvent -= StartDialogue;
        }
        #endregion

        //초기화
        private void Initialize()
        {
            dialogues.Clear();

            nameText.text = "";
            dialogueText.text = "";
            nextButton.gameObject.SetActive(false);
        }

        public override void SetTarget(object data)
        {
            throw new System.NotImplementedException();
        }

        //대화 시작하기
        private void StartDialogue(ScenarioData data)
        {
            foreach (var dialogue in data.dialogues)
            {
                dialogues.Enqueue(dialogue);
            }

            // 첫번째 대화를 보여준다
            DrawNextDialogue();
            OnDialogueEnter?.Invoke();
        }

        //다음 대화를 보여준다 - (큐)dialogs에서 하나 꺼내서 보여준다
        public void DrawNextDialogue()
        {
            // dialogues 체크
            if (dialogues == null || dialogues.Count == 0)
            {
                EndDialogue();
                return;
            }

            // dialogues 큐
            Dialogue dialogue = dialogues.Dequeue();

            // 대화창은 Player 전용
            if (dialogue.character > 1) return;

            // 대사 출력
            StopAllCoroutines();
            StartCoroutine(TypingSentence(dialogue));

            // 대화 도중 연출 처리
            //if (dialogue.nextType == NextType.Composition)
            //    Manager_Scenario.Instance.SelectComposition(dialogue.number, dialogue.order);
        }

        //텍스트 타이핑 연출
        IEnumerator TypingSentence(Dialogue dialogue)
        {
            int length = 0;

            nextButton.gameObject.SetActive(false);
            nameText.text = dialogue.name;
            StringBuilder sb = new();

            foreach (char letter in dialogue.sentence)
            {
                sb.Append(letter);
                dialogueText.text = sb.ToString();

                // 줄바꿈 계산을 위한 시각적 길이 카운트
                if (!",.!?".Contains(letter))
                    length++;

                // 공백에서만 줄바꿈 허용
                if (length > lengthLimit && letter == ' ')
                {
                    sb.Append('\n');
                    length = 0;
                }

                yield return new WaitForSeconds(0.01f);
            }

            nextButton.gameObject.SetActive(true);

            yield break;
        }

        //대화 종료
        private void EndDialogue()
        {
            //대화 종료시 이벤트 처리
            OnDialogueEnd?.Invoke();
        }
    }
}