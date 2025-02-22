using System;
using System.Collections.Generic;

namespace Seti
{
    /// <summary>
    /// Behaviour Interface
    /// </summary>
    public interface IBehaviour
    {
        void Initialize(Actor actor);
        Type GetBehaviourType();
    }
}