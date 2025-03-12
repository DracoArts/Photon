using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("UI Panels")]
    public GameObject connectingPanel;
    public GameObject namePanel;
    public GameObject createRoomPanel;
    public GameObject startGamePanel;
    public GameObject joinedRoomPanel;
    public GameObject roomListContent;
    
    [Header("UI Elements")]
    public InputField nameInputField;
    public InputField roomInputField;
    public Button startGameButton;
    public Text connectedPlayersText;
    public Text joinedRoomText;
    public GameObject roomListItemPrefab;
    private bool isGameLoaded;
     private bool isBack;
    private void Start()
    {
     PhotonNetwork.ConnectUsingSettings();
        connectingPanel.SetActive(true);
        namePanel.SetActive(false);
        createRoomPanel.SetActive(false);
        startGamePanel.SetActive(false);
        joinedRoomPanel.SetActive(false);
     
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon server.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby.");
        if(!isBack){
         connectingPanel.SetActive(false);
        namePanel.SetActive(true);
        }
        
    }

    public void SaveName()
    {
        string playerName = nameInputField.text.Trim();
        if (string.IsNullOrEmpty(playerName)) return;

        PlayerPrefs.SetString("PlayerName", playerName);
        PhotonNetwork.NickName = playerName;
        namePanel.SetActive(false);
        createRoomPanel.SetActive(true);
    }

    public void CreateRoom()
    {
        startGameButton.interactable = false;
        string roomName = PlayerPrefs.GetString("PlayerName", "defaultRoom");
        if (!int.TryParse(roomInputField.text, out int maxPlayers) || maxPlayers <= 0)
        {
            Debug.LogError("Invalid max players value");
            return;
        }
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = (byte)maxPlayers };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created: " + PhotonNetwork.CurrentRoom.Name);
        createRoomPanel.SetActive(false);
        startGamePanel.SetActive(true);
        SetConnectedPlayersText(1, PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    private void SetConnectedPlayersText(int playersCount, int maxPlayers)
    {
        connectedPlayersText.text = $"Connected players: {playersCount} / {maxPlayers}";
    }

    public override void OnJoinedRoom()
    {    if(!PhotonNetwork.LocalPlayer.IsMasterClient){
                Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
                joinedRoomPanel.SetActive(true);
                createRoomPanel.SetActive(false);
                joinedRoomText.text = "Joined Room: " + PhotonNetwork.CurrentRoom.Name;
          }
      
    }

      public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform child in roomListContent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (RoomInfo room in roomList)
        {
            if (!room.RemovedFromList)
            {
                GameObject roomListItem = Instantiate(roomListItemPrefab, roomListContent.transform);
                roomListItem.transform.GetChild(0).GetComponent<Text>().text = room.Name + ": " + room.MaxPlayers;
                roomListItem.gameObject.GetComponent<Button>().onClick.AddListener(() => {
                    
                    JoinRoom(room.Name);
                    
            });
            }
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
     public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom " + newPlayer.NickName);
          int playerCount=  PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
                 
            PhotonNetwork.CurrentRoom.IsOpen = false;
            ActivateStartGameBtn();
        }
        SetConnectedPlayersText(playerCount,PhotonNetwork.CurrentRoom.MaxPlayers);
      
    }
     public void ActivateStartGameBtn()
    {
         startGameButton.interactable = true;
         startGameButton.onClick.AddListener(()=>{
         isGameLoaded=true;
               if(PhotonNetwork.IsMasterClient){
                photonView.RPC(nameof(StartGamePlay),RpcTarget.AllBuffered);
         }
   
    
         });
    }
        public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom " + otherPlayer.NickName);
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount < PhotonNetwork.CurrentRoom.MaxPlayers && !isGameLoaded)
        {
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
        
        if (PhotonNetwork.IsMasterClient && playerCount == 1)
        {
           startGameButton.interactable=false;
        }
        
        SetConnectedPlayersText(playerCount, PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("Master client left. Destroying room and forcing all players to leave.");
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    private void StartGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void BackBtn()
    {
        
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        isBack=true;
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left the room");
        createRoomPanel.SetActive(true);
        joinedRoomPanel.SetActive(false);
        startGamePanel.SetActive(false);
    }
}
