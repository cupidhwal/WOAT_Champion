using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 
    /// </summary>
    public class Test_Speed : MonoBehaviour
    {
        Animator animator;
        Vector3 lastPosition;
        Vector3 startPosition;
        float lastTime;
        [SerializeField]
        string motionName;

        void Start()
        {
            animator = GetComponent<Animator>();
            //lastPosition = transform.position;
            startPosition = transform.position;
            lastTime = Time.time;
        }

        void Update()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(motionName))
            {
                float deltaTime = Time.time - lastTime;
                float distanceMoved = Vector3.Distance(transform.position, startPosition);
                float animationSpeed = distanceMoved / deltaTime;

                Debug.Log("실제 애니메이션 속도: " + animationSpeed + " 유닛/초");

                //lastPosition = transform.position;
                //lastTime = Time.time;
            }
        }

    }
}