using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;

public class GameDataEditor : EditorWindow
{
    public GameData gameData;

    private string gameDataProjectfilePath = "/StreamingAssets/data.json";


    [MenuItem("Window/Game Data Editor")]
    static void Init()
    {
        GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow(typeof(GameDataEditor));
        window.Show();
    }


    void OnGUI()
    {
        if(gameData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty =  serializedObject.FindProperty("gameData");

            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save Data"))
            {
                SaveGameData();
            }
        }
        if(GUILayout.Button ("Load Data"))
        {
            LoadGameData();
        }
    }


    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataProjectfilePath;

        if (File.Exists (filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            gameData = new GameData();
        }
    }
	
    private void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + gameDataProjectfilePath;
        File.WriteAllText(filePath, dataAsJson);
    }
}
