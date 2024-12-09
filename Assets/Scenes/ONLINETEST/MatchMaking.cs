using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Matchmaking : MonoBehaviourPunCallbacks
{
    public Button searchButton; // Reference to the UI Button

    void Start()
    {
        searchButton.onClick.AddListener(OnSearchButtonClick);
    }

    public void OnSearchButtonClick()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Searching for opponent...");
            PhotonNetwork.JoinRandomRoom(); // Try to join a random room
        }
        else if (PhotonNetwork.IsConnected)
        {
            Debug.LogWarning("Still connecting. Please wait...");
        }
        else
        {
            Debug.Log("Not connected to Master Server. Connecting now...");
            PhotonNetwork.ConnectUsingSettings(); // Start the connection process
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server. Joining Lobby...");
        PhotonNetwork.JoinLobby(); // Join the default lobby
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby. Ready for matchmaking.");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully!");
        PhotonNetwork.LoadLevel("BattleScene"); // Load the Battle Scene
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room found, creating a new one...");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions()); // Create a new room
    }
}
