using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject menuCamera;
    [SerializeField] private Vector3 camPos1;
    [SerializeField] private Vector3 camRot1;
    private Quaternion rot1;
    [SerializeField] private Vector3 camPos2;
    [SerializeField] private Vector3 camRot2;
    private Quaternion rot2;

    public AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        rot1.eulerAngles = camRot1;
        rot2.eulerAngles = camRot2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            menuCamera.transform.position = Vector3.Slerp(menuCamera.transform.position, camPos2, curve.Evaluate(Time.deltaTime));
        }
        else
        {
            menuCamera.transform.position = Vector3.Slerp(menuCamera.transform.position, camPos1, curve.Evaluate(Time.deltaTime));
        }
    }
}
