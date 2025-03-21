using System;
using System.Collections;
using UnityEngine;

namespace Seti
{
    // 게임 유틸리티
    public static class GameUtility
    {
        // 일반 타이머
        public static IEnumerator Interpolation(float a, float b, float duration, float reception, Action<float> onUpdate)
        {
            float startTime = Time.time;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed = Time.time - startTime;
                float value = Mathf.Lerp(a, b, reception * (elapsed / duration));
                onUpdate?.Invoke(value); // 여기서 값을 업데이트
                yield return null;
            }

            onUpdate?.Invoke(b); // 마지막 값 보정
        }
        public static IEnumerator Interpolation(Vector3 a, Vector3 b, float duration, float reception, Action<Vector3> onUpdate)
        {
            float startTime = Time.time;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed = Time.time - startTime;
                Vector3 value = Vector3.Lerp(a, b, reception * (elapsed / duration));
                onUpdate?.Invoke(value); // 여기서 값을 업데이트
                yield return null;
            }

            onUpdate?.Invoke(b); // 마지막 값 보정
        }

        // 충돌 타이머
        public static IEnumerator Timer_Collision(Transform transform, LayerMask layer, float t)
        {
            Collider collider = transform.GetComponent<Collider>();
            collider.excludeLayers |= (1 << layer);

            yield return new WaitForSeconds(t);
            collider.excludeLayers &= ~(1 << layer);
        }

        // 중력 유틸리티
        public static void Gravity(Transform target, float gravityStrength = 9.81f)
        {
            Vector3 currentVelocity = Vector3.zero;
            Vector3 gravity = new(0, -gravityStrength, 0);
            currentVelocity += gravity * Time.deltaTime;
            target.position += currentVelocity * Time.deltaTime;
        }

        // 마우스로 클릭한 지점의 위치 정보를 반환하는 유틸리티
        public static Vector3 RayToWorldPosition() => RayToWorldPosition(0);
        public static Vector3 RayToWorldPosition(int ignoreLayerMask)
        {
            Vector3 hitPosition = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~ignoreLayerMask))
                hitPosition = hit.point;

            return hitPosition;
        }
        public static Vector3 RayToWorldPosition(LayerMask ignoreLayer)
        {
            Vector3 hitPosition = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ignoreLayer))
                hitPosition = hit.point;

            return hitPosition;
        }

        // B에 대한 A의 스크린 상 위치를 반환하는 유틸리티
        public static Vector2 ScreenPositionFromA(Camera camera, Transform aTransform, Transform bTransform)
        {
            if (camera == null || aTransform == null || bTransform == null)
            {
                Debug.LogWarning("Camera or Transforms are null.");
                return Vector2.zero;
            }

            // B의 월드 좌표를 스크린 좌표로 변환
            Vector3 screenPosition = camera.WorldToScreenPoint(bTransform.position - aTransform.position);

            // A의 스크린 좌표와 비교 (Z축 제외)
            Vector2 screenPositionRelativeToA = new(
                -screenPosition.x,
                -screenPosition.y
            );
            Debug.Log(screenPositionRelativeToA);
            return screenPositionRelativeToA;
        }
    }
}