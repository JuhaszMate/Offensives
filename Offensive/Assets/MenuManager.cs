using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject menuCamera;
    [SerializeField] private Vector3 camPos1;
    [SerializeField] private Vector3 camRot1;
    [SerializeField] private Vector3 camPos2;
    [SerializeField] private Vector3 camRot2;
    [SerializeField] private Vector3 camPos3;
    [SerializeField] private Vector3 camRot3;
    [SerializeField] private Vector3 camPos4;
    [SerializeField] private Vector3 camRot4;
    
    
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject KurszkUI;
    [SerializeField] private GameObject BerlinUI;

    private Quaternion camRotation;

    private Vector3 difference;
    private Vector3 finalPos;
    private Quaternion rotDifference;
    private Quaternion finalRot;

    private float lerpEndValue;
    [SerializeField] private float lerpSpeed;

    [SerializeField] private LayerMask raycastLayer;

    [SerializeField] private GameObject tableLight;

    // Start is called before the first frame update
    void Start()
    {
        finalPos = camPos1;
        finalRot.eulerAngles = camRot1;
        KurszkUI.SetActive(false);
        BerlinUI.SetActive(false);
        Background.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        #region lerp

        lerpEndValue = lerpSpeed / 1000f;

        #region position
        menuCamera.transform.position = Vector3.Lerp(menuCamera.transform.position, finalPos, lerpSpeed * Time.deltaTime);

        difference = menuCamera.transform.position - finalPos;

        if (Mathf.Abs(difference.x) < lerpEndValue && Mathf.Abs(difference.y) < lerpEndValue && Mathf.Abs(difference.z) < lerpEndValue)
        {
            menuCamera.transform.position = finalPos;
        }

        #endregion

        #region rotation

        menuCamera.transform.rotation = camRotation;
        camRotation = Quaternion.Lerp(menuCamera.transform.rotation, finalRot, lerpSpeed * Time.deltaTime);

        rotDifference.eulerAngles = menuCamera.transform.rotation.eulerAngles - finalRot.eulerAngles;

        if (Mathf.Abs(rotDifference.eulerAngles.x) < lerpEndValue && Mathf.Abs(rotDifference.eulerAngles.y) < lerpEndValue && Mathf.Abs(rotDifference.eulerAngles.z) < lerpEndValue)
        {
            menuCamera.transform.rotation = finalRot;
        }

        #endregion

        #endregion

        #region menuManager

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(menuCamera.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, raycastLayer))
            {
                if (hit.transform.GetComponent<Interactable>().cameraControll == 2)
                {
                    finalPos = camPos2;
                    finalRot.eulerAngles = camRot2;
                }
                if (hit.transform.GetComponent<Interactable>().cameraControll == 3)
                {
                    finalPos = camPos3;
                    finalRot.eulerAngles = camRot3;
                    KurszkUI.SetActive(true);
                    Background.SetActive(true);
                }
                else
                {
                    KurszkUI.SetActive(false);
                }

                if (hit.transform.GetComponent<Interactable>().cameraControll == 4)
                {
                    finalPos = camPos4;
                    finalRot.eulerAngles = camRot4;
                    BerlinUI.SetActive(true);
                    Background.SetActive(true);
                }
                else
                {
                    BerlinUI.SetActive(false);
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Background.SetActive(false);
            if (finalPos == camPos2)
            {
                finalPos = camPos1;
                finalRot.eulerAngles = camRot1;
            }
            if (finalPos != camPos1 && finalPos != camPos2)
            {
                finalPos = camPos2;
                finalRot.eulerAngles = camRot2;
            }
        }

        if (menuCamera.transform.position == camPos1)
        {
            tableLight.SetActive(true);
        }
        else
        {
            tableLight.SetActive(false);
        }
    }

        #endregion
}
