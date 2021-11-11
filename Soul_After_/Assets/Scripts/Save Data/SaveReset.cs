using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using UnityEngine;

// ���Ǵ� Scene: Start Menu
// ���: ó���ϱ� ���, ���� ����Ʈ ������ �ʱ�ȭ����
public class SaveReset : MonoBehaviour
{
    public GameSaveManager GameSaveManager;
    private List<ScriptableObject> objects;

    private void Start()
    {
        objects = GameSaveManager.objToSave;
    }

    public void ResetFunc()
    {
        foreach (ScriptableObject obj in objects)
        {
            string type = obj.GetType().ToString();
            Debug.Log(obj + " = " + type);

            switch (type)
            {
                case "StringValue":
                    {
                        StringValue ob = (StringValue)obj;
                        ob.initialValue = ob.defaultValue;
                        break;
                    }
                case "VectorValue":
                    {
                        VectorValue ob = (VectorValue)obj;
                        ob.initialValue = ob.defaultValue;
                        break;
                    }
                case "VectorList":
                    {
                        break;
                    }
                case "BoolValue":
                    {
                        BoolValue ob = (BoolValue)obj;
                        ob.initialValue = ob.defaultValue;
                        break;
                    }
                case "BoolList":
                    {
                        BoolList ob = (BoolList)obj;
                        ob.initialValue = new List<bool>();
                        foreach (bool b in ob.defaultValue)
                            ob.initialValue.Add(b);
                        break;
                    }
                case "AnimatorValue":
                    {
                        AnimatorValue ob = (AnimatorValue)obj;
                        ob.initialAnimator = ob.defaultAnimator;
                        break;
                    }
                case "CutsceneList":
                    {
                        CutsceneList ob = (CutsceneList)obj;
                        ob.initialValue = new List<string>();
                        foreach (string s in ob.defaultValue)
                            ob.initialValue.Add(s);
                        break;
                    }
                case "FloatValue":
                    {
                        FloatValue ob = (FloatValue)obj;
                        ob.initialValue = ob.defaultValue;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
