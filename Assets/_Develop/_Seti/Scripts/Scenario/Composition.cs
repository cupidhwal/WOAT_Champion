using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// �ó����� ���� �������� �� Ŭ����
    /// </summary>
    [Serializable]
    public class Composition
    {
        // �ʵ�
        [SerializeField]
        private string iD;
        [SerializeField]
        private GameObject target;
        [SerializeField]
        private CompositionObject action;

        // �Ӽ�
        public string ID => iD;
        public GameObject Target => target;
        public CompositionObject Action => action;
    }

    /// <summary>
    /// �ó����� ���� ������ �����̳�
    /// </summary>
    [Serializable]
    public class CompositionsPerScene
    {
        // �ʵ�
        public int sceneIndex;
        public List<Composition> compositions;

        // �ν����Ϳ��� ���� ����� �� �ڵ� ����
        public void UpdateIndex(int index)
        {
            sceneIndex = index;
        }
    }
}