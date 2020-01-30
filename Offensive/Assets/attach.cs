using UnityEngine;

public class attach : MonoBehaviour {

    [SerializeField] private GameObject otherGO;
    [SerializeField] private Vector3 relPos;

    [SerializeField] private Quaternion gunRot;

	// Use this for initialization
	void Start () {

        gunRot = new Quaternion();

	}
	
	// Update is called once per frame
	void Update () {

        gunRot = gameObject.transform.rotation;

        otherGO.transform.position = transform.TransformPoint(relPos);

        otherGO.transform.rotation = gunRot;
	}
}
