using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 정의 : 집속부
    /// </summary>
    /// 체내에서 발생한 에너지를 라이딩기어의 배터리에 집속시키는 파츠
    public abstract class Receiver : Parts
    {
        // 필드
        [Header("Parts : Receiver")]
        [Range(0, 1)]
        [SerializeField]
        private float efficiency;

        // 속성
        public float Efficiency => efficiency;
    }
}