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

    [Space]
    [SerializeField] private GameObject camTower;
    [SerializeField] private GameObject camGun;
    [SerializeField] private GameObject sight;

    [Space]
    [SerializeField] private GameObject tower;
    [SerializeField] private float towerSpeed;
    [SerializeField] private float _towerSpeed;
    [SerializeField] private float actualTowerSpeed;
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float sensitivity2 = 1f;
    private Quaternion towerRotation;

    [Space]
    [SerializeField] private GameObject gun;
    [SerializeField] private float gunSpeed;
    [SerializeField] private float _gunSpeed;
    [SerializeField] private float actualGunSpeed;
    private Quaternion gunRotation;


    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float division;

    private JointSpring js;
    
    [Space]
    [SerializeField] private float springValue;
    [SerializeField] private float damperValue;
    [SerializeField] private float targetPos;
    [SerializeField] private float massValue;
    [SerializeField] private float distance;
    [SerializeField] private float rate;

    // Start is called before the first frame update
    void Start()
    {
        tankRb = gameObject.GetComponent<Rigidbody>();
        towerRotation.eulerAngles = new Vector3(-90, 0, 0);
        gunRotation.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        #region Input

        if (Input.GetKey(KeyCode.W))
        {
            rightSpeed = speed;
            leftSpeed = speed;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            rightSpeed = 0;
            leftSpeed = 0;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rightSpeed = -speed;
            leftSpeed = -speed;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            rightSpeed = 0;
            leftSpeed = 0;
        }

        /////////////////////////////////////

        if (Input.GetKey(KeyCode.D))
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                leftSpeed = speed / division;
                rightSpeed = -speed / division;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                leftSpeed = speed / division;
                rightSpeed = 0;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                leftSpeed = 0;
                rightSpeed = -speed / division;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rightSpeed = 0;
            leftSpeed = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                leftSpeed = -speed / division;
                rightSpeed = speed / division;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                leftSpeed = 0;
                rightSpeed = speed / division;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                leftSpeed = -speed / division;
                rightSpeed = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rightSpeed = 0;
            leftSpeed = 0;
        }
        #endregion

        #region Movement
        
        js.spring = springValue;
        js.damper = damperValue;
        js.targetPosition = targetPos;

        foreach (WheelCollider wheel in leftWheels)
        {
            wheel.motorTorque = leftForce * Time.deltaTime * leftSpeed;
            wheel.suspensionSpring = js;
            wheel.mass = massValue;
            wheel.wheelDampingRate = rate;
            wheel.suspensionDistance = distance;
        }

        foreach (WheelCollider wheel in rightWheels)
        {
            wheel.motorTorque = rightForce * Time.deltaTime * rightSpeed;
            wheel.suspensionSpring = js;
            wheel.mass = massValue;
            wheel.wheelDampingRate = rate;
            wheel.suspensionDistance = distance;
        }

        #endregion

        #region Tower

        _towerSpeed = Input.GetAxisRaw("Horizontal") * sensitivity * Time.deltaTime;
        towerSpeed += _towerSpeed;
        actualTowerSpeed = Mathf.Lerp(actualTowerSpeed, towerSpeed, sensitivity2 * Time.deltaTime);
        towerRotation.eulerAngles = new Vector3(-90, 0, actualTowerSpeed);
        tower.transform.localRotation = towerRotation;
        Cursor.lockState = CursorLockMode.Locked;

        #endregion

        #region Gun

        if (camGun.activeSelf)
        {
            _gunSpeed = -Input.GetAxisRaw("Vertical") * sensitivity * Time.deltaTime;
            gunSpeed += _gunSpeed;
            actualGunSpeed = Mathf.Lerp(actualGunSpeed, gunSpeed, sensitivity2 * Time.deltaTime);
            gunRotation.eulerAngles = new Vector3(actualGunSpeed / 2, 0, 0);
            gun.transform.localRotation = gunRotation;

            if (gunRotation.eulerAngles.x < -15f && gunRotation.eulerAngles.x > -50)
            {
                actualGunSpeed = -30f;
                gunSpeed = -15f;
                _gunSpeed = 0f;
            }
            if (gunRotation.eulerAngles.x > 15f && gunRotation.eulerAngles.x < 50)
            {
                actualGunSpeed = 30f;
                gunSpeed = 15f;
                _gunSpeed = 0f;
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            sight.SetActive(!camGun.activeSelf);
            camTower.SetActive(!camTower.activeSelf);
            camGun.SetActive(!camGun.activeSelf);
        }


        #endregion
    }
}
