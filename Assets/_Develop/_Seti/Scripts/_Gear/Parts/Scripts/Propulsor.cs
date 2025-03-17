using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 정의 : 구동부
    /// </summary>
    public abstract class Propulsor : Parts
    {
        // 필드
        [Header("Parts : Transducer")]
        [SerializeField]
        protected float performance;

        // 속성
        public float Performance => performance;
    }
}