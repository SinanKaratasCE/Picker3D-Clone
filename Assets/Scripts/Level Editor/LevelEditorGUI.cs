using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorGUI : Editor
{
    [SerializeField]
    private string platformValueString = "0";
    [SerializeField]
    private string collectablesValueString = "0"; 
    [SerializeField]
    private string getLevelIndexString = "0";
    [SerializeField]
    private string createLevelIndexString = "0";
    GUIStyle horizontalLine;


    public override void OnInspectorGUI()
    {
        LevelEditor levelEditor = (LevelEditor)target;
        var oldColor = GUI.backgroundColor;
        GameObject obj;
        
        //Create Line Features
        horizontalLine = new GUIStyle();
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
        horizontalLine.margin = new RectOffset( 0, 0, 30, 30 );
        horizontalLine.fixedHeight = 1;
        
        GUILayout.Label("Level Editor");

        Rect createLevelRect = EditorGUILayout.GetControlRect();
        createLevelIndexString = EditorGUI.TextField(createLevelRect, "CreateLevelIndex", createLevelIndexString);
        
        int createLevelIndex = 0;
        if (int.TryParse(createLevelIndexString, out createLevelIndex))
        {
            levelEditor.ChangeWantedLevelIndex(createLevelIndex);
        }
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Create Level", GUILayout.MinWidth(100), GUILayout.MinHeight(50)))
        {
            levelEditor.CreateNewLevel(createLevelIndex);
        }
        
        
        
        //Create Line
        GUI.backgroundColor = oldColor;
        HorizontalLine( Color.black );
        
        Rect getLevelRect = EditorGUILayout.GetControlRect();
        getLevelIndexString = EditorGUI.TextField(getLevelRect, "LevelIndex", getLevelIndexString);
        
        int getLevelIndex = 0;
        if (int.TryParse(getLevelIndexString, out getLevelIndex))
        {
            levelEditor.ChangeWantedLevelIndex(getLevelIndex);
        }
        
        GUI.backgroundColor = Color.blue;
        if (GUILayout.Button("Get Level", GUILayout.MinWidth(100), GUILayout.MinHeight(50)))
        {
            levelEditor.GetLevel();
        }
        
        GUI.backgroundColor = oldColor;
        HorizontalLine( Color.black );
        
        Rect collectablesRect = EditorGUILayout.GetControlRect();
        collectablesValueString = EditorGUI.TextField(collectablesRect, "Collectables Index To Add", collectablesValueString);
        
        int collectablesGroupEnumValue = 0;
        if (int.TryParse(collectablesValueString, out collectablesGroupEnumValue))
        {
            levelEditor.ChangeCollectableGroupToAdd(collectablesGroupEnumValue);
        }
        
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Add Collectables To Level", GUILayout.MinWidth(100), GUILayout.MinHeight(50)))
        {
            levelEditor.AddCollectableObject();
        }
        
        
        GUI.backgroundColor = oldColor;
        HorizontalLine( Color.black );
        
        
        Rect platformRect = EditorGUILayout.GetControlRect();
        platformValueString = EditorGUI.TextField(platformRect, "Platform Index To Add", platformValueString);
        
        int platformTypeEnumValue = 0;
        if (int.TryParse(platformValueString, out platformTypeEnumValue))
        {
            levelEditor.ChangePlatformToAdd(platformTypeEnumValue);
        }
        
        GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Add Platform To Level", GUILayout.MinWidth(100), GUILayout.MinHeight(50)))
        {
            levelEditor.AddPlatformObject();
        }
        
        GUI.backgroundColor = oldColor;
        HorizontalLine( Color.black );
        
        GUI.backgroundColor = Color.yellow;
        if (GUILayout.Button("Save/Update Level", GUILayout.MinWidth(100), GUILayout.MinHeight(50)))
        {
            levelEditor.SaveLevel();
        }
        
        GUI.backgroundColor = oldColor;
        HorizontalLine( Color.black );
        
        DrawDefaultInspector();
        
    }
    
    private void HorizontalLine ( Color color ) {
        var c = GUI.color;
        GUI.color = color;
        GUILayout.Box( GUIContent.none, horizontalLine );
        GUI.color = c;
    }

}
