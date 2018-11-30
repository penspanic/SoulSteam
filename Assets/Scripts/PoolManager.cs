using System.Collections.Generic;

public class PoolManager<T> where T : IPoolable
{
	public static PoolManager<T> Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new PoolManager<T>();
			}

			return _instance;
		}
	}

	private static PoolManager<T> _instance;
	private List<T> _pool = new List<T>();

	public T Get()
	{
		if (_pool.Count == 0)
		{
			T newInstance = ObjectFactory<T>.Instance.Create();
			_pool.Add(newInstance);
		}

		T target = _pool[_pool.Count - 1];
		_pool.RemoveAt(_pool.Count - 1);
		target.OnInit();

		return target;
	}

	public void Release(T instance)
	{
		instance.OnRelease();
		_pool.Add(instance);
	}
}

public class SimplePool<T> where T : IPoolable
{
	private List<T> _pool = new List<T>();
	private System.Func<T> _createFunc;

	public SimplePool(System.Func<T> createFunc)
	{
		_createFunc = createFunc;
	}

	public T Get()
	{
		if (_pool.Count == 0)
		{
			T newInstance = _createFunc();
			_pool.Add(newInstance);
		}

		T target = _pool[_pool.Count - 1];
		_pool.RemoveAt(_pool.Count - 1);
		target.OnInit();

		return target;
	}

	public void Release(T instance)
	{
		instance.OnRelease();
		_pool.Add(instance);
	}
}