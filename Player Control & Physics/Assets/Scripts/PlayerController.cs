using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal;
    float vertical;
    Vector3 velocity;
    float rotation_speed = 500f;

    public float movementSpeed = 5.0f;
    public float gravityMultiplier = 2;
    public float JumpForce = 20;
    
    private CharacterController character_controller;
    private float downward_velocity; //stores the velocity of the player going... well down.

    public Transform Respawn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character_controller = GetComponent<CharacterController>();
        Camera.main.GetComponent<CameraController>().Target = transform;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        float move_amount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        velocity = new Vector3(horizontal, 0f, vertical) * movementSpeed;
        velocity = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z)) * velocity;


        if (transform.position.y < -10f)
        {
            character_controller.enabled = false;
            transform.position = Respawn.position;
            downward_velocity = 0f;
            character_controller.enabled = true;
        }
        if (character_controller.isGrounded) //Was the player touching the ground?
        {
            downward_velocity = -2f; //locks the player to the ground, good for slopes.

            if (Input.GetButtonDown("Jump"))
            {
                downward_velocity = JumpForce; //Makes the player go up when spacebar is pressed.
            }
        }
        else
        {
            downward_velocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime; //uses the rigidbody and inbuilt gravity system of unity.
            //this is used so our player actually, comes down, not only that but so they dont feel "light" the gravity multipler is there to bring them back down with some force.
        }

        velocity.y = downward_velocity;
        character_controller.Move(velocity * Time.deltaTime);
        

        if (move_amount > 0)
        {
            var target_rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0f, velocity.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, 500f * Time.deltaTime); //This is just to make sure our character turns with our movement
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {

        }
    }
}
