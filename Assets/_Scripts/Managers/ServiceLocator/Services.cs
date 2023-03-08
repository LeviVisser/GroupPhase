using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Object = UnityEngine.Object;

public class Services : Singleton<Services> {
	private const string RootObjectName = "GameManager";

	private Dictionary<Type, object> _serviceMap = new Dictionary<Type, object>();

	private int _serviceCount;

	public static bool Has<T>()
	{
		return Instance.HasInternal<T>();
	}

	private bool HasInternal<T>()
	{
		return _serviceMap.Any(keyValuePair => keyValuePair.Key.IsAssignableFrom(typeof(T)));
	}

	public static T Get<T>()
	{
		return Instance.GetInternal<T>();
	}

	private T GetInternal<T>()
	{
		if (_serviceCount == 0)
		{
			Debug.LogWarning("No services have been registered. A bootstrapper might be missing from the scene");
		}

		foreach (KeyValuePair<Type, object> keyValuePair in _serviceMap.Where(keyValuePair => keyValuePair.Key.IsAssignableFrom(typeof(T))))
		{
			return (T) keyValuePair.Value;
		}

		return default;
	}

	public static List<T> GetAll<T>()
	{
		return Instance.GetAllInternal<T>();
	}

	private List<T> GetAllInternal<T>()
	{
		return (from keyValuePair in _serviceMap where keyValuePair.Value is T select (T) keyValuePair.Value).ToList();
	}


	public void Add<T>(T instance)
	{
		Instance.AddInternal(instance);
	}

	private void AddInternal<T>(T instance)
	{
		if (_serviceMap.ContainsKey(typeof(T)))
		{
			throw new Exception("Service of type " + typeof(T) + " has already been registered");
		}

		_serviceMap.Add(typeof(T), instance);
		_serviceCount = _serviceMap.Count;
	}

	public static void AddPrefab<T>(Behaviour behaviour) where T : Object
	{
		if (behaviour == null)
		{
			Debug.LogError("Services could not instantiate prefab, the given Behaviour is null");
			return;
		}

		if (!behaviour is T)
		{
			Debug.LogError("Services could not instantiate prefab, the given Behaviour is not of type " + typeof(T));
			return;
		}

		AddPrefab<T>(behaviour.gameObject);
	}

	public static void AddPrefab<T>(GameObject prefab) where T : Object
	{
		if (prefab == null)
		{
			Debug.LogError("Services could not instantiate prefab, the given GameObject is null");
			return;
		}

		if (prefab.GetComponent<T>() == null)
		{
			Debug.LogError(
				"Services could not instantiate prefab, the given prefab does not contain a script of type " +
				typeof(T));
			return;
		}

		Instance.AddPrefabInternal<T>(prefab);
	}

	private void AddPrefabInternal<T>(GameObject prefab) where T : Object
	{
		GameObject rootObject = GetOrCreateRootGameObject();
		Component component = prefab.GetComponent(typeof(T));
		Object instance = FindObjectOfType<T>();
		
		if (component == null)
		{
			throw new Exception("Prefab " + prefab.name +
			                    " could not be added to Services, it does not have a component of type " + typeof(T));
		}
		if (_serviceMap.ContainsKey(typeof(T)))
		{
			Debug.LogWarning("Replacing service of type " + typeof(T) + " with prefab " + prefab, prefab);
			_serviceMap.Remove(typeof(T));
		}
		if (instance == null)
		{
			Debug.LogWarning("No instance of type " + typeof(T) + " with prefab " + prefab + " is available to add to the service locator", prefab);
			instance = Instantiate(prefab, rootObject.transform, true);
		}

		instance.name = Regex.Replace(instance.name, "(\\(Clone\\))$", "");

		_serviceMap.Add(typeof(T), component);
		_serviceCount = _serviceMap.Count;
	}

	private static GameObject GetOrCreateRootGameObject()
	{
		GameObject rootObject = GameObject.Find(RootObjectName);
		if (rootObject == null)
		{
			rootObject = new GameObject(RootObjectName);
		}

		return rootObject;
	}

	public void Remove<T>()
	{
		_serviceMap.Remove(typeof(T));
		_serviceCount = _serviceMap.Count;
	}

	public void Clear()
	{
		_serviceMap.Clear();
		_serviceCount = _serviceMap.Count;
	}
}