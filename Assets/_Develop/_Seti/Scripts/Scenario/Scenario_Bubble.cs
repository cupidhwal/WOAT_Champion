using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Seti
{
    /// <summary>
    /// 말풍선
    /// </summary>
    public class Scenario_Bubble : MonoBehaviour
    {
        // 필드
        #region Variables
        [Header("Dialogue : Text")]
        [SerializeField]
        private TextMeshProUGUI dialogueText;

        private readonly Queue<Dialogue> dialogues = new();
        #endregion

        // 메서드
        public void Speak(ScenarioData data)
        {
            foreach (var dialogue in data.dialogues)
            {
                dialogues.Enqueue(dialogue);
            }

            Next();
        }

        //다음 대화를 보여준다 - (큐)dialogs에서 하나 꺼내서 보여준다
        private void Next()
        {
            //dialogs 체크
            if (dialogues == null || dialogues.Count == 0)
            {
                End();
                return;
            }

            //dialogs에서 하나 꺼내온다
            Dialogue dialogue = dialogues.Dequeue();

            StopAllCoroutines();
            StartCoroutine(TypingSentence(dialogue.sentence));
        }

        private void End()
        {

        }

        // 텍스트 타이핑 연출
        IEnumerator TypingSentence(string text)
        {
            dialogueText.text = "";

            foreach (char letter in text)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(5f);
            Manager_Scenario.Instance.ExitBubble(gameObject);

            yield break;
        }
    }
}