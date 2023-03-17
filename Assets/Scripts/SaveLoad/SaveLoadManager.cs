using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveIsEasy;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SaveLoad
{
    public class SaveLoadManager : MonoBehaviour
    {
        public static SaveLoadManager instance;

        [SerializeField] private Button loadBtn;

        private int saveAvailable = -1;

        private void Awake()
        {
            instance = this;
        }

        public void CheckSaves()
        {
            var saves = SaveIsEasyAPI.ListOfValidSaves();
            int saveAvailable = -1;
            for (int i = 0; i < saves.Count; i++)
            {
                if (saves[i].GetQuickAccessString("gameVersion") == Application.version) { saveAvailable = i; break; }
            }
            this.saveAvailable = saveAvailable;
            UpdateBtnState();
        }

        private void UpdateBtnState()
        {
            loadBtn.interactable = saveAvailable >= 0;
        }

        public void Save()
        {
            var selected = SceneManager.GetSceneAt(0);
            SaveIsEasyAPI.SetSceneFileNameByScene("MainSave", selected);
            SaveIsEasyAPI.SaveAll(selected);
            CheckSaves();
        }

        public void Load()
        {
            CheckSaves();

            var selected = SceneManager.GetSceneAt(0);
            var saves = SaveIsEasyAPI.ListOfValidSaves();

            SaveIsEasyAPI.SetSceneFileNameByScene(saves[saveAvailable].Name, selected);
            SaveIsEasyAPI.LoadAll(selected);

            NewGameInfo.instance.loadingSave = false;
        }

        public void DoLoad()
        {
            NewGameInfo.instance.loadingSave = true;
            LoadingScript.instance.LoadScene(2);
        }
    }
}
