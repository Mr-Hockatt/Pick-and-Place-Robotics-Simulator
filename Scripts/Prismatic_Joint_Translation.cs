using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prismatic_Joint_Translation : MonoBehaviour
{
	Vector3 offset;
	Vector3 axis_vector;
	float axis_sign;
	Transform Articulation;
	public enum axis {None, x, y, z};
	public enum sign {None, positive, negative};
	public Text joint_label;
	public axis translation_axis;
	public sign axis_direction;


	void Start()
	{
		/*Asign numeric translation vector to be able to do calculations
		Y translation = (0, 1, 0) / X translation = (1, 0, 0) / Z translation = (0, 0, 1)*/

		switch(translation_axis)
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

		//axis_vector_opposite = Vector3.Negate(axis_vector);
		//Asign transform component of gameobject in question
		Articulation = gameObject.transform;
		offset = Articulation.localPosition;
	}

	public void set_length_to(float length)
	{
		//Set lenght to the joint and display to its text label
		Articulation.localPosition = length * axis_vector * axis_sign + offset;
		joint_label.text = Mathf.Abs(Vector3.Dot(Articulation.localPosition, axis_vector)).ToString();
	}
}
