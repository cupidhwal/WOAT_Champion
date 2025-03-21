using UnityEngine;
using Unity.Cinemachine;

namespace Seti
{
    public class CameraFollow : MonoBehaviour
    {
        // 필드
        #region Variables
        [Header("View : Root")]
        [SerializeField]
        private Transform firstPerson;
        [SerializeField]
        private Transform thirdPerson;

        [Header("View : Variables")]
        [SerializeField]
        private float duration;
        [SerializeField]
        private float reception;

        private Condition_Player condition;
        private CinemachineCamera cinemachineCamera;
        private CinemachineThirdPersonFollow thirdFollow;
        private Vector3 initialDamping;
        private Vector3 initialOffset;
        private float initialArmLength;
        private float initialCameraDis;
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void Start()
        {
            condition = Manager_Initialize.Instance.Player.Condition as Condition_Player;

            cinemachineCamera = GetComponent<CinemachineCamera>();
            thirdFollow = GetComponent<CinemachineThirdPersonFollow>();

            if (!cinemachineCamera.Target.TrackingTarget)
                View_ThirdPerson();

            initialDamping = thirdFollow.Damping;
            initialOffset = thirdFollow.ShoulderOffset;
            initialArmLength = thirdFollow.VerticalArmLength;
            initialCameraDis = thirdFollow.CameraDistance;
        }
        #endregion

        public void View_FirstPerson()
        {
            StartCoroutine(GameUtility.Interpolation(thirdFollow.Damping, Vector3.zero, duration, reception, value => thirdFollow.Damping = value));
            StartCoroutine(GameUtility.Interpolation(thirdFollow.ShoulderOffset, Vector3.zero, duration, reception, value => thirdFollow.ShoulderOffset = value));
            StartCoroutine(GameUtility.Interpolation(thirdFollow.VerticalArmLength, 0f, duration, reception, value => thirdFollow.VerticalArmLength = value));
            StartCoroutine(GameUtility.Interpolation(thirdFollow.CameraDistance, 0f, duration, reception, value => thirdFollow.CameraDistance = value));
            cinemachineCamera.Target.TrackingTarget = firstPerson;

            condition.ViewChange(Type_View.Follow_First);
        }

        public void View_ThirdPerson()
        {
            condition.ViewChange(Type_View.Follow_Third);

            cinemachineCamera.Target.TrackingTarget = thirdPerson;
            StartCoroutine(GameUtility.Interpolation(thirdFollow.Damping, initialDamping, duration, reception, value => thirdFollow.Damping = value));
            StartCoroutine(GameUtility.Interpolation(thirdFollow.ShoulderOffset, initialOffset, duration, reception, value => thirdFollow.ShoulderOffset = value));
            StartCoroutine(GameUtility.Interpolation(thirdFollow.VerticalArmLength, initialArmLength, duration, reception, value => thirdFollow.VerticalArmLength = value));
            StartCoroutine(GameUtility.Interpolation(thirdFollow.CameraDistance, initialCameraDis, duration, reception, value => thirdFollow.CameraDistance = value));
        }
    }
}