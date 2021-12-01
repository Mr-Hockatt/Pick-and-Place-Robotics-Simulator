using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_Object : MonoBehaviour
{
	Transform FingerA;
	Transform FingerB;
	bool colliding_with_fingerA = false;
	bool colliding_with_fingerB = false;
	float DISTANCE_FROM_GRIPPER_TO_CENTER = 0.2f;

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "FingerA")
		{
			colliding_with_fingerA = true;
			FingerA = other.collider.transform;
		}
		else if(other.collider.tag == "FingerB")
		{
			colliding_with_fingerB = true;
			FingerB = other.collider.transform;
		}
	}

	void OnCollisionStay(Collision other)
	{
		if(colliding_with_fingerA && colliding_with_fingerB)
		{
			this.gameObject.GetComponent<Rigidbody>().useGravity = false;
			this.gameObject.transform.position = FingerA.parent.position - new Vector3(0, DISTANCE_FROM_GRIPPER_TO_CENTER, 0);
			this.gameObject.transform.rotation = FingerA.parent.rotation;
			this.gameObject.transform.parent = FingerA.parent;
			this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
			this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		}
	}

	void OnCollisionExit(Collision other)
	{

		if(other.collider.tag == "FingerA" || other.collider.tag == "FingerB")
		{
			this.gameObject.transform.parent = null;
			this.gameObject.GetComponent<Rigidbody>().useGravity = true;
			this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}
		if(other.collider.tag == "FingerA"){colliding_with_fingerA = false;}
		if(other.collider.tag == "FingerB"){colliding_with_fingerB = false;}
	}
}
