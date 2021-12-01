using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mobile_Robot_Controller : MonoBehaviour
{
	float right_wheel_velocity;
	float left_wheel_velocity;
	Transform Mobile_Robot;
	Vector3 START_POINT;
	Vector3 TARGET_POINT;
	float MAX_ERROR;
	Vector3 distance_to_target;
	Vector3 current_position;
	float current_orientation;
	Vector2 velocities;
	Vector2 displacements;
	float linear_displacement;
	float angular_displacement;
	Vector2 cartesian_displacements;
	Vector3 relative_displacements;
	Vector3 next_position;


	public float get_final_orientation(Vector3 delta_position)
	{
		float delta_x = delta_position[0];
		float delta_y = delta_position[1];
		float delta_theta = delta_position[2];

		float final_orientation = Mathf.Rad2Deg * Mathf.Atan2(delta_y, delta_x);

		return final_orientation;
	}

	public Vector2 get_linear_and_angular_velocity(Vector3 delta_position)
	{
		float delta_x = delta_position[0];
		float delta_y = delta_position[1];
		float delta_theta = delta_position[2];

		float linear_velocity = Mathf.Sqrt(delta_x * delta_x + delta_y * delta_y);
		float angular_velocity = delta_theta;

		return new Vector2(linear_velocity, angular_velocity);
	}

	public Vector2 integrate_velocities(Vector2 velocities)
	{
		return velocities * Time.deltaTime;
	}

	public Vector2 get_cartesian_displacements(float linear_displacement, float orientation)
	{
		float position_x = linear_displacement * Mathf.Cos(Mathf.Deg2Rad * orientation);
		float position_y = linear_displacement * Mathf.Sin(Mathf.Deg2Rad * orientation);

		return new Vector2(position_x, position_y);
	}

	public Vector3 get_next_position(Vector3 current_position, Vector3 relative_position)
	{
		return current_position + relative_position;
	}

	public IEnumerator go_to(Vector3 target_point)
	{
		Vector3 start_point = new Vector3();
		start_point[0] = Mobile_Robot.position[0];//x coordinate
		start_point[1] = Mobile_Robot.position[2];//z coordinate (as the robot moves in the xz plane)
		start_point[2] = 360 - Mobile_Robot.eulerAngles[1];//theta coordinate in y (as the robot rotates in the y axis)

		//Debug.Log("Current Position" + start_point.ToString());

		Vector3 distance_to_target = target_point - start_point;
		Vector3 current_position = start_point;


		while (distance_to_target.magnitude > MAX_ERROR)
		{
			//Debug.Log(Time.time.ToString());
			current_orientation = get_final_orientation(distance_to_target) - distance_to_target[2];
			velocities = get_linear_and_angular_velocity(distance_to_target);
			displacements = integrate_velocities(velocities);
			linear_displacement = displacements[0];
			angular_displacement = displacements[1];
			cartesian_displacements = get_cartesian_displacements(linear_displacement, current_orientation);
			relative_displacements = new Vector3();
			relative_displacements[0] = cartesian_displacements[0];
			relative_displacements[1] = cartesian_displacements[1];
			relative_displacements[2] = angular_displacement;
			next_position = get_next_position(current_position, relative_displacements);
			current_position = next_position;
			distance_to_target = target_point - current_position;

			Mobile_Robot.position = new Vector3(current_position[0], 0, current_position[1]);
			Mobile_Robot.eulerAngles = new Vector3(0, -current_position[2], 0);

			yield return null;
		}
	}

	public IEnumerator travel_to(Vector3 target_point)
	{
		Vector3 target_movement = new Vector3();

		float current_x_position = Mobile_Robot.position[0];
		float current_z_position = Mobile_Robot.position[2];

		float final_x_position = target_point[0];
		float final_z_position = target_point[1];
		float final_orientation = target_point[2];

		float angle_facing_movement_direction = Mathf.Rad2Deg * Mathf.Atan2(final_z_position - current_z_position, final_x_position - current_x_position);

		target_movement = new Vector3(current_x_position, current_z_position, angle_facing_movement_direction);
		Debug.Log("Going to: " + target_movement.ToString());
		yield return StartCoroutine(go_to(target_movement));

		target_movement = new Vector3(final_x_position, final_z_position, 360 - Mobile_Robot.eulerAngles[1]);//aka current orientation
		Debug.Log("Going to: " + target_movement.ToString());
		yield return StartCoroutine(go_to(target_movement));

		target_movement = new Vector3(final_x_position, final_z_position, final_orientation);
		Debug.Log("Going to: " + target_movement.ToString());
		yield return StartCoroutine(go_to(target_movement));

		Debug.Log("Travel Completed Susccessfully");
	}

	public void change_left_wheel_speed(float wheel_speed)
	{
		left_wheel_velocity = wheel_speed;
	}

	public void change_right_wheel_speed(float wheel_speed)
	{
		right_wheel_velocity = wheel_speed;
	}

	public void stop_cart()
	{
		right_wheel_velocity = 0;
		left_wheel_velocity = 0;
	}

	void Start()
	{
		Mobile_Robot = gameObject.transform;
		//START_POINT = new Vector3(0, 0, 0);
		//TARGET_POINT = new Vector3(10, 10, 45);
		MAX_ERROR = 0.001f * Mathf.Sqrt(3);
		//distance_to_target = TARGET_POINT - START_POINT;
		//current_position = START_POINT;

		//Debug.Log(distance_to_target.magnitude > MAX_ERROR);
	}

	void Update()
	{
		//Debug.Log(Mobile_Robot.eulerAngles);

		float current_theta = 360 - Mobile_Robot.eulerAngles[1];
		float dx = (right_wheel_velocity * Mathf.Cos(Mathf.Deg2Rad * current_theta) / 2) + left_wheel_velocity * Mathf.Cos(Mathf.Deg2Rad * current_theta) / 2;
		float dz = (right_wheel_velocity * Mathf.Sin(Mathf.Deg2Rad * current_theta) / 2) + left_wheel_velocity * Mathf.Sin(Mathf.Deg2Rad * current_theta) / 2;
		dx = dx * Time.deltaTime;
		dz = dz * Time.deltaTime;
		Mobile_Robot.transform.Translate(dx, 0, dz);
		float dtheta = (right_wheel_velocity * 1/0.15f) + (left_wheel_velocity * -1/0.15f);
		dtheta = dtheta * Time.deltaTime;
		Mobile_Robot.transform.Rotate(0, dtheta, 0);

		/*
		if (distance_to_target.magnitude > MAX_ERROR)
		{
			Debug.Log(Time.time.ToString());
			current_orientation = get_final_orientation(distance_to_target) - distance_to_target[2];
			velocities = get_linear_and_angular_velocity(distance_to_target);
			displacements = integrate_velocities(velocities);
			linear_displacement = displacements[0];
			angular_displacement = displacements[1];
			cartesian_displacements = get_cartesian_displacements(linear_displacement, current_orientation);
			relative_displacements = new Vector3();
			relative_displacements[0] = cartesian_displacements[0];
			relative_displacements[1] = cartesian_displacements[1];
			relative_displacements[2] = angular_displacement;
			next_position = get_next_position(current_position, relative_displacements);
			current_position = next_position;
			distance_to_target = TARGET_POINT - current_position;

			Mobile_Robot.position = new Vector3(current_position[0], 0, current_position[1]);
			Mobile_Robot.eulerAngles = new Vector3(0, -current_position[2], 0);
		}*/
	}
}
