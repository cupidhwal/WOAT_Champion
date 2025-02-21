using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Seti
{
    [InitializeOnLoad]
    public static class Utility_Factory_Actor
    {
        static Utility_Factory_Actor()
        {
            // ���̾��Ű ���� �� �� �ε� ������ ����ȭ
            EditorApplication.hierarchyChanged += SyncAllFactories;
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
            EditorSceneManager.sceneLoaded += (scene, mode) => SyncAllFactories();

            // SceneView �̺�Ʈ�� ����
            //SceneView.duringSceneGui += OnSceneGUI;
        }

        /// <summary>
        /// ��� Factory_Actor �ν��Ͻ��� ����ȭ
        /// </summary>
        private static void SyncAllFactories()
        {
            // Factory_Actor �ν��Ͻ� ã��
            var factoryInstances = Resources.FindObjectsOfTypeAll<Factory_Actor>();
            foreach (var factory in factoryInstances)
            {
                SyncFactoryWithScene(factory);
            }

            // �������� ��� �ν����� â ����
            RepaintAllInspectors();
        }

        /// <summary>
        /// Ư�� Factory_Actor �ν��Ͻ��� ���� ���� ���Ϳ� ����ȭ
        /// </summary>
        private static void SyncFactoryWithScene(Factory_Actor factory)
        {
            // ���� ���� ��� Actor ã��
#pragma warning disable CS0618 // ���� �Ǵ� ����� ������ �ʽ��ϴ�.
            var actorsInScene = Object.FindObjectsOfType<Actor>();
#pragma warning restore CS0618 // ���� �Ǵ� ����� ������ �ʽ��ϴ�.

            // ���丮�� SpawnedActors ����Ʈ�� ���� Actor�� ����ȭ
            factory.madeObjects.Clear();
            foreach (var actor in actorsInScene)
            {
                if (!factory.madeObjects.Contains(actor.gameObject))
                {
                    factory.madeObjects.Add(actor.gameObject);
                }
            }

            // ���丮 ������ ����
            EditorUtility.SetDirty(factory);
        }

        /// <summary>
        /// ReorderableList�� Actor�� Hierarchy�� ����ȭ
        /// </summary>
        private static void OnHierarchyChanged()
        {
            var factoryInstances = Resources.FindObjectsOfTypeAll<Factory_Actor>();

            foreach (var factory in factoryInstances)
            {
                // ���̾��Ű ������ ����ȭ
                factory.madeObjects = factory.madeObjects
                    .OrderBy(actor => actor.transform.GetSiblingIndex())
                    .ToList();

                EditorUtility.SetDirty(factory);
            }

            RepaintAllInspectors();
        }

        /// <summary>
        /// �������� ��� �ν����� â ����
        /// </summary>
        private static void RepaintAllInspectors()
        {
            foreach (var inspector in Resources.FindObjectsOfTypeAll<EditorWindow>())
            {
                if (inspector.GetType().Name == "InspectorWindow")
                {
                    inspector.Repaint();
                }
            }
        }

        /// <summary>
        /// ReorderableList�� Actor�� Ű �Է����� ���� - ���� �۵� �� ��
        /// </summary>
        /// <param name="sceneView"></param>
        /*private static void OnSceneGUI(SceneView sceneView)
        {
            Event e = Event.current;

            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Delete)
            {
                // ���� ���õ� ���丮 ������Ʈ ��������
                var factory = Selection.activeObject as Factory_Actor;

                if (factory != null)
                {
                    // ���õ� ReorderableList�� �׸� �ε��� ��������
                    var editor = Editor.CreateEditor(factory) as Factory_Actor_Editor;
                    if (editor != null && editor.SpawnedActorsList.index >= 0)
                    {
                        int selectedIndex = editor.SpawnedActorsList.index;
                        if (selectedIndex >= 0 && selectedIndex < factory.MadeObjects.Count)
                        {
                            GameObject selectedActor = factory.MadeObjects[selectedIndex];
                            factory.DestroyActor(selectedActor);
                            editor.UpdateSpawnedActorsList();
                            editor.Repaint();

                            Debug.Log($"'{selectedActor.name}'�� �����Ǿ����ϴ�.");
                        }
                    }
                }

                e.Use(); // �̺�Ʈ �Һ�
            }
        }*/
    }
}