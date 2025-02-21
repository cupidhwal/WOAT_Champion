using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Factory�� ���ϼ��� �����ϱ� ���� �̱��� �������� ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Factory<T> : ScriptableObject where T : Factory<T>
    {
        // �̱���
        #region Singleton
        private static T instance;

        /// <summary>
        /// ������ Factory �ν��Ͻ�
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // Resources �������� �ش� Factory Ÿ���� ���� �˻�
                    instance = Resources.Load<T>(typeof(T).Name);

                    if (instance == null)
                    {
                        Debug.LogError($"Factory<{typeof(T).Name}> �ν��Ͻ��� ã�� �� �����ϴ�. " +
                                       $"Resources ������ {typeof(T).Name} ������ ��ġ�ϼ���.");
                    }
                }
                return instance;
            }
        }
        #endregion

        // �ʵ�
        #region Variables
        [HideInInspector]
        [Tooltip("������ GameObject�� �����ϴ� ����Ʈ")]
        public List<GameObject> madeObjects = new();
        /// <summary>
        /// ������ ���͸� �б� �������� ��ȯ
        /// </summary>
        public IReadOnlyList<GameObject> MadeObjects => madeObjects;
        #endregion

        /// <summary>
        /// �ʱ�ȭ ���� (�ʿ�� �������̵�)
        /// </summary>
        public virtual void Initialize()
        {
            Debug.Log($"{typeof(T).Name} �ʱ�ȭ �Ϸ�");
        }

        /*[InitializeOnLoadMethod]
        private static void EnsureUniqueInstance()
        {
            // ������Ʈ ���� ��� Factory_Actor ���� �˻�
            var allInstances = AssetDatabase.FindAssets("t:Factory_Actor");

            if (allInstances.Length > 1)
            {
                Debug.LogWarning("Factory_Actor�� ������Ʈ ���� �ϳ��� �����ؾ� �մϴ�!");
                foreach (var guid in allInstances)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    Debug.Log($"Factory_Actor ���� ���: {path}");
                }
            }
        }*/
    }

    /*public static class FactoryMenu
    {
        [MenuItem("Assets/Create/Factory/Actor", true)] // ���Ǻ� Ȱ��ȭ
        private static bool ValidateCreateFactoryActor()
        {
            // ���ϼ� �˻�: ������Ʈ ���� Factory_Actor�� �̹� �����ϸ� ��Ȱ��ȭ
            var existingFactories = AssetDatabase.FindAssets("t:Factory_Actor");
            return existingFactories.Length == 0;
        }

        [MenuItem("Assets/Create/Factory/Actor")]
        private static void CreateFactoryActor()
        {
            // ���丮 ����
            var factory = ScriptableObject.CreateInstance<Factory_Actor>();

            // Resources ������ ���� ����
            var path = "Assets/Resources/Factory_Actor.asset";
            AssetDatabase.CreateAsset(factory, path);
            AssetDatabase.SaveAssets();

            Debug.Log("Factory_Actor ������ �����Ǿ����ϴ�.");
        }
    }*/
}