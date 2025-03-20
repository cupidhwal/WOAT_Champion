using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Channel 관리자
    /// </summary>
    public class Manager_Channel : Singleton<Manager_Channel>
    {
        // 필드
        #region Variables
        [Header("Actor")]
        [SerializeField]
        private Actor hostActor = null;
        [SerializeField]
        private List<Actor> waitingActors = new();

        [Header("Channel")]
        [SerializeField]
        private GameObject channel;
        [SerializeField]
        private int initialChannel = 5;

        // Pooling
        private readonly Queue<GameObject> channels = new();
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void Start()
        {
            // 초기 풀링
            for (int i = 0; i < initialChannel; i++)
            {
                GameObject temp = Instantiate(channel, transform);
                channels.Enqueue(temp);
                temp.SetActive(false);
            }
        }
        #endregion

        // Actor 이벤트 구독
        public void Register(Actor actor)
        {
            actor.OnMeetAnother += OnNotify;
        }

        // 이벤트 신호 수신 (호스트 지정)
        public void OnNotify(Actor actor)
        {
            if (!waitingActors.Contains(actor))
                waitingActors.Add(actor);

            if (hostActor == null)
                hostActor = actor;

            // 채널 개설은 호스트가 담당
            if (waitingActors.Count >= 2 && hostActor == actor)
            {
                CreateChannel();
            }
        }

        // 채널 생성/회수
        #region Channel
        private void CreateChannel()
        {
            Debug.Log("두 명의 액터가 만났습니다! 채널 개설!");

            AddChannel(waitingActors[0].transform);
            waitingActors.Clear();
            hostActor = null;
        }
        private GameObject AddChannel(Transform transform)
        {
            if (!channels.TryDequeue(out var result))
            {
                result = Instantiate(channel, this.transform);
            }
            result.transform.position = transform.position;
            result.SetActive(true);
            return result;
        }
        public void DelChannel(GameObject gameObject)
        {
            if (gameObject.GetComponent<InteractionChannel>())
            {
                channels.Enqueue(gameObject);
                gameObject.SetActive(false);
            }
        }
        #endregion
    }
}