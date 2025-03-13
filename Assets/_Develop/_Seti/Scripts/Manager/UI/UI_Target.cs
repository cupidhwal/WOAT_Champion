using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Abstract Target UI
    /// </summary>
    public abstract class UI_Target : MonoBehaviour
    {
        [Header("Definition")]
        [SerializeField]
        private string nameOfUI;
        public string UIName => nameOfUI;
    }
}