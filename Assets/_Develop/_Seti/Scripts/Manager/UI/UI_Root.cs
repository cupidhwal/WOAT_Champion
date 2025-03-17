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
        protected List<GameObject> ui_Parts = new();
        public List<GameObject> UI_Parts => ui_Parts;
    }
}