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

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

        characters = new List<string>();
        characters.Add("PlayerManager");
        characters.Add("BlackChickenManager");
        characters.Add("BrownChickenManager");
        characters.Add("WhiteChickenManager");
        characters.Add("GooseManager");
        characters.Add("TurkeyManager");

        availableCharacters = new List<string>();
        availableCharacters = characters;
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
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                int i = Random.Range(0, availableCharacters.Count - 1);
                Debug.Log("Random index" + i);
                PhotonNetwork.Instantiate(availableCharacters[i], Vector3.zero, Quaternion.identity);
                availableCharacters[i].Remove(i);
            }
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
