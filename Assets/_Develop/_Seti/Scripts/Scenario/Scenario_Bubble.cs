using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

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

        [Header("Dialogue : Properties")]
        [SerializeField]
        private int lengthLimit = 15;
        #endregion

        // 라이프 사이클
        private void LateUpdate()
        {
            TraceHead();
        }

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

            // 말풍선은 Player 대사를 쓰지 않는다
            if (dialogue.character > 1)
            {
                // 대사 출력
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);

                StopAllCoroutines();
                StartCoroutine(TypingSentence(dialogue));
            }
        }

        private void End()
        {
            if (Manager_UI.Instance.stackCount > 0)
                Manager_UI.Instance.CloseAll();
        }

        // 말풍선 트레이싱
        void TraceHead()
        {
            NPC npc = Manager_Initialize.Instance.Player.CurrentNPC;
            if (!npc) return;

            // NPC 머리 위치
            Vector3 head_NPC = npc.Head.position;
            Vector3 head_Screen = Camera.main.WorldToScreenPoint(head_NPC);

            // Canvas Rect
            RectTransform rect_Canvas = Manager_UI.Instance.transform.GetChild(0).GetComponent<RectTransform>();

            // Bubble Rect
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rect_Canvas,
                head_Screen,
                null,
                out Vector2 uiPosition
            );

            RectTransform rect_Bubble = gameObject.GetComponent<RectTransform>();
            rect_Bubble.anchoredPosition = uiPosition;
        }

        // 텍스트 타이핑 연출
        IEnumerator TypingSentence(Dialogue dialogue)
        {
            int length = 0;

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

            yield return new WaitForSeconds(5f);
            gameObject.SetActive(false);

            yield break;
        }
    }
}