using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject Pause_Menu;
    bool Is_Paused = false;

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Is_Paused) Reanudar();
            else 
            {
                if(TurnManager.Instance._currentplayer.Can_Move) Pausar();
                
            }
        } 
    }
   public void Reanudar()
    {
        TurnManager.Instance._currentplayer.stopwatch.Start();
        Pause_Menu.SetActive(false);
        Time.timeScale = 1;
        Is_Paused = false;
    }
    public void Pausar()
    {
        TurnManager.Instance._currentplayer.stopwatch.Stop();
        Pause_Menu.SetActive(true);
        Time.timeScale = 0;
        Is_Paused = true;
    }
}
