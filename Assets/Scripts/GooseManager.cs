﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GooseManager : MonoBehaviour
{
    PhotonView PV;

    //get skeleton transform
    Transform skeleton;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        skeleton = gameObject.transform.Find("Goose Skeleton");
    }
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Debug.Log("Instantiated Controller");

        PhotonNetwork.Instantiate("Goose", Vector3.zero, Quaternion.identity);
    }
}
