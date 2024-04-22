using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

	private static Storage _inst = null;

	public List<GameObject> menuSnakePrefabs = new List<GameObject>();

	public static Storage Instance
	{
		get { 
			return _inst;
		}
	}

	void Awake () {
		_inst = this;

	}

	public GameObject getMenuSnakePrefab(int idx)
	{
		return idx >= 0 && idx < menuSnakePrefabs.Count ? menuSnakePrefabs [idx] : menuSnakePrefabs [0];
	}
}
