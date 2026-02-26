using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class BlockSetup : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;

    [SerializeField] private Transform[] rows;
    [SerializeField] private int blocksPerRow;
    [SerializeField] private float offsetX = 1.4f;
    //[SerializeField] private float offsetY = 0.5f;

    private void Start()
    {
        SpawnBlocks();
    }

    private void SpawnBlocks()
    {
        foreach (Transform row in rows)
        {
            if (row != null)
            {
                for (int i = 0; i < blocksPerRow; i++)
                {
                    float offset1;
                    float offset2;
                    Vector2 spawnPos;

                    if (blocksPerRow % 2 == 0)
                    {
                        offset1 = ((row.position.x + offsetX / 2) + (offsetX * i));
                        offset2 = (blocksPerRow / 2 * offsetX);
                    }
                    else
                    {
                        offset1 = (row.position.x + (offsetX * i));
                        offset2 = (blocksPerRow / 2 * offsetX);
                    }

                    spawnPos = new Vector2(offset1 - offset2, row.position.y);

                    Instantiate(blockPrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (rows != null)
        {
            Gizmos.color = Color.red;
            foreach (Transform row in rows)
            {
                if (row != null)
                {
                    for (int i = 0; i < blocksPerRow; i++)
                    {
                        float offset1;
                        float offset2;
                        Vector2 spawnPos;

                        if(blocksPerRow % 2 == 0)
                        {
                            offset1 = ((row.position.x + offsetX/2) + (offsetX * i));
                            offset2 = (blocksPerRow / 2 * offsetX);
                            //Debug.Log($"Spawning block at: {offset1} minus {offset2} equals {offset1 - offset2}");
                        }
                        else
                        {
                            offset1 = (row.position.x + (offsetX * i));
                            offset2 = (blocksPerRow / 2 * offsetX);
                            //Debug.Log($"Spawning block at: {offset1} minus {offset2}");
                        }

                        spawnPos = new Vector2(offset1 - offset2, row.position.y);

                        Gizmos.DrawWireCube(spawnPos, blockPrefab.GetComponent<SpriteRenderer>().size * blockPrefab.transform.localScale);

                    }
                }
            }
        }
    }
}

