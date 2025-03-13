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
        [Header("Selector")]
        [SerializeField]
        private GameObject selector;
        private Queue<GameObject> selectors = new();
        private int initialCount = 3;
        #endregion

        // 메서드
        public void Open(UI_Root root)
        {
            for (int i = 0; i < root.UI_Parts.Count; i++)
            {
                if (!selectors.TryDequeue(out var result))
                {
                    result = Instantiate(selector, transform);
                }
                result.SetActive(true);
                Selector sel = result.GetComponent<Selector>();
                sel.Set(root.UI_Parts[i].GetComponent<UI_Target>());
            }
        }

        public void Close()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject temp = transform.GetChild(i).gameObject;
                temp.SetActive(false);
                selectors.Enqueue(temp);
            }
        }

        public void ReadyToSelect()
        {
            for (int i = 0; i < initialCount; i++)
            {
                GameObject temp = Instantiate(selector, transform);
                temp.SetActive(false);
                selectors.Enqueue(temp);
            }
            gameObject.SetActive(false);
        }
    }
}