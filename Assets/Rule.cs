using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule : MonoBehaviour
{
    public List<GameObject> listChair = new List<GameObject>();
    public List<GameObject> listDrunks = new List<GameObject>();
    bool firstDownButton = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject drunks = GameObject.Find("Drunks");// ищем хранилище поситителей
        if (drunks != null)
        {
           foreach (Transform child in drunks.transform)
           {
               listDrunks.Add(child.gameObject);
           }
        }

          foreach (Transform child in this.transform)
          {
            listChair.Add(child.gameObject);
          }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
         
           foreach (GameObject drunk in listDrunks) // проход по поситителям
           {
                if (firstDownButton) // один раз нажали уже
                {
                    if (!getLuckCheck45()) continue; // пропуск если не 45 вернули
                }
                        
                StatusDrunk st = drunk.GetComponent<StatusDrunk>();
                if (st != null) {
                    st.setChair();
                }
           }
            if (!firstDownButton) firstDownButton = true;

        }

    }


    public GameObject getAllowChair(string name) {
        List<GameObject> allowsChair = new List<GameObject>();
        foreach (GameObject chairTmp in listChair) // проход по стульям Листа
        {
            TriggerChair st = chairTmp.GetComponent<TriggerChair>();
            if (st != null)
                if (st.getStatusChair()) // стул свободен
                {
                    allowsChair.Add(chairTmp);
                }
        }
        if (allowsChair.Count > 1) {
            foreach ( GameObject chairTmp in allowsChair) // уже по свободным
            {
                if (chairTmp.name.Equals(name))
                {
                    allowsChair.Remove(chairTmp); // удаляем стул с таким же именем
                    break;
                   
                }

            }
        }
        GameObject[] arrChair = allowsChair.ToArray();
        int identChair = Random.Range(0, arrChair.Length);
        return arrChair[identChair];
    }



        private bool getLuckCheck45() {
        bool luckBool;
        int[] randArrCheck = new int[100];
        for (int i = 0; i < randArrCheck.Length; ++i)
        {
            if (i < 45) randArrCheck[i] = 1;
            else randArrCheck[i] = 0;
        }
        int luck = randArrCheck[Random.Range(0, randArrCheck.Length)];
        Debug.Log(luck);
        if(luck == 1) luckBool = true;
        else luckBool = false;
        return luckBool;
    }
}
