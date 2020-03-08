using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControll : MonoBehaviour
{
    private Rigidbody tankRb;
    private float leftSpeed;
    private float rightSpeed;
    [SerializeField] private float leftForce = 20000f;
    [SerializeField] private float rightForce = 20000f;

    [SerializeField] private List<WheelCollider> leftWheels;
    [SerializeField] private List<WheelCollider> rightWheels;

    ///////////////////////////////

    [SerializeField] private GameObject tower;
    [SerializeField] private float towerSpeed;
    [SerializeField] private float _towerSpeed;
    [SerializeField] private float actualTowerSpeed;
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float sensitivity2 = 1f;
    private Quaternion towerRotation;
    private Quaternion _towerRotation;


    // Start is called before the first frame update
    void Start()
    {

        tankRb = gameObject.GetComponent<Rigidbody>();
        _towerRotation.eulerAngles = new Vector3(-90, 0, 0);
        towerRotation.eulerAngles = new Vector3(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        #region Input

        if (Input.GetKey(KeyCode.W))
        {
            rightSpeed = 3;
            leftSpeed = 3;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            rightSpeed = 0;
            leftSpeed = 0;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rightSpeed = -3;
            leftSpeed = -3;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            rightSpeed = 0;
            leftSpeed = 0;
        }

        /////////////////////////////////////

        if (Input.GetKey(KeyCode.D))
        {
            rightSpeed = -3;
            leftSpeed = 3;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rightSpeed = 0;
            leftSpeed = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rightSpeed = 3;
            leftSpeed = -3;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rightSpeed = 0;
            leftSpeed = 0;
        }
        #endregion

        #region Movement

        foreach (WheelCollider wheel in leftWheels)
        {
            wheel.motorTorque = leftForce * Time.deltaTime * leftSpeed;
        }

        foreach (WheelCollider wheel in rightWheels)
        {
            wheel.motorTorque = rightForce * Time.deltaTime * rightSpeed;
        }

        #endregion

        #region Tower

        _towerSpeed = Input.GetAxisRaw("Horizontal") * sensitivity * Time.deltaTime;
        //towerSpeed = Mathf.Lerp(towerSpeed, _towerSpeed, sensitivity * Time.deltaTime);
        towerSpeed += _towerSpeed;
        actualTowerSpeed = Mathf.Lerp(actualTowerSpeed, towerSpeed, sensitivity2 * Time.deltaTime);
        towerRotation.eulerAngles = new Vector3(-90, 0, actualTowerSpeed);
        tower.transform.rotation = towerRotation;
        Cursor.lockState = CursorLockMode.Locked;



        #endregion
    }
}
