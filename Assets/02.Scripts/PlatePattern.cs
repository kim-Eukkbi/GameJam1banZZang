using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePattern
{
    public int[,] patterns = new int[7, 8]
    {
        // {
        //     1, 1, 1, 1, 1, 1, 1, 1
        // },
        // {
        //     1, 1, 1, 1, 1, 1, 1, 1
        // },
        // {
        //     1, 1, 1, 1, 1, 1, 1, 1
        // },
        // {
        //     1, 1, 1, 1, 1, 1, 1, 1
        // },
        // {
        //     1, 1, 1, 1, 1, 1, 1, 1
        // },
        // {
        //     1, 1, 1, 1, 1, 1, 1, 1
        // },
        // {
        //     1, 1, 1, 1, 1, 1, 1, 1
        // },
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

    public int[] rotateSpeed = new int[] {10, 20, 30, 40, 50, 60, 70};
}
