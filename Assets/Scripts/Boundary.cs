using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

enum Axis { x, y, z };

public class Boundary : MonoBehaviour
{

  private GameObject[] players;
  [SerializeField]
  private float distance = 0f;
  [SerializeField]
  private Axis axis = Axis.x;
  [SerializeField]
  private float axisOrigin = 0f;

  void Start()
  {
    
  }

  // Update is called once per frame
  void Update()
  {
    // Get all players
    players = GameObject.FindGameObjectsWithTag("Player");
    Debug.Log("Players length: " + players.Length);

    foreach (GameObject player in players)
    {
      // Get the right player position axis based on setting
      float playerPosition = 0;
      switch (axis)
      {
        case Axis.x:
          playerPosition = player.transform.position.x;
          break;
        case Axis.y:
          playerPosition = player.transform.position.y;
          break;
        case Axis.z:
          playerPosition = player.transform.position.z;
          break;
        default:
          break;
      }

      Debug.Log("playerPosition: " + playerPosition);

      // See if player is in range based on set distance
      if (playerPosition > axisOrigin - distance && playerPosition < axisOrigin + distance)
      {
        // Set material opacity to partially transparent if within distance
        setOpacity(50f);
        Debug.Log("Set to partially transparent at " + axisOrigin + axis);
        Debug.Log(playerPosition + " vs. >" + (axisOrigin - distance) + " or " + playerPosition + " vs. <" + (axisOrigin + distance));
      }
      else
      {
        // Set material opacity to completely transparent if outside distance
        setOpacity(0f);
        Debug.Log("Set to completely transparent at " + axisOrigin + axis);
      }
    }
  }

  private void setOpacity(float opacity)
  {
    // Set material opacity by getting color, changing it, and then setting it back
    MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
    Color newColor = mr.material.color;
    newColor.a = opacity;
    mr.material.SetColor("_Color", newColor);
  }
}
