using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 정의 : 집속부
    /// </summary>
    public abstract class Receiver : Parts
    {
        // 필드
        [SerializeField]
        private float efficiency;

        // 속성
        public float Efficiency => efficiency;
    }
}