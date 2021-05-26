using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChair : MonoBehaviour
{
    public bool allowed = true;

    public bool getStatusChair() {
        return allowed;
    }
    public void busyChair()
    {
        allowed = false;
    }

    public void allowChair()
    {
        allowed = true;
    }
}
