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

	public T Create(string id)
	{
		string resourcePath = $"Prefabs/{id}";
		return GameObject.Instantiate(Resources.Load<GameObject>(resourcePath)).GetComponent<T>();

		throw new NotSupportedException();
	}
}