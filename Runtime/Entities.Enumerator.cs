using System.Collections;
using System.Collections.Generic;

namespace Fury.ECS
{
    public partial class Entities<T>
    {
        public struct Enumerator : IEnumerator<Entity<T>>
        {
            readonly Entities<T> _entities;

            public Enumerator(Entities<T> entities)
            {
                _entities = entities;
            }

            public Entity<T> Current => throw new System.NotImplementedException();

            object IEnumerator.Current => throw new System.NotImplementedException();

            public void Dispose()
            {
                throw new System.NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new System.NotImplementedException();
            }

            public void Reset()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
