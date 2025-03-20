using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Scenario 데이터 리스트
    /// </summary>
    [Serializable]
    public class Scenarios
    {
        public List<Scenario> Scenario;
    }

    /// <summary>
    /// Dialogue 데이터 리스트
    /// </summary>
    [Serializable]
    public class Scenario
    {
        public int id;
        public string title;
        public List<Dialogue> dialogues;
    }

    /// <summary>
    /// Dialogue 데이터 모델 클래스
    /// </summary>
    [Serializable]
    public class Dialogue
    {
        public int character;
        public string name;
        [TextArea(5, 30)]
        public string sentence;
        public NextType nextType;
    }

    public enum NextType
    {
        None,
        Quest,
        Composition,
    }
}
