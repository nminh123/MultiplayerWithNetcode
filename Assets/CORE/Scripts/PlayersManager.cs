using Netcode.Core;
using Unity.Netcode;

public class PlayersManager : Singleton<PlayersManager>
{
    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();

    public int PlayersInGame
    {
        get 
        {
            return playersInGame.Value;
        }
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if(IsServer)
            {
                Logger.Instance.LogInfo($"{id} just connected......");
                playersInGame.Value++;
            }
        };
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if(IsServer)
            {
                Logger.Instance.LogInfo($"{id} just disconnected......");
                playersInGame.Value--;
            }
        };
    }
}