using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveIsEasy;

namespace SaveLoad 
{
    public class SaveAdapter : MonoBehaviour, ISaveIsEasyEvents
    {
        [SerializeField] private PersonaSaver[] availableWorkers;
        [SerializeField] private PersonaSaver[] hiredWorkers;
        [SerializeField] private OfficeState[] officeStates;

        [SerializeField] private bool[] platfromsBoughtStatuses;
        [SerializeField] private bool[] enginesBoughtStatuses;

        [SerializeField] private GameSaver[] games;

        public void OnLoad()
        {
            DoLoad();
        }

        public void OnSave()
        {
            DoSave();
        }

        private void DoSave()
        {
            // roster and buildings
            availableWorkers = new PersonaSaver[RosterManager.instance.availableWorkers.Count];
            for (int i = 0; i < availableWorkers.Length; i++)
            {
                availableWorkers[i] = new PersonaSaver(RosterManager.instance.availableWorkers[i]);
            }
            hiredWorkers = new PersonaSaver[RosterManager.instance.hiredWorkers.Count];
            for (int i = 0; i < hiredWorkers.Length; i++)
            {
                hiredWorkers[i] = new PersonaSaver(RosterManager.instance.hiredWorkers[i]);
            }

            // offices
            officeStates = new OfficeState[OfficeManager.instance.offices.Length];
            for (int i = 0; i < officeStates.Length; i++)
            {
                officeStates[i] = OfficeManager.instance.offices[i].state;
            }

            // engines and platforms
            platfromsBoughtStatuses = new bool[PlatformsManager.instance.platforms.Length];
            for (int i = 0; i < platfromsBoughtStatuses.Length; i++)
            {
                platfromsBoughtStatuses[i] = PlatformsManager.instance.platforms[i].boughted;
            }
            enginesBoughtStatuses = new bool[AttributesManager.instance.engines.Length];
            for (int i = 0; i < enginesBoughtStatuses.Length; i++)
            {
                enginesBoughtStatuses[i] = AttributesManager.instance.engines[i].boughted;
            }

            // games
            games = new GameSaver[Statistics.instance.games.Count];
            for (int i = 0; i < games.Length; i++)
            {
                games[i] = new GameSaver(Statistics.instance.games[i]);
            }
        }

        private void DoLoad()
        {
            // roster and buildings
            var buildings = FindObjectsOfType<Building>();
            BuildingManager.instance.buildings = new List<Building>(buildings);

            RosterManager.instance.availableWorkers.Clear();
            for (int i = 0; i < availableWorkers.Length; i++)
            {
                RosterManager.instance.availableWorkers.Add(new Persona(availableWorkers[i]));
            }
            RosterManager.instance.hiredWorkers.Clear();
            for (int i = 0; i < hiredWorkers.Length; i++)
            {
                RosterManager.instance.hiredWorkers.Add(new Persona(hiredWorkers[i]));
                if (hiredWorkers[i].tableId >= 0)
                {
                    RosterManager.instance.selectedTable = GetPersonaTable(hiredWorkers[i].tableId, buildings);
                    RosterManager.instance.AssignWorker(RosterManager.instance.hiredWorkers[RosterManager.instance.hiredWorkers.Count - 1]);
                }
            }
            BuildingManager.instance.InvokeBuilded();
            StartCoroutine(TableActions());

            // offices 
            for (int i = 0; i < officeStates.Length; i++)
            {
                OfficeManager.instance.offices[i].state = officeStates[i];
            }
            OfficeManager.instance.SetOfficeObj(false);

            // platforms and engines
            for (int i = 0; i < platfromsBoughtStatuses.Length; i++)
            {
                PlatformsManager.instance.platforms[i].SetBoughted(platfromsBoughtStatuses[i]);
            }
            for (int i = 0; i < enginesBoughtStatuses.Length; i++)
            {
                AttributesManager.instance.engines[i].SetBoughted(enginesBoughtStatuses[i]);
            }

            // games
            Statistics.instance.games.Clear();  
            for (int i = 0; i < games.Length; i++)
            {
                Statistics.instance.games.Add(new Game(games[i]));
            }

            // oth
            MainUI.instance.ChangeMoneyText();
            Test();
        }

        private Table GetPersonaTable(int tableId, Building[] buildings)
        {
            for (int i = 0; i < buildings.Length; i++)
            {
                if (buildings[i].buildingId == tableId) return buildings[i] as Table;
            }
            Debug.LogError("There is no such table id in current buildings");
            return null;
        }

        private IEnumerator TableActions()
        {
            yield return new WaitForEndOfFrame();
            ComputerManager.instance.CheckForPcUpdates();
            var tables = FindObjectsOfType<Table>();
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].AfterLoading(); Debug.Log("Rotated");
            }
        }

        private void Test()
        {

        }

    }
}