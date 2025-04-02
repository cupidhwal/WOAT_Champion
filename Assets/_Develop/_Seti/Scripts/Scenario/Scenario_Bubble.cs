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
        private TextMeshProUGUI nameText;
        [SerializeField]
        private TextMeshProUGUI dialogueText;

        private readonly Queue<Dialogue> dialogues = new();
        #endregion

        public void Speak(ScenarioData data)
        {
            dialogues.Clear();

            foreach (var dialogue in data.dialogues)
            {
                dialogues.Enqueue(dialogue);
            }

            Next();
        }

        public void Next()
        {
            // dialogues 체크
            if (dialogues == null || dialogues.Count == 0)
            {
                End();
                return;
            }

            // dialogues 큐
            Dialogue dialogue = dialogues.Dequeue();

            // 대사 출력
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(TypingSentence(dialogue));
        }

        private void End()
        {

        }

        // 텍스트 타이핑 연출
        IEnumerator TypingSentence(Dialogue dialogue)
        {
            nameText.text = dialogue.name;
            dialogueText.text = "";

            foreach (char letter in dialogue.sentence)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(5f);
            gameObject.SetActive(false);

            yield break;
        }
    }
}