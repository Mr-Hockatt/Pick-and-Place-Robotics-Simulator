using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gripper_Opening_Handler : MonoBehaviour
{
	float opening_distance;
	public Transform Finger_A;
	public Transform Finger_B;
	public Text opening_percentange_label;
	public float MAX_OPENING_DISTANCE;


	public void set_opening_distance(float percentage)
	{
		opening_distance = MAX_OPENING_DISTANCE * percentage / 100;
		Finger_A.localPosition = new Vector3(-opening_distance, Finger_A.localPosition.y, Finger_A.localPosition.z);
		Finger_B.localPosition = new Vector3(opening_distance, Finger_B.localPosition.y, Finger_B.localPosition.z);
		opening_percentange_label.text = percentage.ToString();
	}
}
