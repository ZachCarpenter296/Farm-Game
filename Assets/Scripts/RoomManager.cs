using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    GameObject go;
    //private GameObject farmerPrefab;
    [SerializeField]
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
        characters[0] = "PlayerManager";
        characters[1] = "BlackChickenManager";
        characters[2] = "BrownChickenManager";
        characters[3] = "WhiteChickenManager";
        characters[4] = "GooseManager";
        characters[5] = "TurkeyManager";

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
                int i = Random.Range(0, availableCharacters.Count);
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
