using Photon.Pun;
using UnityEngine;

public class BattleManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;  // The prefab for your player character

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)  // This ensures only the master client instantiates the first player object
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate the first player object
        }
        else
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(5, 0, 0), Quaternion.identity);  // Instantiate the second player object
        }
    }
}