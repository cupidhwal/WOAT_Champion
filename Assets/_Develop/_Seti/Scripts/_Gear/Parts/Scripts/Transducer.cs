using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 정의 : 변환부
    /// </summary>
    public abstract class Transducer : Parts
    {
        // 필드
        [SerializeField]
        private float efficiency;

        // 속성
        public float Efficiency => efficiency;
    }
}