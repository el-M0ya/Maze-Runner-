using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Metodos
{

    public enum Tile
    {
        T_Wall,
        T_Way,
        T_Pure_Wall,
        T_Chest,
        T_Trap_1,
        T_Trap_2,
        T_Truhan,
        T_Map,
        T_Exit,
        T_Smoker,
        T_Kid_Trap,
        T_Jail,
        T_Decition,
        T_I,
        T_F,
        T_u,
    }

    // Por ahora va a estar vacio pues voy a generar uno Default. Sino se haria (int Lengh , int High , int Player)
    public static bool Is_F(int[,] array)
    {
        for(int i = 0 ; i < array.GetLength(0) ; i++)
            {
                for(int j = 0 ; j < array.GetLength(1); j++)
                {
                    if(array[i , j] == (int)Tile.T_F)
                        return true;
                } 
            }
        return false;
    }

    public static void Near(int[,] array , int x , int y)
    {
        //North
        if(x - 2 >= 0 && array[x-2 , y] == (int)Tile.T_u)
        {
            array[x - 2 , y] = (int)Tile.T_F;
        }
        // South
        if(x + 2 < array.GetLength(0) && array[x+2 , y] == (int)Tile.T_u)
        {
            array[x + 2 , y] = (int)Tile.T_F;
        }
        //Est
        if(y + 2 < array.GetLength(1) && array[x , y + 2] == (int)Tile.T_u)
        {
            array[x , y + 2] = (int)Tile.T_F;
        }
        //West
        if(y - 2 > 0  && array[x , y - 2] == (int)Tile.T_u)
        {
            array[x , y - 2] = (int)Tile.T_F;
        }
    }
    public static void F_To_I(int[,] array)
    {
        System.Random random = new System.Random();
        for(int i = 0 ; i < array.GetLength(0) ; i++)
        {
            for(int j = 0 ; j < array.GetLength(1) ; j++)
            {
                int aleatory = random.Next(1 , 3);
                if(array[i , j] == (int)Tile.T_F && aleatory == 1 )
                {
                    array[i , j] = (int)Tile.T_I;
                    // Direccion Aleatoria
                    int dir = random.Next(0 , 4);
                    while(true)
                    {
                        if(dir == 4)
                            dir = 0;
                        if( Position.Valid_Dir(array , i + 2 * Position.Dir_Ver[dir] , j + 2 * Position.Dir_Hor[dir]) && array[i + 2 * Position.Dir_Ver[dir] , j + 2 * Position.Dir_Hor[dir]] == (int)Tile.T_I  )
                        {
                        array[i +  Position.Dir_Ver[dir] , j +  Position.Dir_Hor[dir]] = (int)Tile.T_Way; 
                        break;
                        }
                        else 
                        {
                            dir++;
                        }
                    }
                    Near(array , i , j);
                   return;
                }
            }
        }
        F_To_I(array);
    }

    
    public static int[,] Generar_Array(int high , int lenght)
    {
        System.Random random = new System.Random();
        int[,] Lab = new int[high, lenght];

        // (1) Hacer un Lab con habitaciones(un camino esta rodeado de paredes) [( 1  : camino) (888 : pared)]
        for(int i = 0 ; i < Lab.GetLength(0) ; i++)
        {
            for(int j = 0 ; j < Lab.GetLength(1); j++)
            {
                if (i%2 != 0 && j%2 != 0)
                Lab[i , j] = (int)Tile.T_u;
                else
                Lab[i , j] = (int)Tile.T_Wall;
            }
        }

        // Algoritmo de construccion de laberintos de Prim´s
        // Luego de creado el Lab con habitaciones (1) 

        // (2) Seleccionar una casilla Random y convertirla a I
        int x , y;
        while(true)
        {
            x = random.Next(0 , Lab.GetLength(0));
            y = random.Next(0 , Lab.GetLength(1));
            if(Lab[x , y] == (int)Tile.T_u)
            {
                Lab[x , y] = (int)Tile.T_I;
                break;
            }
        }
        // (3) y las habitaciones aledañas convertirlas en F
        Near(Lab , x , y);

        //(4) Luego seleccionar aleatoriamente un F y convertirlo en I y abrir caminos entre I
        F_To_I(Lab);

        //(5) Repetir el proceso hasta que no hayan F
        while(Is_F(Lab))
        {
            F_To_I(Lab);
    
        }
        //(6) Convertir las I en Camino 
        for(int i = 0 ; i < Lab.GetLength(0) ; i++)
        {
            for(int j = 0 ; j < Lab.GetLength(1); j++)
            {
                if(Lab[i , j] == (int)Tile.T_I)
                {
                    Lab[i , j] = (int)Tile.T_Way;
                }
            }
        }
        // Para que tenga limites el laberinto y crear Pure_Walls
        int New_High = high;
        int New_Lenght = lenght; 

        if(high % 2 == 0)
            New_High ++;

        if(lenght % 2 == 0)
            New_Lenght ++;
            

        int[,] Board = new int[New_High , New_Lenght];
        //Copiar array
            for(int i = 0 ; i < New_High; i++)
            {
                for(int j = 0 ; j < New_Lenght; j++)
                {   
                    if(i == 0 || j == 0 || i == New_High - 1 || j == New_Lenght - 1)
                        Board[i , j] = (int)Tile.T_Pure_Wall;
                    else
                        Board[i , j] = Lab[i , j];
                }
            }
        // Poner paredes
        // Generar casillas especiales
        
        Gen_Special_Limit(Board , (int)Tile.T_Exit);
        Gen_Special(Board, (int)Tile.T_Chest, 5);
        Gen_Special(Board, (int)Tile.T_Trap_1, 3);
        Gen_Special(Board, (int)Tile.T_Trap_2, 3);

        return Board;

    }
    // Crear Players y salida
    public static void Gen_Special_Limit(int[,] Lab, int that)
    {
        int i = -1;
        int j = -1;
        int Count = 0;
        bool Is_null_i;

        System.Random random = new System.Random();
        int Position = random.Next(1, 5);
        Debug.Log(Position);
        
        // 1 ---> Izquierda
        // 2 ---> Derecha
        // 3 ---> Abajo
        // 4 ---> Arriba

        switch (Position)
        {
            case 1:
            {
                i = 1;
                Is_null_i = false;
                break;
            }
            case 2:
            {
                i = Lab.GetLength(0) - 2;
              
                Is_null_i = false;
                break;
            }
            case 3:
            {
               
                j = 1;
                Is_null_i = true;
                break;
            }
            case 4:
            {
                
                j = Lab.GetLength(1) - 2;
                Is_null_i = true;
                break;
            } 
            default:
            {
                throw new Exception("WTF , el random se paso de rango");
            }
        }
        // Contador para que si en ese borde estan ocupados todos los espacios, entonces comience de nuevo
        while(Count < 100)
        {
            if(Is_null_i)
            {
                i = random.Next(0 , Lab.GetLength(0));
            }
            else j = random.Next(0 , Lab.GetLength(1));

            Debug.Log($"{i} , {j}");
            
            if(Lab[i , j] == (int)Tile.T_Way)
            {
                if(Is_null_i)
                {
                    if(j == 1) 
                    {
                        if (Lab[i , 0] == (int)Tile.T_Pure_Wall) 
                            Lab[i , 0] = that;  
                    }
                    
                    else  if(Lab[i , j+1] == (int)Tile.T_Pure_Wall)
                             Lab[i , j+1] = that ;
                    return;
                }
                else
                {
                    if(i == 1)
                    {
                       if(Lab[0 , j] == (int)Tile.T_Pure_Wall)
                          Lab[0 , j] = that;
                    } 
                    else if(Lab[i + 1 , j] == (int)Tile.T_Pure_Wall) 
                            Lab[i + 1 , j] = that;
                    return;
                }
            }
            else Count++;
        }
        Gen_Special_Limit(Lab , that);
    }
    // Cambia un Camino por una casilla especial7
    public static void Gen_Special(int[,] Lab, int that, int cant16x16)
    {
        int cantidad = (int)Math.Sqrt(Lab.Length) / (16 / cant16x16);
        while (cantidad > 0)
        {
            int i = UnityEngine.Random.Range(1, Lab.GetLength(0) - 2);
            int j = UnityEngine.Random.Range(1, Lab.GetLength(1) - 2);
            if (Lab[i, j] == (int)Tile.T_Way)
            {
                Lab[i, j] = that;
                cantidad--;
            }

        }
        return;
    }
    public static int[,] Copy_Array(int[,] array)
    {
        int[,] new_array = new int[array.GetLength(0) , array.GetLength(1)];
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                new_array[i , j] = array[i , j];
            }
        }
        return new_array;
    }

    public static void Gen_Map(int[,] array, Tilemap tilemap, TileBase Wall_Tile, TileBase PureWall_Tile, TileBase Way_Tile, TileBase Trap_1_Tile,
     TileBase Trap_2_Tile, TileBase Exit_Tile, TileBase Jail)

    {
        // Limpiar mapa
        tilemap.ClearAllTiles();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] == (int)Tile.T_Wall)
                    tilemap.SetTile(new Vector3Int(i, j, 0), Wall_Tile);

                if (array[i, j] == (int)Tile.T_Pure_Wall)
                    tilemap.SetTile(new Vector3Int(i, j, 0), PureWall_Tile);

                if (array[i, j] == (int)Tile.T_Way || array[i, j] == (int)Tile.T_Chest)
                    tilemap.SetTile(new Vector3Int(i, j, 0), Way_Tile);

                if (array[i, j] == (int)Tile.T_Trap_1)
                    tilemap.SetTile(new Vector3Int(i, j, 0), Trap_1_Tile);

                if (array[i, j] == (int)Tile.T_Trap_2)
                    tilemap.SetTile(new Vector3Int(i, j, 0), Trap_2_Tile);

                if (array[i, j] == (int)Tile.T_Exit)
                    tilemap.SetTile(new Vector3Int(i, j, 0), Exit_Tile);
                    
                if (array[i, j] == (int)Tile.T_Jail)
                    tilemap.SetTile(new Vector3Int(i, j, 0), Jail);
            }
        }
    }

}