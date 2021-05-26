using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDrunk : MonoBehaviour
{
    int speed = 2;
    public int onChair = 0;
    TriggerChair tr;
    string nameChair = null;
    /*
    0 - first initial
    1 - way to chair
    2 - on chair
     */
    GameObject targetChair;

    public int getStatus()
    {
        return onChair;
    }

    public void setChair()
    {
        GameObject chairFromBar = getChairFromBar();
        if (targetChair != null) // освобождаем стул
        {
            tr = targetChair.GetComponent<TriggerChair>();
            tr.allowChair();
        }

        targetChair = chairFromBar;
        if (targetChair != null)
        {
            tr = targetChair.GetComponent<TriggerChair>();
        }

        onChair = 1;

    }


    void Update()
    {
        if (onChair == 1)
        {
            // движемся только когда стул свободен
            if (tr.getStatusChair())
            {
                transform.position += (targetChair.transform.position - transform.position).normalized * Time.deltaTime * speed; // нормализованный глючит
                if (Vector3.Distance(transform.position, targetChair.transform.position) < 0.03f)
                {
                    tr.busyChair(); // занимаем стул
                    onChair = 2;
                    nameChair = targetChair.name;
                    transform.position = targetChair.transform.position;
                    Debug.Log(targetChair.name + "_busy_" + tr.getStatusChair()) ;        
                    }
                }
                else
                {
                    targetChair = null;
                    tr = null;
                    setChair();
                }
        }
    }

    GameObject getChairFromBar() {
        GameObject chair = null;
        GameObject drunks = GameObject.Find("Bar");// ищем бар и в нем элементы
        if (drunks != null)
        {
            chair = drunks.GetComponent<Rule>().getAllowChair(nameChair); // запросим новый стул
        }
        else onChair = 0; // нет стульев стоим и ждем 
        
        return chair;


    }
}