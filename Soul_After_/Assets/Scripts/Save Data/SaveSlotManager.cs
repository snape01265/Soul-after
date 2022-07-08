using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotManager : MonoBehaviour
{
    public StringValue curTime;
    private readonly string prefix = "- ";

    private void OnEnable()
    {
        List<string> dateTimes = new List<string>();
        int curTimeIdx = 17;
        string defaultTimeMsg = "No Save";
        

        GameObject.Find("Player").GetComponent<Player>().ispaused = true;

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
                    string[] results = ((string)binary.Deserialize(file)).Split('~');
                    Array.Resize(ref results, results.Length - 1);
                    StringValue saveTime = ScriptableObject.CreateInstance(typeof(StringValue)) as StringValue;
                    JsonUtility.FromJsonOverwrite(results[curTimeIdx], saveTime);
                    dateTimes.Add(DateTime.Parse(saveTime.initialValue).ToString("yyyy년 MM월 dd일 HH:mm"));
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

        this.transform.Find("Background/Save Slot/Slot 1/DateTime").GetComponent<Text>().text = prefix + dateTimes[0];
        this.transform.Find("Background/Save Slot/Slot 2/DateTime").GetComponent<Text>().text = prefix + dateTimes[1];
        this.transform.Find("Background/Save Slot/Slot 3/DateTime").GetComponent<Text>().text = prefix + dateTimes[2];
    }

    public void SaveInSlot(int slotNo)
    {
        curTime.initialValue = DateTime.Now.ToString();

        bool result = GameObject.Find("GameSaveManager").GetComponent<GameSaveManager>().SaveFunc(slotNo);

        if (result)
        {
            string loc = string.Format("Background/Save Slot/Slot {0}/DateTime", (slotNo + 1).ToString());
            this.transform.Find(loc).GetComponent<Text>().text = prefix + DateTime.Parse(curTime.initialValue).ToString("yyyy년 MM월 dd일 HH:mm");
            gameObject.transform.Find("Background/Save Slot/Save Comp").gameObject.SetActive(true);
            gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            Debug.Log("저장 실패!");
        }
    }

    public void Exit()
    {
        GameObject.Find("Player").GetComponent<Player>().ispaused = false;
    }
}
