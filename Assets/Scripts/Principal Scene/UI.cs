using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Time;
    
    [SerializeField]
    GameObject HUD;
    public GameObject[] Lifes;
    public static UI Instance{get; private set;}
    bool Is_Active = true;
    bool Count = true;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else UnityEngine.Debug.LogError("Mas de un TurnManager");
    }
    // Update is called once per frame
    void Update()
    {
        Time.text = ": " + (TurnManager.Instance._currentplayer._Time - TurnManager.Instance._currentplayer.stopwatch.ElapsedMilliseconds/1000);
        if(Input.GetKey(KeyCode.H))
        {
           if(Is_Active && Count) 
           {
            HUD.SetActive(false);
            Is_Active = false;
           }
           else if(!Is_Active && Count) 
           {
            HUD.SetActive(true);
            Is_Active = true;
           }
           Count = false;
        }
        else
            Count = true;

    }


    public void Life_Off(int index)
    {
        Lifes[index].SetActive(false);
    }
    public void Life_On(int index)
    {
        Lifes[index].SetActive(true);
    }
    public void ShowLife()
    {
        for(int i = 0 ; i < 10 ; i++)
        {
            if(i < TurnManager.Instance._currentplayer.Life)   Life_On(i);
            else Life_Off(i);
        }
    }
}
