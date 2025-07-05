using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// UI 선택자
    /// </summary>
    public class UI_Selector : MonoBehaviour
    {
        // 필드
        #region Variables
        [Header("Unit : Selector")]
        [SerializeField]
        private GameObject selector;
        [SerializeField]
        private GameObject selectorNode;

        private readonly Queue<GameObject> choices = new();
        private readonly Stack<GameObject> selectors = new();
        private int popCount = 3;
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void OnEnable()
        {
            Manager_Initialize.Instance.Player.Condition.InteractionChange(Interaction.Choice);
        }
        #endregion

        // 메서드
        public void Open_Node(Type_Interaction[] Interactions)
        {
            // Test
            //Manager_UI.Instance.Test.Test($"{Interactions.Length}개의 Node");

            if (Interactions.Length > 1)
                Manager_UI.Instance.Open(gameObject);

            if (popCount != 0)
                CloseStack();

            for (int i = 0; i < Interactions.Length; i++)
            {
                // Selector 생성
                if (!selectors.TryPop(out var result))
                {
                    result = Instantiate(selector, transform.GetChild(0));
                }
                else popCount++;

                // Node 생성
                if (!choices.TryDequeue(out var choice))
                {
                    choice = Instantiate(selectorNode, transform.GetChild(1));
                }
                Selector_Node node = choice.GetComponent<Selector_Node>();
                node.SetNode(Interactions[i]);

                // Selector 세팅
                Selector sel = result.GetComponent<Selector>();
                sel.Set(node);

                if (Interactions.Length == 1)
                    sel.Open();
                else result.SetActive(true);
            }
        }

        public void Open_Root(UI_Root root)
        {
            // Test
            //Manager_UI.Instance.Test.Test($"Root: {root.name}");

            if (root.UI_Options.Count > 1 && !gameObject.activeSelf)
                Manager_UI.Instance.Open(gameObject);

            Close();

            for (int i = 0; i < root.UI_Options.Count; i++)
            {
                // Selector 생성
                if (!selectors.TryPop(out var result))
                {
                    result = Instantiate(selector, transform.GetChild(0));
                }
                else popCount++;

                // Selector 세팅
                Selector sel = result.GetComponent<Selector>();
                sel.Set(root.UI_Options[i].GetComponent<UI_Target>());

                if (root.UI_Options.Count == 1)
                    sel.Open();
                else result.SetActive(true);
            }
        }

        public void Close()
        {
            CloseStack();
            CloseQueue();
        }

        private void CloseStack()
        {
            Transform stack = transform.GetChild(0);
            for (int i = popCount - 1; i >= 0; i--)
            {
                GameObject temp = stack.GetChild(i).gameObject;
                temp.SetActive(false);
                selectors.Push(temp);
                popCount--;
            }
        }
        private void CloseQueue()
        {
            Transform queue = transform.GetChild(1);
            for (int i = 0; i < queue.childCount; i++)
            {
                GameObject temp = queue.GetChild(i).gameObject;
                choices.Enqueue(temp);
            }
        }
    }
}