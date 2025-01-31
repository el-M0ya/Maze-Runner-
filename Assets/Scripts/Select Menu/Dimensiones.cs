using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimensiones : MonoBehaviour
{
    public  Select selectX = new Select();
    public Select selectY;

   public static int X;
   public static int Y;

   public void Convert()
   {
    X = selectX.number;
    Y = selectY.number;
   }
}
