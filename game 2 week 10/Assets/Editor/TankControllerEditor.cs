using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

// Declare type of Custom Editor
[CustomEditor(typeof(TankController))] //1
public class TankControllerEditor : Editor 
{
    float thumbnailWidth = 70;
    float thumbnailHeight = 70;
    float labelWidth = 150f;

    string playerName = "Player 1";
    string playerLevel = "1";
    string playerElo = "5";
    string playerScore = "100";

    // OnInspector GUI
    public override void OnInspectorGUI()
    { 
        // Call base class method
        base.OnInspectorGUI();





        // Custom form for Player Preferences
        TankController tank = (TankController)target;

        GUILayout.Space(20f);
        GUILayout.Label("Custom Edditor Elements", EditorStyles.boldLabel);

        GUILayout.Space(20f);
        GUILayout.Label("Player prefrence", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Player Name", GUILayout.Width(labelWidth));
        playerName = GUILayout.TextField(playerName);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Player Score", GUILayout.Width(labelWidth));
        playerScore = GUILayout.TextField(playerScore);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Player Level", GUILayout.Width(labelWidth));
        playerLevel = GUILayout.TextField(playerLevel);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Player Elo", GUILayout.Width(labelWidth));
        playerElo = GUILayout.TextField(playerElo);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.SetString("PlayerScore", playerScore);
            PlayerPrefs.SetString("PlayerLevel", playerLevel);
            PlayerPrefs.SetString("PlayerElo", playerElo);

            Debug.Log("Player pref saved");
        }
        if (GUILayout.Button("Reset"))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Player pref deleted");

        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Spawn Props");
        GUILayout.BeginHorizontal();

        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/Board_Thumbnail"), GUILayout.Width(thumbnailWidth), GUILayout.Height(thumbnailHeight)))
        {
            tank.SpawnProp("board");

        }

        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/OilDrum_Thumbnail"), GUILayout.Width(thumbnailWidth), GUILayout.Height(thumbnailHeight)))
        {
            tank.SpawnProp("oil_drum");

        }

        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/Crate_Thumbnail"), GUILayout.Width(thumbnailWidth), GUILayout.Height(thumbnailHeight)))
        {
            tank.SpawnProp("crate");

        }

        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/TrafficCone_Thumbnail"), GUILayout.Width(thumbnailWidth), GUILayout.Height(thumbnailHeight)))
        {
            tank.SpawnProp("Traffic_cone");

        }
        if (GUILayout.Button(Resources.Load<Texture>("Thumbnails/Wheel_Thumbnail"), GUILayout.Width(thumbnailWidth), GUILayout.Height(thumbnailHeight)))
        {
            tank.SpawnProp("wheel_cone");

        }
        GUILayout.EndHorizontal();
        // Custom Buttons with Image as Thumbnail

    }
}
