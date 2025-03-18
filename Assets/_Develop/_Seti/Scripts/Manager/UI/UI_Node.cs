using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 선택형 UI의 원점
    /// </summary>
    public abstract class UI_Node : MonoBehaviour
    {
        [Header("Definition")]
        [SerializeField]
        protected string nameOfUI;
        public string UIName => nameOfUI;
    }
}