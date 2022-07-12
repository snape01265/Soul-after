using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LoadSlotManager : MonoBehaviour
{
    private void OnEnable()
    {
        List<string> dateTimes = new List<string>();
        int curTimeIdx = 17;
        string defaultTimeMsg = "No Save";
        string prefix = "- ";

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

        for (int i = 0; i < 3; i++)
        {
            if (dateTimes[i] == defaultTimeMsg)
                transform.Find($"Background/LoadSlot/Slot_{i+1}").GetComponent<Button>().interactable = false;
            this.transform.Find($"Background/LoadSlot/Slot_{i+1}/DateTime").GetComponent<Text>().text = prefix + dateTimes[i];
        }
    }

    public void LoadInSlot(int slotNo)
    {
        bool result = GameObject.Find("GameSaveManager").GetComponent<GameSaveManager>().LoadFunc(slotNo);

        if (result)
            Debug.Log("로드 성공!");
        else
            Debug.Log("로드 실패!");
    }
}
