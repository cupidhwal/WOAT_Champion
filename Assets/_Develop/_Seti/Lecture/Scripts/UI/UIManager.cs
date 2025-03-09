using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        // 필드
        #region Variables
        public ItemDatabase database;

        public PlayerStatusUI playerStatusUI;
        public PlayerEquipmentUI playerEquipmentUI;
        public PlayerInventoryUI playerInventoryUI;
        public DialogueUI dialogueUI;

        public int itemID = 0;
        #endregion

        // 메서드
        #region Methods
        public void Toggle(GameObject go)
        {
            go.SetActive(!go.activeSelf);
        }

        public void OpenDialogueUI(int dialogIndex)
        {
            Toggle(dialogueUI.gameObject);
            dialogueUI.StartDialogue(dialogIndex);
        }

        public void NextDialogueUI()
        {
            dialogueUI.DrawNextDialogue();
        }

        public void CloseDialogueUI()
        {
            Toggle(dialogueUI.gameObject);
        }

        public void AddNewItem(int index)
        {
            ItemObject itemObject = database.itemObjects[index];
            Item newItem = itemObject.CreateItem();

            playerInventoryUI.inventoryObject.AddItem(newItem, 1);
        }
        #endregion
    }
}