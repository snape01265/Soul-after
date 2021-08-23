using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// 사용되는 Scene: Start Menu
// 기능: 처음하기 기능, 각각 디폴트 값으로 초기화해줌
public class SaveReset : MonoBehaviour
{
    public static SaveReset gameSave;
    public List<ScriptableObject> objects;

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
                        VectorList ob = (VectorList)obj;
                        ob.defaultPos = new List<Vector2>();
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
                        ob.defaultValue = new List<bool>();
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
                        ob.defaultValue = new List<string>();
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
