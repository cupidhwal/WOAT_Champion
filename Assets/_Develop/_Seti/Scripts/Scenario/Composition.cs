using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 시나리오 연출 데이터의 모델 클래스
    /// </summary>
    [Serializable]
    public class Composition
    {
        // 필드
        [SerializeField]
        private string iD;
        [SerializeField]
        private GameObject target;
        [SerializeField]
        private CompositionObject action;

        // 속성
        public string ID => iD;
        public GameObject Target => target;
        public CompositionObject Action => action;
    }

    /// <summary>
    /// 시나리오 연출 데이터 컨테이너
    /// </summary>
    [Serializable]
    public class CompositionsPerScene
    {
        // 필드
        public int sceneIndex;
        public List<Composition> compositions;

        // 인스펙터에서 값이 변경될 때 자동 실행
        public void UpdateIndex(int index)
        {
            sceneIndex = index;
        }
    }
}