using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "To Do Object", menuName = "To Do/Object")]
    public class TodoObject : ScriptableObject
    {
        [Header("해야 할 일")]
        [TextArea(5, 100)]
        public string toDo;
    }
}