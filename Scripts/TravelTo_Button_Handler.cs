using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TravelTo_Button_Handler : MonoBehaviour
{
	public GameObject Mobile_Robot;
	public InputField target_x_position_input_field;
	public InputField target_z_position_input_field;
	public InputField target_theta_position_input_field;

	public void travel_to()
	{
		Vector3 target_position = new Vector3();
		target_position[0] = float.Parse(target_x_position_input_field.text);
		target_position[1] = float.Parse(target_z_position_input_field.text);
		target_position[2] = float.Parse(target_theta_position_input_field.text);

		//Mobile_Robot.GetComponent<Mobile_Robot_Controller>().travel_to(target_position);
		StartCoroutine(Mobile_Robot.GetComponent<Mobile_Robot_Controller>().travel_to(target_position));
	}
}
