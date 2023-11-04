using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Library : MonoBehaviour {
        public static Library Main;
        public List<GameObject> CardPrefabs;
        [HideInInspector] public List<string> Keys;

        public void Awake()
        {
            if (Main)
                Destroy(gameObject);
            else
            {
                Main = this;
                DontDestroyOnLoad(gameObject);
            }
            Ini();
        }

        public void Ini()
        {
            Keys = new List<string>();
            for (int i = 0; i < CardPrefabs.Count; i++)
            {
                Card C = CardPrefabs[i].GetComponent<Card>();
                if (C)
                {
                    Keys.Add(C.GetGenerationKey());
                    /*if (C.GetComponent<Event>())
                        CombatControl.Main.AllEvent.Add(C.GetComponent<Event>());*/
                    continue;
                }
            }
        }

        public void EditorIni()
        {
            CardPrefabs = new List<GameObject>();
            Keys = new List<string>();
            foreach (LibraryUnit LU in GetComponentsInChildren<LibraryUnit>())
            {
                if (LU.Prefab.GetComponent<Card>())
                    CardPrefabs.Add(LU.Prefab);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject GetCard(string Key)
        {
            int i = Keys.IndexOf(Key);
            if (i < 0)
                return null;
            return CardPrefabs[i];
        }
    }
}