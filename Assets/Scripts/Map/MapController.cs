using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public float chunkSize = 22f;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement pm;

    [Header("Optimization")]
    GameObject latestChunk;
    List<GameObject> spawnedChunks = new List<GameObject>();
    public float maxOpDist;
    float opDist;
    float optimizerCooldown;
    public float OptCooldownDur;

    Vector3[] neighbourOffsets;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();

        neighbourOffsets = new Vector3[]
        {
            new Vector3(0, chunkSize, 0), //up
            new Vector3(0, -chunkSize, 0), //down
            new Vector3(chunkSize, 0, 0), //right
            new Vector3(-chunkSize, 0, 0), //left
            new Vector3(chunkSize, chunkSize, 0), //Right Up
            new Vector3(chunkSize, -chunkSize, 0), //Right Down
            new Vector3(-chunkSize, chunkSize, 0), //Left Up
            new Vector3(-chunkSize, -chunkSize, 0), //Left Down
        };

        if (!currentChunk)
        {
            Collider2D hit = Physics2D.OverlapPoint(player.transform.position, terrainMask);
            if (hit) currentChunk = hit.gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        UpdateCurrentChunk();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {

        if (!currentChunk)
        {
            return;
        }

        if (pm.moveDir.y != 0 || pm.moveDir.x != 0)
        {
            foreach (Vector3 offset in neighbourOffsets)
            {
                Vector3 checkposition = currentChunk.transform.position + offset;

                if (!Physics2D.OverlapCircle(checkposition, checkerRadius, terrainMask))
                {
                    SpawnChunk(checkposition);
                }
            }
        }

    }

    void SpawnChunk(Vector3 positionToSpawn)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], positionToSpawn, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void UpdateCurrentChunk()
    {
        Collider2D hit = Physics2D.OverlapPoint(player.transform.position, terrainMask);
        if (hit)
        {
            currentChunk = hit.gameObject;
        }
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if(optimizerCooldown <= 0f)
        {
            optimizerCooldown = OptCooldownDur;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
