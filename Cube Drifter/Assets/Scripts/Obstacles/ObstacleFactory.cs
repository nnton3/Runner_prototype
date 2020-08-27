using UnityEngine;
using System.Collections;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePref;

    public GameObject GetDefaultObstacle(int difficult)
    {
        var obstacle = new GameObject($"obstacle_default_{difficult}");
        var cursorPos = new Vector3(-4.5f, 0f, 0f);
        int caurrentDiff = difficult;
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
                caurrentDiff = Random.Range(difficult - 1, difficult + 2);
            for (int j = 0; j < caurrentDiff; j++)
            {
                Instantiate(obstaclePref, cursorPos, Quaternion.identity, obstacle.transform);
                cursorPos += Vector3.up;
            }
            cursorPos += Vector3.right;
            cursorPos.y = 0f;
        }

        return obstacle;
    }

    public GameObject GetPerforatedObstacle(int difficult)
    {
        var obstacle = new GameObject($"obstacle_perforated_{difficult}");
        var cursorPos = new Vector3(-4.5f, 0f, 0f);

        for (int i = 0; i < difficult; i++)
        {
            if ((i + 1) % 2 == 0)
            {
                MoveToNextLine();
                continue;
            }

            for (int j = 0; j < 10; j++)
            {
                Instantiate(obstaclePref, cursorPos, Quaternion.identity, obstacle.transform);
                cursorPos += Vector3.right;
            }

            MoveToNextLine();

            void MoveToNextLine()
            {
                cursorPos += Vector3.up;
                cursorPos.x = -4.5f;
            }
        }

        return obstacle;
    }

    public GameObject GetWheelsObstacle(int difficult)
    {
        var obstacle = new GameObject($"obstacle_wheels_{difficult}");
        var cursorPos = new Vector3(-4.5f, 0f, 0f);

        for (int i = 0; i < difficult; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (j % 2 == 0)
                {
                    if ((i + 1) % 2 == 0)
                        Instantiate(obstaclePref, cursorPos, Quaternion.identity, obstacle.transform);
                }
                if ((j + 1) % 2 == 0)
                {
                    if (i % 2 == 0)
                        Instantiate(obstaclePref, cursorPos, Quaternion.identity, obstacle.transform);
                }

                cursorPos += Vector3.right;
            }
            cursorPos.x = -4.5f;
            cursorPos += Vector3.forward;
        }

        return obstacle;
    }
}
