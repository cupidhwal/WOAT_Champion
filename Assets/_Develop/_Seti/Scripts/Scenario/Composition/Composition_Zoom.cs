using System.Collections;
using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "New Camera Action", menuName = "Scenario/Composition/Camera/Zoom")]
    public class Composition_Zoom : CompositionObject
    {
        // 연출
        [Header("Variables")]
        [SerializeField]
        float durationExcute = 1f;
        [SerializeField]
        float presentZoomEff = 2f;

        [Header("Sensitivity")]
        [SerializeField]
        private float sharpnessExcute = 10f;

        public override void Execute(GameObject obj)
        {
            Manager_Scenario.Instance.CorExcutor(CameraCor(durationExcute, presentZoomEff));
        }

        // 반복기
        #region Coroutines
        // 카메라 연출 : Zoom
        IEnumerator CameraCor(float excuteDuration, float presentZoomEff)
        {
            Manager_Scenario.Instance.IsComposition = true;

            // 타겟 지점으로 카메라 이동
            float elapsed = 0f;
            while (elapsed < excuteDuration)
            {
                elapsed += Time.deltaTime;
                Manager_Scenario.Instance.Cinemachine.Lens.OrthographicSize = Mathf.Lerp(Manager_Scenario.Instance.Cinemachine.Lens.OrthographicSize,
                                                                                     presentZoomEff,
                                                                                     sharpnessExcute * Time.deltaTime);
                yield return null;
            }
            Manager_Scenario.Instance.Cinemachine.Lens.OrthographicSize = presentZoomEff;

            Manager_Scenario.Instance.IsComposition = false;
            yield break;
        }
        #endregion
    }
}