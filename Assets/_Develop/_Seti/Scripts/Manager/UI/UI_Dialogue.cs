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
        public UnityAction OnDialogueEnter;
        public UnityAction OnDialogueEnd;
        #endregion

        private void OnEnable()
        {
            dialogues = new Queue<Dialogue>();
            Initialize();

            OnDialogueEnd += Seen;

            Manager_Initialize.Instance.Player.Condition.InteractionChange(Interaction.Dialogue);
        }

        private void OnDisable()
        {
            Initialize();
            dialogues = null;

            OnDialogueEnd -= Seen;
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
        public void StartDialogue(int dialogIndex)
        {
            //현재 대화씬(dialogIndex) 내용을 큐에 입력
            //foreach (var dialogue in Manager_Data.Instance.GetDialogData().Dialogues.dialogues)
            //{
            //    if (dialogue.number == dialogIndex)
            //    {
            //        dialogues.Enqueue(dialogue);
            //    }
            //}

            //첫번째 대화를 보여준다
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
            //Dialogue dialogue = dialogues.Dequeue();
            //currentNumber = dialogue.number;

            //if (dialogue.character == 1)
            //{
            //    npcImage.SetActive(true);
            //    npcImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/MainCharacter_Stand");
            //}
            //else //dialog.character <= 0
            //{
            //    npcImage.SetActive(false);
            //}

            //nextButton.gameObject.SetActive(false);

            //nameText.text = dialogue.name;

            //StopAllCoroutines();
            //StartCoroutine(TypingSentence(dialogue.sentence));

            //// 대화 도중 연출 처리
            //if (dialogue.nextType == NextType.Composition)
            //    Manager_Scenario.Instance.SelectComposition(dialogue.number, dialogue.order);
        }

        //텍스트 타이핑 연출
        IEnumerator TypingSentence(string typingText)
        {
            sentenceText.text = "";

            foreach (char latter in typingText)
            {
                sentenceText.text += latter;
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

            if (ComponentUtility.TryGetComponentInParent<UIManager>(transform, out var uiManager))
                uiManager.CloseDialogueUI();
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