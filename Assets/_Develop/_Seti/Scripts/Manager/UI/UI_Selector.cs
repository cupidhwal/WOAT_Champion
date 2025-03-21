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
        #endregion

        // 메서드
        public void Open_Node(Type_Interaction[] Interactions)
        {
            for (int i = 0; i < Interactions.Length; i++)
            {
                // Selector 생성
                if (!selectors.TryPop(out var result))
                {
                    result = Instantiate(selector, transform.GetChild(0));
                }
                result.SetActive(true);

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
            }
        }

        public void Open_Root(UI_Root root)
        {
            Close();

            for (int i = 0; i < root.UI_Parts.Count; i++)
            {
                if (!selectors.TryPop(out var result))
                {
                    result = Instantiate(selector, transform.GetChild(0));
                }
                result.SetActive(true);
                Selector sel = result.GetComponent<Selector>();
                sel.Set(root.UI_Parts[i].GetComponent<UI_Target>());

                if (root.UI_Parts.Count == 1)
                {
                    Manager_UI.Instance.Close();
                    sel.Open();
                }
            }
        }

        public void Close()
        {
            CloseStack();
            CloseQueue();
        }

        public void ReadyToSelect()
        {
            CloseStack();
            gameObject.SetActive(false);
        }

        private void CloseStack()
        {
            Transform stack = transform.GetChild(0);
            for (int i = stack.childCount - 1; i >= 0; i--)
            {
                GameObject temp = stack.GetChild(i).gameObject;
                temp.SetActive(false);
                selectors.Push(temp);
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