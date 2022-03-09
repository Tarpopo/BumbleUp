using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
public class ManagerPool : Singleton<ManagerPool>
{
    private readonly Dictionary<int,Pool>_pools=new Dictionary<int, Pool>();
    
    public Pool AddPool(PoolType id,bool reparent=true)
    {
        Pool pool;
        if (_pools.TryGetValue((int) id, out pool)==false)
        {
            pool=new Pool();
            _pools.Add((int)id,pool);
            if (reparent)
            {
                var poolsGo = GameObject.Find("[POOLS]") ?? new GameObject("[POOLS]");
                var poolGo = new GameObject("pool:" + id);
                poolGo.transform.SetParent(poolsGo.transform);
                pool.SetParent(poolGo.transform);
            }
        }

        return pool;  
    }

    public GameObject Spawn(PoolType id, GameObject prefab, Vector3 position = default(Vector3),
        Quaternion rotation = default(Quaternion), Transform parent = null,bool setActive=true)
    {
        return _pools[(int) id].Spawn(prefab, position, rotation, parent,setActive);
    }

    public T Spawn<T>(PoolType id, GameObject prefab, Vector3 position = default(Vector3),
        Quaternion rotation = default(Quaternion), Transform parent = null)where T:class
    {
        var val = _pools[(int) id].Spawn(prefab, position, rotation, parent);
        return val.GetComponent<T>();
    }

    public void Despawn(PoolType id,GameObject obj)
    {
        _pools[(int)id].Despawn(obj);
    }

    public void Dispose()
    {
        foreach (var poolsValue in _pools.Values) poolsValue.Dispose();
        _pools.Clear();
    }
}
