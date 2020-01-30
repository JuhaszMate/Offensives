using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankScript : MonoBehaviourPunCallbacks, IPunObservable
{

    private Rigidbody tankRb;
    private float speed;
    private float maxSpeed;
    private float rotateSpeed;
    private float maxRotateSpeed;
    private float acceleration;
    private float rotateAccenleration;

    [SerializeField] private GameObject tower;
    [SerializeField] private float towerSpeed;
    [SerializeField] private float _towerSpeed;
    [SerializeField] private float actualTowerSpeed;
    public float sensitivity = 0.1f;
    public float sensitivity2 = 0.1f;
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
        if (!photonView.IsMine)
        {
            return;
        }

        #region Movement

        if (Input.GetKey(KeyCode.W))
        {
            maxSpeed = 10;
            if (maxSpeed > speed && speed < -0.01f)
            {
                acceleration = 1;
            }
            else
            {
                acceleration = 0.1f;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            maxSpeed = 0;
        }

        if (Input.GetKey(KeyCode.S))
        {
            maxSpeed = -10;
            if (maxSpeed < speed && speed > 0.01f)
            {
                acceleration = 1;
            }
            else
            {
                acceleration = 0.1f;
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            maxSpeed = 0;
        }

        /////////////////////////////////////////

        speed = Mathf.Lerp(speed, maxSpeed, acceleration * Time.deltaTime);
        tankRb.MovePosition(tankRb.position + new Vector3(0, 0, speed * Time.deltaTime));

        /////////////////////////////////////////

        if (Input.GetKey(KeyCode.D))
        {
            rotateSpeed = 10;
            if (maxRotateSpeed > rotateSpeed && rotateSpeed < -0.01f)
            {
                rotateAccenleration = 1;
            }
            else
            {
                rotateAccenleration = 0.1f;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            maxRotateSpeed = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotateSpeed = -10;
            if (maxRotateSpeed < rotateSpeed && rotateSpeed > 0.01f)
            {
                rotateAccenleration = 1;
            }
            else
            {
                rotateAccenleration = 0.1f;
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            maxRotateSpeed = 0;
        }

        /////////////////////////////////////////

        rotateSpeed = Mathf.Lerp(rotateSpeed, maxRotateSpeed * 10, rotateAccenleration / 10 * Time.deltaTime);
        tankRb.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
