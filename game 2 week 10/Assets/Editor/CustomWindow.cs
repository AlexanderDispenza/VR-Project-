using UnityEditor;
using UnityEngine;

public class CustomWindow : EditorWindow {

    Color color;

    [MenuItem("Custom/Colorizer")]

    public static void ShowWindow()
    {
        GetWindow<CustomWindow>("Colorizer");


    }



    // Variables

    // Window display items
    private void OnGUI()
    {
        GUILayout.Label("Color the selected object", EditorStyles.boldLabel);

        color = EditorGUILayout.ColorField("Color", color);

        if (GUILayout.Button("Colorizer"))
            Colorizer();
    }
    // Methods


    void Colorizer()
    {

        foreach(GameObject g in Selection.gameObjects)
        {

            Renderer ren = g.GetComponent<Renderer>();
            if (ren)
                ren.sharedMaterial.color = color;
        }
    }
}
