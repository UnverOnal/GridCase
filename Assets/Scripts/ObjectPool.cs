using System.Collections.Generic;

public class ObjectPool<T> where T : new()
{
    private readonly Queue<T> _pool;

    public ObjectPool()
    {
        _pool = new Queue<T>();
    }

    public T GetObject()
    {
        if (_pool.Count > 0)
            return _pool.Dequeue();

        return new T();
    }

    public void ReturnObject(T obj)
    {
        _pool.Enqueue(obj);
    }
}