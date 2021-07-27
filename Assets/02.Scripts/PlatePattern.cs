using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePattern
{
    public int[,] patterns = new int[7, 8]
    {
      /*  {
            1, 1, 1, 1, 1, 1, 1, 1
        },
        {
            1, 1, 1, 1, 1, 1, 1, 1
        },
        {
            1, 1, 1, 1, 1, 1, 1, 1
        },
        {
            1, 1, 1, 1, 1, 1, 1, 1
        },
        {
            1, 1, 1, 1, 1, 1, 1, 1
        },
        {
            1, 1, 1, 1, 1, 1, 1, 1
        },
        {
            1, 1, 1, 1, 1, 1, 1, 1
        }*/
        {
            1, 1, 0, 0, 1, 1, 0, 0
        },
        {
            1, 0, 1, 0, 1, 0, 1, 0
        },
        {
            1, 1, 1, 1, 0, 0, 0, 0
        },
        {
            0, 0, 0, 0, 0, 0, 1, 1
        },
        {
            1, 1, 0, 0, 0, 1, 1, 1
        },
        {
            0, 0, 0, 0, 0, 1, 1, 1
        },
        {
            1, 1, 1, 1, 1, 1, 1, 1
        }
    };

    public int[] rotatePattern = new int[] { 0, 1, 1, 0, 1, 0, 0 };

    public int[] rotateSpeed = new int[] {40, 30, 50, 35, 40, 35, 50};
}
