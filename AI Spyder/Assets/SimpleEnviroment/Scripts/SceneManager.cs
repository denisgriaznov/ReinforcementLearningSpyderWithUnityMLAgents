using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
	public GameObject sceneWorld;
	private int actualMenuSnake = 0;
	public bool GUI_ON = true;

	// Use this for initialization
	void Start () {
		createMenuSnake (actualMenuSnake);
	}

	void OnGUI()
	{
		if (!GUI_ON)
			return;
		
		if (GUI.Button(new Rect(10, 175, 150, 30), "PREV") && !isChange)
		{
			actualMenuSnake--;
			if (actualMenuSnake < 0)
				actualMenuSnake = Storage.Instance.menuSnakePrefabs.Count - 1;

			createMenuSnake (actualMenuSnake);
		}

		if (GUI.Button(new Rect(Screen.width-160, 175, 150, 30), "NEXT")&& !isChange)
		{
			actualMenuSnake++;
			if (actualMenuSnake >= Storage.Instance.menuSnakePrefabs.Count)
				actualMenuSnake = 0;
			createMenuSnake (actualMenuSnake);
		}

	}

	private GameObject actualSnake = null;
	private bool isChange = false;
	public void createMenuSnake(int idx)
	{
		isChange = true;
		StartCoroutine (_createMenuSnake ());

	}

	IEnumerator _createMenuSnake()
	{
		if (actualSnake != null) 
		{
			GameObject.Destroy (actualSnake);
		}
		yield return null;

		actualSnake = GameObject.Instantiate(Storage.Instance.getMenuSnakePrefab(actualMenuSnake));
		actualSnake.transform.parent = sceneWorld.transform;
		actualSnake.transform.localPosition = new Vector3(0,0,0);

		yield return null;
		isChange = false;
	}

}
