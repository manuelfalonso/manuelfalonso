using System;
using UnityEngine;

[System.Serializable]
public class AssetFile<T> where T : UnityEngine.Object
{
	[SerializeField]
	private string path;

	Type type = typeof(T);

	T test;

	private T _asset = null;
	public T Asset
	{
		get
		{
			if (_asset != null)
				return _asset;

			_asset = Resources.Load<T>(path);

			if (_asset == null)
			{
				Debug.LogWarning("Not asset found at Resources/" + path);
				_asset = Resources.Load<T>(placeholderAsset);
			}
			return _asset;
		}
	}

	private string placeholderAsset = "Placeholder/placeholder";
}