namespace Fury.ECS
{
    public abstract class ECSSystem
    {
        public virtual void Setup() { }
        public virtual void Cleanup() { }
        public virtual void Update(float dt) { }
        public virtual void FixedUpdate(float dt) { }
    }
}
