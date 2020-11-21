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
    public List<string> availableCharacters;
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
        //characters.Add("PlayerManager");
        characters.Add("BlackChickenManager");
        characters.Add("BrownChickenManager");
        characters.Add("WhiteChickenManager");
        characters.Add("GooseManager");
        characters.Add("TurkeyManager");

        //Make a list of available characters based on the amount of people playing, but making sure there is always a farmer
        availableCharacters = new List<string>();
        availableCharacters.Add("PlayerManager");
        for (int i = PhotonNetwork.PlayerList.Length; i > 1; i--)
        {
            int j = Random.Range(0, characters.Count - 1);
            availableCharacters.Add(characters[j]);
        }

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
            for (int j = PhotonNetwork.PlayerList.Length; j > 0; j--)
            {
                Debug.Log("Loop! " + j);
                int i = Random.Range(0, availableCharacters.Count - 1);
                Debug.Log("Random index " + i);
                PhotonNetwork.Instantiate(availableCharacters[i], Vector3.zero, Quaternion.identity);
                availableCharacters[i].Remove(i);
            }

            //int k = Random.Range(0, availableCharacters.Count - 1);
            //Debug.Log("Random index " + k);
            //PhotonNetwork.Instantiate(availableCharacters[k], Vector3.zero, Quaternion.identity);
            //availableCharacters[k].Remove(k);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
