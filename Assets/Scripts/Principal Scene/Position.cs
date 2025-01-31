using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position 
{
    public int Pos_x;
    public int Pos_y;
    public Position(int Pos_x , int Pos_y)
    {
        this.Pos_x = Pos_x;
        this.Pos_y = Pos_y;
    }
    public static Position operator +(Position a , Position b)
    {
        Position Sum = new Position(a.Pos_x + b.Pos_x , a.Pos_y + b.Pos_y);
        return Sum;
    }
    public static Position operator * (Position a , int b)
    {
        Position Product = new Position(a.Pos_x * b , a.Pos_y * b);
        return Product;
    }
    public enum D
    {
        North ,
        South ,
        Est ,
        West ,
    }
    //                               N  S  E   W    
    public static int[] Dir_Ver = { -1, 1, 0,  0 };
    public static int[] Dir_Hor = {  0, 0, 1, -1 };
    public static bool Valid_Dir(int[,] array, int x, int y)
    {
        return x >= 0 && y >= 0 && x < array.GetLength(0) && y < array.GetLength(1);
    }
    /// <summary>
    /// Devuelve el elemento en la direccion dada
    /// </summary>
    /// <param name="Lab">Array donde se va a trabajar</param>
    /// <param name="i">Parametro con el indice i</param>
    /// <param name="j">Parametro con el indice j</param>
    /// <param name="direction">Direccion hacia donde se va a comprobar</param>
    /// <param name="scalar">Cantidad de pasos hacia donde se va a comprobar</param>
    /// <returns></returns>
    public static int Get_Dir(int[,] Lab, int i, int j, D direccion, int scalar)
    {
        if (Valid_Dir(Lab, i + Dir_Ver[(int)direccion], j + Dir_Hor[(int)direccion]))
            return Lab[i + scalar * Dir_Ver[(int)direccion], j + scalar * Dir_Hor[(int)direccion]];
        else
            return 0;
    }
    
}
