using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility_Toggler : MonoBehaviour
{
	public bool visibility_state;

	void Start()
	{
		visibility_state = gameObject.activeSelf;
	}

	public void toggle_visibility()
	{
		visibility_state = !gameObject.activeSelf;
		gameObject.SetActive(visibility_state);
	}
}
