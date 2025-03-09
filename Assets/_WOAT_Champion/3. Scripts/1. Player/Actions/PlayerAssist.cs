using System.Collections;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Test
    /// </summary>
    public class PlayerAssist : MonoBehaviour
    {
        private float climbOffset = 0.5f;
        private float climbDuration = 0.1f;

        void JumpToLedge(Vector3 ledgePoint)
        {
            Vector3 targetPosition = new Vector3(ledgePoint.x, ledgePoint.y + climbOffset, ledgePoint.z);
            StartCoroutine(SmoothClimb(targetPosition));
        }

        IEnumerator SmoothClimb(Vector3 targetPosition)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = transform.position;

            while (elapsedTime < climbDuration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / climbDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                Debug.Log("Ledge detected via trigger!");
                JumpToLedge(other.transform.position);
            }
        }

    }
}