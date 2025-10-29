using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float Distance = 10f;
    public float RotationSpeed = 2;
    public float MinVertical = 0;
    public float MaxVerticalAngle = 85;
    public float ZoomSpeed = 5f;
    public float MinZoom = 2f;
    public float MaxZoom = 15f;



    private Vector2 Rotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
     void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Distance -= scroll * ZoomSpeed;
        Distance = Mathf.Clamp(Distance, MinZoom, MaxZoom); // For zooming in and out the camera

        Rotation += new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X") * RotationSpeed);
        Rotation.x = Mathf.Clamp(Rotation.x, MinVertical, MaxVerticalAngle);

        var TargetRotation = Quaternion.Euler(Rotation); //Euler confuses me a fair bit, but from what I understand it simplifies a float to three decimal points? ill just have to ask.

        transform.position = Target.position - TargetRotation * new Vector3(0f, 0f, Distance);
        transform.rotation = TargetRotation;


    }
}
