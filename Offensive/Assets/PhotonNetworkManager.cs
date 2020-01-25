using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{

    public string TeamID;
    [SerializeField] private GameObject[] choosingTeamObj;
    [SerializeField] private GameObject deployCanvas;
    [SerializeField] private GameObject menuCam;

    // Start is called before the first frame update
    void Start()
    {
        deployCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TeamID != "")
        {
            foreach (GameObject g in choosingTeamObj)
            {
                g.SetActive(false);
            }
        }

        if (deployCanvas.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            PhotonNetwork.Instantiate("Tiger", transform.position, transform.rotation, 0);
            deployCanvas.SetActive(false);
            menuCam.SetActive(false);
        }
    }

    public void German()
    {
        TeamID = "german";
        Connect();
        
    }
    public void Soviet()
    {
        TeamID = "soviet";
        Connect();
    }

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to master");
        PhotonNetwork.JoinOrCreateRoom("room", null, null);
    }

    public override void OnCreatedRoom()
    {
        print("Room created");
        deployCanvas.SetActive(true);
    }
}
