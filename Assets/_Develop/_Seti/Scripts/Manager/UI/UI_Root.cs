using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Abstract UI Root
    /// </summary>
    public abstract class UI_Root : MonoBehaviour
    {
        // Link to Selector
        [Header("UI : Elements")]
        [SerializeField]
        protected List<GameObject> uIParts = new();
        public List<GameObject> UI_Parts => uIParts;
    }
}