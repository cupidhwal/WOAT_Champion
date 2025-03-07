using UnityEngine;
using UnityEditor;

namespace Seti
{
    [CustomEditor(typeof(MacroMECH))]
    public class Editor_MacroMECH : Editor
    {
        //public override void OnInspectorGUI()
        //{
        //    MacroMECH macroMECH = (MacroMECH)target;
        //    serializedObject.Update();

        //    // DB 필드 수동 렌더링
        //    macroMECH.receiverDB = (DB_Receiver)EditorGUILayout.ObjectField("Receiver DB", macroMECH.receiverDB, typeof(DB_Receiver), true);
        //    macroMECH.transducerDB = (DB_Transducer)EditorGUILayout.ObjectField("Transducer DB", macroMECH.transducerDB, typeof(DB_Transducer), true);
        //    macroMECH.propulsorDB = (DB_Propulsor)EditorGUILayout.ObjectField("Propulsor DB", macroMECH.propulsorDB, typeof(DB_Propulsor), true);

        //    EditorGUILayout.Space();
        //    EditorGUILayout.LabelField("Census Data", EditorStyles.boldLabel);

        //    // census 필드를 읽기 전용으로 표시
        //    GUI.enabled = false;
        //    EditorGUILayout.IntField("Census Receivers", macroMECH.Census_Receivers);
        //    EditorGUILayout.IntField("Census Transducers", macroMECH.Census_Transducers);
        //    EditorGUILayout.IntField("Census Propulsors", macroMECH.Census_Propulsors);
        //    GUI.enabled = true;

        //    serializedObject.ApplyModifiedProperties();
        //}
    }
}