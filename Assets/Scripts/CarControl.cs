using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{


    private Rigidbody rb;
    public float topSpeed;
    public float speed;
    public float hýzayarý;

    private float horizontalInput;
    private float verticalInput;

    private bool isFren;
    private float currentFrenForce;
    private float currentDonusAcisi;

    public float FrenGucu;
    public bool kontak;
    public float motor;


    [SerializeField] private float maxDonusAcisi;
    [SerializeField] private float motorTorqueForce;
    [SerializeField] private float brakeForce;

    [SerializeField] private WheelCollider onSolTekerlekCollider;
    [SerializeField] private WheelCollider onSagTekerlekCollider;
    [SerializeField] private WheelCollider arkaSolTekerlekCollider;
    [SerializeField] private WheelCollider arkaSagTekerlekCollider;

    [SerializeField] private Transform onSolTekerlekTransform;
    [SerializeField] private Transform onSagTekerlekTransform;
    [SerializeField] private Transform arkaSolTekerlekTransform;
    [SerializeField] private Transform arkaSagTekerlekTransform;

   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        speed = transform.InverseTransformDirection(rb.velocity).z * hýzayarý;
            getUserInput();
            moveTheCar();
            rotateTheCar();
            rotateTheWheels();
            speed = CarSpeed();
            float FrenTorku =FrenGucu* Mathf.Abs(Input.GetAxis("Jump"));
        onSolTekerlekCollider.steerAngle = onSagTekerlekCollider.steerAngle = currentDonusAcisi;
        if (FrenTorku < 0.05)
        {
            arkaSagTekerlekCollider.motorTorque = motor;
            arkaSolTekerlekCollider.motorTorque = motor;
            arkaSagTekerlekCollider.brakeTorque = 0;
            arkaSolTekerlekCollider.brakeTorque = 0;
            onSagTekerlekCollider.brakeTorque = 0;
            onSolTekerlekCollider.brakeTorque = 0;

        }
        else
        {
            arkaSagTekerlekCollider.brakeTorque = FrenTorku;
            arkaSolTekerlekCollider.brakeTorque = FrenTorku;
            onSagTekerlekCollider.brakeTorque = FrenTorku;
            onSolTekerlekCollider.brakeTorque = FrenTorku;

        }
    }
   
   
    private void rotateTheWheels()
    {
        rotateTheWheel(onSolTekerlekCollider, onSolTekerlekTransform);
        rotateTheWheel(onSagTekerlekCollider, onSagTekerlekTransform);
        rotateTheWheel(arkaSolTekerlekCollider, arkaSolTekerlekTransform);
        rotateTheWheel(arkaSagTekerlekCollider, arkaSagTekerlekTransform);


    }
    private void rotateTheWheel(WheelCollider tekerlekCollider, Transform tekerlekTransform)
    {
        Vector3 position;
        Quaternion rotation;
        tekerlekCollider.GetWorldPose(out position, out rotation);
        tekerlekTransform.position = position;
        tekerlekTransform.rotation = rotation;
    }
    private void rotateTheCar()
    {
        currentDonusAcisi = maxDonusAcisi * horizontalInput;
        onSolTekerlekCollider.steerAngle = currentDonusAcisi;
        onSagTekerlekCollider.steerAngle = currentDonusAcisi;
 

    }
    private void moveTheCar()
    {
        onSolTekerlekCollider.motorTorque = verticalInput * motorTorqueForce;
        onSagTekerlekCollider.motorTorque = verticalInput * motorTorqueForce;
        currentFrenForce=isFren ? brakeForce:0f;
        if (isFren)
        {
            onSolTekerlekCollider.brakeTorque = currentFrenForce;
            onSagTekerlekCollider.brakeTorque = currentFrenForce;
            arkaSolTekerlekCollider.brakeTorque = currentFrenForce;
            arkaSagTekerlekCollider.brakeTorque = currentFrenForce;
        }
    }
    private void getUserInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isFren = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetCarRotation();
        }
    }
    private void resetCarRotation()
    {
        Quaternion rotation = transform.rotation;
        rotation.z = 0f;
        rotation.x = 0f;
        transform.rotation = rotation;

    }
    public float CarSpeed()
    {
        float speed = rb.velocity.magnitude;
        speed *= 3.6f;
        if (speed > topSpeed)
            rb.velocity = (topSpeed / 3.6f) * rb.velocity.normalized;
        return speed;
    }

}
