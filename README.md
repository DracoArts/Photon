 #                      Welcome to DracoArts
![Logo](https://dracoarts-logo.s3.eu-north-1.amazonaws.com/DracoArts.png)


# Photon Engine

Photon refers to the integration of the Photon Unity Networking (PUN) or Photon Quantum SDKs into Unity, a popular game development engine. Photon is a real-time multiplayer game development framework developed by Exit Games, designed to simplify the process of creating multiplayer games by handling networking, synchronization, and server management.

Here’s an overview of Unity Photon and its key features:

# What is Photon in Unity?
Photon is a cloud-based networking solution that allows developers to create multiplayer games without needing to manage complex server infrastructure. It provides tools and APIs to handle real-time communication between players, making it easier to build games with features like matchmaking, room management, and player synchronization.

# Key Features of Photon in Unity
Photon Unity Networking (PUN):

Designed for turn-based or real-time multiplayer games.

Provides a high-level API for managing rooms, players, and game state synchronization.

Supports up to 20 players per room (depending on the plan).

Easy to integrate into Unity projects.

# Cloud Hosting:

Photon servers are hosted in the cloud, reducing the need for developers to manage their own servers.

Global server coverage ensures low latency for players worldwide.

# Matchmaking and Rooms:

Automates the process of finding and joining games.

Supports creating public or private rooms with customizable properties.

# Cross-Platform Support:

Works across multiple platforms, including PC, mobile, and consoles.

Ensures seamless multiplayer experiences regardless of the device.

# Scalability:

Handles varying player counts and game sizes, from small indie games to large-scale multiplayer titles.

# Real-Time Communication:

Supports real-time updates for player positions, game events, and other in-game actions.

 # Integration with Unity:

Photon provides a Unity SDK with pre-built scripts, demos, and documentation to streamline development.

 # How to Use Photon in Unity
Set Up a Photon Account:

Create an account on the 
[Photon Engine website](https://www.photonengine.com/)

Obtain your App ID for authentication.

# Install the Photon SDK:

Download the Photon Unity Networking (PUN)  from the Unity Asset Store or the Photon website.

Import the SDK into your Unity project.

Configure Photon Settings:

Add your App ID to the Photon settings in Unity.

Set up network callbacks and event handlers.


## Usage/Examples
  
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

--------


## Images
![Logo](https://raw.githubusercontent.com/AzharKhemta/DemoClient/refs/heads/main/Photon.gif)
## Authors

- [@DracoArts](https://github.com/orgs/DracoArts)

 - 


## 🔗 Links

[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/company/mir-hamza-hasan/posts/?feedView=all/)
## Tech Stack
**Client:** Unity,C#

**Server:** Photon Pun 2,

