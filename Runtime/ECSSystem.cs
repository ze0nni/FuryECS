using System;

namespace Fury.ECS
{
    public abstract class ECSSystem
    {
        public virtual void Setup() { }
        public virtual void Cleanup() { }
        public virtual void Update(float dt) { }
        public virtual void FixedUpdate(float dt) { }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple =true)]
        internal protected sealed class WithFilterAttribute : Attribute
        {
            public WithFilterAttribute(string Name, params Type[] components)
            {

            }
        }
    }
}
