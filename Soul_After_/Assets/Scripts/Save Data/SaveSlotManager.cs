using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Linq;

public class SaveSlotManager : MonoBehaviour
{
    private void OnEnable()
    {
        List<string> dateTimes = new List<string>();
        int curTimeIdx = 17;
        string defaultTimeMsg = " - ";

        GameObject.Find("Player").GetComponent<Player>().ispaused = true;
        this.transform.Find("Background/Save Slot/Slot 1/DateTime").GetComponent<Text>().text = defaultTimeMsg;
        this.transform.Find("Background/Save Slot/Slot 2/DateTime").GetComponent<Text>().text = defaultTimeMsg;
        this.transform.Find("Background/Save Slot/Slot 3/DateTime").GetComponent<Text>().text = defaultTimeMsg;


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
                    Array.Resize(ref results, results.Length - 1);
                    Debug.Log(results.Length);
                    Debug.Log(results[results.Length-1]);
                    Debug.Log(results[0]);
                    StringValue saveTime = ScriptableObject.CreateInstance(typeof(StringValue)) as StringValue;
                    JsonUtility.FromJsonOverwrite(results[curTimeIdx], saveTime);
                    dateTimes.Add(saveTime.initialValue);
                    file.Close();
                }
                else dateTimes.Add(defaultTimeMsg);
            }
            catch (Exception e)
            {
                dateTimes.Add(defaultTimeMsg);
                Debug.LogWarning(e.StackTrace);
            }
        }

        Debug.Log(dateTimes.Count);

        this.transform.Find("Background/Save Slot/Slot 1/DateTime").GetComponent<Text>().text += dateTimes[0];
        this.transform.Find("Background/Save Slot/Slot 2/DateTime").GetComponent<Text>().text += dateTimes[1];
        this.transform.Find("Background/Save Slot/Slot 3/DateTime").GetComponent<Text>().text += dateTimes[2];
    }

    public void SaveInSlot(int slotNo)
    {
        bool result = GameObject.Find("GameSaveManager").GetComponent<GameSaveManager>().SaveFunc(slotNo);

        if (result)
            Debug.Log("저장 성공!");
        else
            Debug.Log("저장 실패!");

        gameObject.SetActive(false);
        Exit();
    }

    public void Exit()
    {
        GameObject.Find("Player").GetComponent<Player>().ispaused = false;
    }
}
