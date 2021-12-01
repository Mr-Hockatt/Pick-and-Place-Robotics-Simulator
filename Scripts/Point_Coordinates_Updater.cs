using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point_Coordinates_Updater : MonoBehaviour
{
	public Text Px_label;
	public Text Py_label;
	public Text Pz_label;
	Transform game_object;

	void Start()
	{
		game_object = gameObject.transform;
	}

	void Update()
	{
		Px_label.text = game_object.position.x.ToString();
		Py_label.text = game_object.position.y.ToString();
		Pz_label.text = game_object.position.z.ToString();
	}
}