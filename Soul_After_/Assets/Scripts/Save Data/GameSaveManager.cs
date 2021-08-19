using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;
    public List<ScriptableObject> objToSave;

    private void Awake()
    {
        if (gameSave == null)
        {
            gameSave = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        LoadScriptables();
    }

    public void SaveGame()
    {
        SaveScriptables(objToSave);
    }

    public void SaveScriptables(List<ScriptableObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath +
                string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        for (int i = 0; i < objToSave.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                    string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objToSave[i]);
                file.Close();
            }
        }
    }

    // 슬롯 번호로 데이터를 저장하여 성공시 true를 실패시 false 반환
    public bool SaveFunc(int slotNo)
    {
        try
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath +
            string.Format("/Save{0}.dat", slotNo));

            string results = "";
            foreach (ScriptableObject scriptable in objToSave)
            {
                results += JsonUtility.ToJson(scriptable) + "_";
                Debug.Log(JsonUtility.ToJson(scriptable));
                Debug.Log("results is " + results);
            }

            binary.Serialize(file, results);

            file.Close();
            return true;
        } catch (Exception e)
        {
            Debug.LogWarning(e.StackTrace);
            return false;
        }
        
    }

    // 슬롯 번호로 데이터를 로드하여 성공시 true를 실패시 false 반환
    public bool LoadFunc(int slotNo)
    {
        try
        {
            if (File.Exists(Application.persistentDataPath +
            string.Format("/Save{0}.dat", slotNo)))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                    string.Format("/Save{0}.dat", slotNo), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                string[] results = ((string)binary.Deserialize(file)).Split('_');
                for (int i = 0; i < objToSave.Count; i++)
                {
                    JsonUtility.FromJsonOverwrite(results[i], objToSave[i]);
                }
                file.Close();
                return true;
            }
            else return false;
        } catch (Exception e)
        {
            Debug.LogWarning(e.StackTrace);
            return false;
        }
    }

    // 시간상으로 최근 데이터 로드
    public void LoadRecent()
    {
        List<DateTime> dateTimes = new List<DateTime>();
        int slotNo = 0;
        int curTimeIdx = 17;

        for (int n = 0; n < 3; n++)
        {
            try
            {
                if (File.Exists(Application.persistentDataPath +
                string.Format("/Save{0}.dat", n)))
                {
                    FileStream file = File.Open(Application.persistentDataPath +
                        string.Format("/Save{0}.dat", n), FileMode.Open);
                    BinaryFormatter binary = new BinaryFormatter();
                    string[] results = ((string)binary.Deserialize(file)).Split('_');
                    StringValue saveTime = ScriptableObject.CreateInstance(typeof(StringValue)) as StringValue;
                    JsonUtility.FromJsonOverwrite(results[curTimeIdx], saveTime);
                    dateTimes.Add(DateTime.Parse(saveTime.initialValue));
                    file.Close();
                }
                else dateTimes.Add(DateTime.MinValue);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.StackTrace);
            }
        }

        for (int i = 1; i < 2; i++)
        {
            if (DateTime.Compare(dateTimes[slotNo], dateTimes[i]) < 0) 
            {
                slotNo = i;
            }
        }

        try
        {
            if (File.Exists(Application.persistentDataPath +
            string.Format("/Save{0}.dat", slotNo)))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                    string.Format("/Save{0}.dat", slotNo), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                string[] results = ((string)binary.Deserialize(file)).Split('_');
                for (int i = 0; i < objToSave.Count; i++)
                {
                    JsonUtility.FromJsonOverwrite(results[i], objToSave[i]);
                }
                file.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.StackTrace);
        }
    }
}