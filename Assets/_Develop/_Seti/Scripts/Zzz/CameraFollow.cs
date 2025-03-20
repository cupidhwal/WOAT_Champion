using UnityEngine;
using Unity.Cinemachine;

namespace Seti
{
    public class CameraFollow : MonoBehaviour
    {
        // 필드
        #region Variables
        [Header("Root : View")]
        [SerializeField]
        private Transform firstPerson;
        [SerializeField]
        private Transform thirdPerson;

        private CinemachineCamera cinemachineCamera;
        //[SerializeField]
        //private float nearClipPlane = 0.3f;
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void Start()
        {
            cinemachineCamera = GetComponent<CinemachineCamera>();
        }
        #endregion
    }
}