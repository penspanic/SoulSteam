using System;
using UnityEngine;
using Logic.Entity;
using Utility;


public class ObjectFactory<T>
{
	public static ObjectFactory<T> Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ObjectFactory<T>();
			}

			return _instance;
		}
	}

	private static ObjectFactory<T> _instance;

	public T Create()
	{
		string resourcePath = $"Prefabs/{typeof(T).Name}";
        Vector3 pos = new Vector3(UnityEngine.Random.Range(200f, 1000f), UnityEngine.Random.Range(200f, 1000f));
		return GameObject.Instantiate(Resources.Load<GameObject>(resourcePath), pos, Quaternion.identity).GetComponent<T>();

		throw new NotSupportedException();
	}
}