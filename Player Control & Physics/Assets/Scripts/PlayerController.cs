using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float movementspeed = 5.0f;
    float rotation_speed = 500f;
    Vector3 velocity;
    private CharacterController character_controller;
    private float downward_velocity; //stores the velocity of the player going... well down.


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        float move_amount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

        velocity = new Vector3(horizontal, 0f, vertical) * movementspeed;

        if (character_controller.isGrounded)
        {
            downward_velocity = -2f; //locks the player to the ground, good for slopes.
        }
        else
        {
            downward_velocity += Physics.gravity.y * Time.deltaTime; //uses the rigidbody and inbuilt gravity system of unity.
        }

        velocity.y = downward_velocity;
        character_controller.Move(velocity * Time.deltaTime);
  

        if (move_amount > 0)
        {
            var target_rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0f, velocity.z)); 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, 500f * Time.deltaTime); //This is just to make sure our character turns with our movement
        }
    }
}
