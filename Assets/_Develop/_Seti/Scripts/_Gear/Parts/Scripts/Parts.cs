using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 정의 : 부품의 기본
    /// </summary>
    public abstract class Parts : ScriptableObject
    {
        public abstract void Excute();
    }
}