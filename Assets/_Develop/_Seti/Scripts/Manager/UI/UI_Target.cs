using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Abstract Target UI
    /// </summary>
    public abstract class UI_Target : UI_Node
    {
        public abstract void SetTarget(object data);
    }
}