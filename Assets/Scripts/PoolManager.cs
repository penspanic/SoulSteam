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

		return _pool[_pool.Count - 1];
	}

	public void Release(T instance)
	{
		instance.OnRelease();
		_pool.Add(instance);
	}
}
