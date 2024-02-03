using System.Collections;
using System.Collections.Generic;

namespace Fury.ECS
{
    public sealed partial class Entities<T>
        where T : struct
    {
        public Enumerator GetEnumerator() => new Enumerator(this);
    }
}
