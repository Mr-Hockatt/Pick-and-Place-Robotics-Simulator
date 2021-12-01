using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Revolute_Joint_Rotation : MonoBehaviour{

	Vector3 axis_vector;
	float axis_sign;
	Transform Articulation;
	public enum axis {None, x, y, z};
	public enum sign {None, positive, negative};
	public Text joint_label;
	public axis rotation_axis;
	public sign axis_direction;


	void Start()
	{
		/*Asign numeric rotation vector to be able to do calculations
		Y rotation = (0, 1, 0) / X rotation = (1, 0, 0) / Z rotation = (0, 0, 1)*/

		switch(rotation_axis)
		{
			case axis.x:
				axis_vector = Vector3.right;
				break;
			case axis.y:
				axis_vector = Vector3.up;
				break;
			case axis.z:
				axis_vector = Vector3.forward;
				break;
		}

		switch(axis_direction)
		{
			case sign.positive:
				axis_sign = 1;
				break;

			case sign.negative:
				axis_sign = -1;
				break;
		}

		//Asign transform component of gameobject in question
		Articulation = gameObject.transform;
	}

	public void set_angle_to(float angle)
	{
		//Set angle to the joint and display to its text label
		Articulation.localEulerAngles = angle * axis_vector * axis_sign;
		//Articulation.eulerAngles = Vector3.Scale(Articulation.eulerAngles, axis_vector_opposite) + (angle * axis_vector);//angle * axis_vector;
		joint_label.text = Vector3.Dot(Articulation.localEulerAngles, axis_vector).ToString();
	}
}
