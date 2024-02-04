using System;

namespace Fury.ECS
{
    public abstract class EcsSystem
    {
        public interface On<T>
        {
            void Run<T>();
        }

        public interface On<T, A>
        {
            void Run<T>(A a);
        }

        public interface OnSetup : On<OnSetup> { }
        public interface OnCleanup : On<OnCleanup> { }
        public interface OnUpdate: On<OnUpdate, float> { }
        public interface OnFixedUpdate : On<OnFixedUpdate, float> { }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple =true)]
        internal protected sealed class WithFilterAttribute : Attribute
        {
            public WithFilterAttribute(string Name, params Type[] components)
            {

            }
        }
    }
}
