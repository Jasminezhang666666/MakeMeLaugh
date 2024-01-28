using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Point
{
    private static int playerPoint = 0;
    private static int enemyBaseLife = 1000;
    private static float scale = 1f;

    public static int GetPlayerPoint()
    {
        return playerPoint;
    }

    public static float GetScale()
    {
        return scale;
    }
    private static void ChangePlayerPoint(int point)
    {
        int newPoint = (int)Mathf.Round(point * scale);
        playerPoint += newPoint;
    }

    public static void DecreaseEnemyLife(int damage)
    {
        enemyBaseLife -= damage;
        if(enemyBaseLife <= 0)
        {
            scale = 1.5f;
        }

        ChangePlayerPoint(damage);
    }
}
