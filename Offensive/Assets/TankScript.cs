using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankScript : MonoBehaviourPunCallbacks
{
    private Rigidbody tankRb;
    private float speed;
    private float maxSpeed;
    private float rotateSpeed;
    private float maxRotateSpeed;
    [SerializeField] private float acceleration = 100f;
    [SerializeField] private float rotateAccenleration = 100f;

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

        #region Movement

        if (Input.GetKey(KeyCode.W))
        {
            maxSpeed = 10;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            maxSpeed = 0;
        }

        if (Input.GetKey(KeyCode.S))
        {
            maxSpeed = -10;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            maxSpeed = 0;
        }

        /////////////////////////////////////////

        speed = maxSpeed * acceleration;
        tankRb.AddForce(Vector3.forward * speed * Time.deltaTime);

        /////////////////////////////////////////

        if (Input.GetKey(KeyCode.D))
        {
            maxRotateSpeed = 10;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            maxRotateSpeed = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            maxRotateSpeed = -10;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            maxRotateSpeed = 0;
        }

        /////////////////////////////////////////

        rotateSpeed = maxRotateSpeed * rotateAccenleration;
        tankRb.AddTorque(0, rotateSpeed * Time.deltaTime, 0);

        /////////////////////////////////////////
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
