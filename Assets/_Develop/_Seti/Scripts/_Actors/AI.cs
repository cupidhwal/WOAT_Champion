using UnityEngine;

namespace Seti
{
    /// <summary>
    /// AI Actor
    /// </summary>
    public class AI : Actor
    {
        protected override Condition_Actor CreateState() => gameObject.AddComponent<Condition_AI>();
    }
}