using UnityEditor;
using UnityEngine;
    using UnityEditor;
#if UNITY_EDITOR

#endif
public class QuitApp : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
            
        }
    }
}
