using UnityEditor;
using UnityEngine;

namespace Controllers
{
    [CustomEditor(typeof(AudioManager))]
    public class AudioManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var audioManager = (AudioManager) target;
            if(GUILayout.Button("Play Sound"))
            {
                // audioManager.PlaySound();
            }
        }
    }
}

