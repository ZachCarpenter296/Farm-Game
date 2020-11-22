using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Realtime;
using System.Linq;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    //private GameObject farmerPrefab;
    public List<string> characters;
    public List<string> spawnPoints;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

        //Make a master list of all possible characters
        characters = new List<string>();
        characters.Add("BlackChickenManager");
        characters.Add("BrownChickenManager");
        characters.Add("WhiteChickenManager");
        characters.Add("GooseManager");
        characters.Add("TurkeyManager");

        spawnPoints = new List<string>();

    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1) //We're in the game
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
            }
            else
            {
                int i = Random.Range(0, characters.Count - 1);
                Debug.Log("Random index " + i);
                PhotonNetwork.Instantiate(characters[i], Vector3.zero, Quaternion.identity);
                characters.RemoveAt(i);
            }
        }
    }
}