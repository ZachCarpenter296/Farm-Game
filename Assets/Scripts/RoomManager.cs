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
    if (PhotonNetwork.IsMasterClient)
    {
      Debug.Log("Is master client");
    }
    if (scene.buildIndex == 1 && PhotonNetwork.IsMasterClient) //We're in the game
    {
      Player[] currentPlayerList = PhotonNetwork.PlayerList;
      for (int j = currentPlayerList.Length - 1; j >= 0; j--)
      {
        // Create a new player from the list
        Debug.Log("Loop! " + j);
        int i = Random.Range(0, availableCharacters.Count - 1);
        Debug.Log("Random index " + i);
        GameObject newPlayer = PhotonNetwork.Instantiate(availableCharacters[i], Vector3.zero, Quaternion.identity);
        availableCharacters[i].Remove(i);

        // Give players 2+ away to other clients
        if (j != 0)
        {
          this.photonView.RPC("acquireController", RpcTarget.Others, j, newPlayer);
          // newPlayer.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.PlayerList[j]);
        }
      }

      //int k = Random.Range(0, availableCharacters.Count - 1);
      //Debug.Log("Random index " + k);
      //PhotonNetwork.Instantiate(availableCharacters[k], Vector3.zero, Quaternion.identity);
      //availableCharacters[k].Remove(k);
    }
  }

  [PunRPC]
  public void acquireController(int controllerIndex, GameObject newPlayer)
  {
    // If we're the player in question
    if (PhotonNetwork.PlayerList[controllerIndex].UserId == PhotonNetwork.LocalPlayer.UserId)
    {
      newPlayer.GetComponent<PhotonView>().RequestOwnership();
    }
  }

  void Start()
  {

  }

  void Update()
  {

  }
}
