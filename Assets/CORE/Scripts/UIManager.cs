using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button startHost, startServer, startClient;
    public TextMeshProUGUI playersInGameText;

    public void Awake()
    {
        Cursor.visible = true;
    }

    public void Update()
    {
        playersInGameText.text = $"Player in game: {PlayersManager.Instance.PlayersInGame}";
    }

    public void Start()
    {
        startHost.onClick.AddListener(() => {
            if(NetworkManager.Singleton.StartHost())
            {
                Netcode.Core.Logger.Instance.LogInfo("Host Started........");
            }
            else
            {
                Netcode.Core.Logger.Instance.LogInfo("Host could not Started........");
            }
        });

        startServer.onClick.AddListener(() => {
            if(NetworkManager.Singleton.StartServer())
            {
                Netcode.Core.Logger.Instance.LogInfo("Server Started........");
            }
            else
            {
                Netcode.Core.Logger.Instance.LogInfo("Server could not Started........");
            }
        });

        startClient.onClick.AddListener(() => {
            if(NetworkManager.Singleton.StartClient())
            {
                Netcode.Core.Logger.Instance.LogInfo("Client Started........");
            }
            else
            {
                Netcode.Core.Logger.Instance.LogInfo("Client could not Started........");
            }
        });
    }
}