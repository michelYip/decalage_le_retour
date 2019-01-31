using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]   // the script must necessarily have a rigidbody
public class PlayerController : MonoBehaviour
{
    [SerializeField] // allows the next var to be seen in the inspector
    private float speed;
    [SerializeField]
    private float lookSensitivityX = 2f;
    [SerializeField]
    private float lookSensitivityY = 2f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // mouvements personnages
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        motor.Move(_velocity);

        // caméra
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0, _yRot, 0) * lookSensitivityX;
        motor.Rotate(_rotation);
        float _xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_xRot, 0, 0) * lookSensitivityY;
        motor.RotateCamera(_cameraRotation);
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Rigidbody rb = this.transform.GetComponent<Rigidbody>();
            rb.velocity = transform.up * 10;
        }

    }
}