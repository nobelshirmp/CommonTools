using System;
using System.Collections.Generic;

namespace CommonTools
{
    public abstract class ObjectPool<T> : IDisposable
        where T : class
    {
        private readonly Queue<T> RelesedObjects = new();
        private readonly LinkedList<T> BindedObjects = new();

        public ObjectPool(T prototype)
        {
            Prototype = prototype;

            if (Prototype == default)
                throw new System.NullReferenceException($"[Object Pool] *{this.GetType().Name}* Prototype is NULL!");
        }

        public T Prototype { get; }

        protected abstract T Create();
        protected abstract void Destroy(T @object);
        protected abstract void OnSpawn(T @object);
        protected abstract void OnDespawn(T @object);

        public virtual T Spawn()
        {
            if(RelesedObjects.Count == 0)
            {
                BindedObjects.AddLast(Create());
                return BindedObjects.Last.Value;
            }
            else
            {
                OnSpawn(RelesedObjects.Peek());
                return RelesedObjects.Dequeue();
            }
        }

        public virtual void Despawm(T @object)
        {
            OnDespawn(@object);
            RelesedObjects.Enqueue(@object);
        }

        public void Dispose()
        {
            RelesedObjects.Clear();

            foreach (var @object in BindedObjects)
            {
                Destroy(@object);
            }

            GC.SuppressFinalize(this);
        }
    }
}