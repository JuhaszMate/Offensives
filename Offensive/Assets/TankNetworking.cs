using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankNetworking : MonoBehaviourPunCallbacks, IPunObservable
{

    [SerializeField] private GameObject[] objToDisable;
    [SerializeField] private Behaviour[] scriptsToDisable;


    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            foreach (GameObject g in objToDisable)
            {
                g.SetActive(false);
            }

            foreach (Behaviour b in scriptsToDisable)
            {
                b.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
