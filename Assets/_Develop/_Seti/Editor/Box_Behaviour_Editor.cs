using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Behaviour Box Custom Editor
    /// </summary>
    [CustomEditor(typeof(Box_Behaviour))]
    public class Box_Behaviour_Editor : Editor
    {
        private Box_Behaviour behaviourBox;

        public override void OnInspectorGUI()
        {
            behaviourBox = (Box_Behaviour)target;

            EditUtility.SubjectLine(2, "��� �����̳�");

            // �⺻ Inspector �׸���
            DrawDefaultInspector();

            // �������� ��ư
            if (GUILayout.Button("Behaviour List ����"))
            {
                RefreshBehaviourList();
            }

            // Behaviour ����Ʈ ǥ��
            DrawBehaviourList();

            EditUtility.DrawLine(2);

            // ���� ��� �޽��� �߰�
            EditorGUILayout.HelpBox("Behaviour Ŭ������ �����Ϸ��� ���� ����� Remove ��ư�� ���� ����ȭ ������ �켱������ �����ϱ� �����մϴ�.", MessageType.Warning);

        }

        private void RefreshBehaviourList()
        {
            // ������ Ŭ������ �ִ��� Ȯ���ϰ� ����
            behaviourBox.behaviours.RemoveAll(behaviour => behaviour == null);

            // IBehaviour�� ������ ��� Ŭ���� Ž��
            var allBehaviours = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeof(IBehaviour).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            int addedCount = 0;

            foreach (var behaviourType in allBehaviours)
            {
                // �ߺ� �˻�
                if (!behaviourBox.behaviours.Any(f => f.GetType() == behaviourType))
                {
                    var newBehaviour = Activator.CreateInstance(behaviourType) as IBehaviour;
                    behaviourBox.behaviours.Add(newBehaviour);
                    addedCount++;
                }
            }

            EditorUtility.SetDirty(behaviourBox); // ���� ���� ����
            Debug.Log($"Box_Behaviour ����: �� Behaviour�� {addedCount}�� �߰��Ǿ����ϴ�.");
        }

        private void DrawBehaviourList()
        {
            EditUtility.SubjectLine(Color.gray, 2, "Behaviour List");

            for (int i = 0; i < behaviourBox.behaviours.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(behaviourBox.behaviours[i].GetType().Name);

                if (GUILayout.Button("Remove"))
                {
                    behaviourBox.behaviours.RemoveAt(i);
                    EditorUtility.SetDirty(behaviourBox);
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}