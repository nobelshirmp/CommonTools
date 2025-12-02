using Unity.VisualScripting;
using UnityEngine;

namespace CommonTools
{
    public abstract class GameObjectPool<T> : ObjectPool<T>
        where T : Object
    {
        protected GameObjectPool(T prototype) : base(prototype)
        {
        }

        protected override T Create()
        {
            var @object = GameObject.Instantiate(Prototype);
            @object.name = Prototype.name;

            return @object;
        }

        protected override void Destroy(T @object)
        {
            GameObject.Destroy(@object);
        }
    }
}