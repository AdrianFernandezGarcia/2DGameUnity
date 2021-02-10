using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class SettingsPersistence : MonoBehaviour
{
    public static SettingsPersistence instance;
    private string SavePath => $"{Application.persistentDataPath}/GameSettings.bin";
    private FileStream fileStream;
    private BinaryFormatter formatter;

    private void Awake()
    {
        formatter = new BinaryFormatter();
       
    }

    //Singleton pattern
    public static SettingsPersistence Instance
    {
        get { return instance != null ? instance : (instance = new GameObject("SettingsPersistence").AddComponent<SettingsPersistence>()); }
    }

    public void SaveSettingsToFile(Settings settings)
    {
        fileStream = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
        formatter.Serialize(fileStream, settings);
        fileStream.Flush();
        fileStream.Close();
        fileStream.Dispose();
    }

    public Settings GetSettings()
    {
        fileStream = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
        try
        {
            object obj = formatter.Deserialize(fileStream);
            Settings settings = (Settings)obj;
            fileStream.Flush();
            fileStream.Close();
            fileStream.Dispose();
            return settings;
        }
        catch (SerializationException)
        {
            //return an empty settings object.
            Settings settings = null;
            return settings;
        }


    }

}


