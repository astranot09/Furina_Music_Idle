using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int point;
    public float masterVolume = 1;
    public float bgmVolume = 1;
    public float sfxVolume = 1;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data,true);
        string path = Application.persistentDataPath + "/save.json";
        File.WriteAllText(path, json);
        Debug.Log(path);
    }
    public SaveData Load()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (!File.Exists(path))
        {
            return new SaveData();
        }
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }


    public void SavePoint(int point)
    {
        SaveData data = Load();   // ambil data lama (volume dll tetap aman)
        data.point = point;       // ubah point saja
        Save(data);
    }
    public void SaveVolume(float master, float bgm, float sfx)
    {
        SaveData data = Load();
        data.masterVolume = master;
        data.bgmVolume = bgm;
        data.sfxVolume = sfx;
        Save(data);
    }
}
