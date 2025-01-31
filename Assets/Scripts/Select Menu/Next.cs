using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour
{
    public GameObject UI_1;
    public GameObject UI_2;

    public void Next_Panel()
    {
        UI_1.SetActive(false);
        UI_2.SetActive(true);
    }
}
