using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("Save Locations")]
    private const string CharPlayerSub = "/playerData.dat";
    
    
    
    [Header("Other Components")]
    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = Singleton.Main.gameManager;
    }
    
    public void SavePlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string playerDataPath = Application.persistentDataPath + CharPlayerSub;

        FileStream playerDataStream = File.Create(playerDataPath);
        PlayerData playerData = new PlayerData(_gameManager);
        formatter.Serialize(playerDataStream, playerData);

        playerDataStream.Close();
    }
    
    public void LoadPlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string playerDataPath = Application.persistentDataPath + CharPlayerSub;

        if (File.Exists(playerDataPath))
        {
            FileStream playerDataStream = File.Open(playerDataPath, FileMode.Open);

            if (formatter.Deserialize(playerDataStream) is PlayerData playerData)
            {
                _gameManager.LoadPlayerData(playerData);
            }
            playerDataStream.Close();
        }
        else
        {            
            Debug.Log("Save file not found in " + playerDataPath + ". Creating new save file.");

            _gameManager.gold = 0;
            _gameManager.levelIndex = 0;
        }

    }
}
