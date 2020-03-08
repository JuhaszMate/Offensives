using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks, IPunObservable
{

    public string TeamID;
    [SerializeField] private GameObject[] choosingTeamObj;
    [SerializeField] private GameObject deployCanvas;
    [SerializeField] private GameObject menuCam;
    [SerializeField] private GameObject mainCanvas;
    private bool canSpawn = false;

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
            if (canSpawn && photonView.IsMine)
            {
                deployCanvas.SetActive(true);
            }
        }

        if (deployCanvas.activeSelf && Input.GetKeyDown(KeyCode.Space) && canSpawn)
        {
            PhotonNetwork.Instantiate("T34-85 Variant", transform.position, transform.rotation, 0);
            deployCanvas.SetActive(false);
            menuCam.SetActive(false);
            canSpawn = false;
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
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
        print("Joining...");
    }
    
    public override void OnConnectedToMaster()
    {
        print("Connected to master");
        PhotonNetwork.JoinOrCreateRoom("room", null, null);
    }

    public override void OnJoinedRoom()
    {
        menuCam.SetActive(true);
        print("Joined room");
        deployCanvas.SetActive(true);
        canSpawn = true;
}

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(TeamID);
        }
        else
        {
            TeamID = (string)stream.ReceiveNext();
        }
    }
}
