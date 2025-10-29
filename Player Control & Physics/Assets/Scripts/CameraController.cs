using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    float Distance = 10f;
    float RotationSpeed = 2;
    float MinVertical = 0;
    float MaxVerticalAngle = 90;

    private Vector2 Rotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Rotation += new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X") * RotationSpeed);
        Rotation.x = Mathf.Clamp(Rotation.x, MinVertical, MaxVerticalAngle);

        var TargetRotation = Quaternion.Euler(Rotation); //Euler confuses me a fair bit, but from what I understand it simplifies a float to three decimal points? ill just have to ask.

        transform.position = Target.position - TargetRotation * new Vector3(0f, 0f, Distance);
        transform.rotation = TargetRotation;
    }
}
