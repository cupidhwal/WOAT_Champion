using System;
using System.Collections.Generic;

namespace Seti
{
    /// <summary>
    /// Dialogue 데이터 리스트
    /// </summary>
    [Serializable]
    public class Dialogues
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
        public string sentence;
        public NextType nextType;
    }

    public enum NextType
    {
        None = -1,
        Quest,
        Composition,
    }
}
