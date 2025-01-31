using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Start_Turn : MonoBehaviour
{
    public GameObject Pause_Start;
    public TextMeshProUGUI text;
    Stopwatch startwatch = new Stopwatch();

    // Update is called once per frame
    void Update()
    {
        // Hacer que el letrero sea intermitente
        if ((startwatch.ElapsedMilliseconds / 100) % 5 == 0 && (startwatch.ElapsedMilliseconds / 100) % 10 != 0)
        {
            text.text = "";
        }
        if ((startwatch.ElapsedMilliseconds / 100) % 10 == 0)
        {
            text.text = "Press Space to Start";
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reanud();
        }
    }
    public void Reanud()
    {
        Pause_Start.SetActive(false);
        Time.timeScale = 1;
        startwatch.Restart();
        TurnManager.Instance._currentplayer.Can_Move = true;
        TurnManager.Instance._currentplayer.stopwatch.Start();

    }

    public void Pause()
    {
        startwatch.Start();
        Pause_Start.SetActive(true);
        Time.timeScale = 0;

    }

}
