using System.Collections;
using System.Collections.Generic;
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
        // 대사
        private Queue<Dialogue> dialogues;

        // UI
        public GameObject dialogueSwitch;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI sentenceText;
        public Button nextButton;

        // 대화 관련
        public State_Scenario_Dialogue dialogueState;
        public UnityAction OnDialogueEnter;
        public UnityAction OnDialogueEnd;
        #endregion

        private void OnEnable()
        {
            dialogues = new Queue<Dialogue>();
            Initialize();

            OnDialogueEnd += Seen;
            OnDialogueEnd += Manager_UI.Instance.CloseAll;

            dialogueState.OnScenarioEvent += StartDialogue;

            Manager_Initialize.Instance.Player.Condition.InteractionChange(Interaction.Dialogue);
        }

        private void OnDisable()
        {
            Initialize();
            dialogues = null;

            OnDialogueEnd -= Seen;

            if (dialogueState)
                dialogueState.OnScenarioEvent -= StartDialogue;
        }

        //초기화
        private void Initialize()
        {
            dialogues.Clear();

            nameText.text = "";
            sentenceText.text = "";
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
            //dialogs 체크
            if (dialogues == null || dialogues.Count == 0)
            {
                EndDialogue();
                return;
            }

            //dialogs에서 하나 꺼내온다
            Dialogue dialogue = dialogues.Dequeue();

            nextButton.gameObject.SetActive(false);

            nameText.text = dialogue.name;

            StopAllCoroutines();
            StartCoroutine(TypingSentence(dialogue.sentence));

            // 대화 도중 연출 처리
            //if (dialogue.nextType == NextType.Composition)
            //    Manager_Scenario.Instance.SelectComposition(dialogue.number, dialogue.order);
        }

        //텍스트 타이핑 연출
        IEnumerator TypingSentence(string typingText)
        {
            sentenceText.text = "";

            foreach (char letter in typingText)
            {
                sentenceText.text += letter;
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

        private void Seen()
        {
            //Manager_Data.Instance.DialogueData.CheckSeens[currentNumber] = true;

            //if (Manager_Data.Instance.DialogueData.CheckSeens[^1])
            //    Manager_Data.Instance.DialogueData.SeenCompleted = true;

            //SaveLoadManager.Instance.SaveScenario(Manager_Data.Instance.DialogueData);
        }
    }
}