using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Crossy road like level generator.
/// </summary>
public class LevelGeneratorLoop : MonoBehaviour
{
    public static LevelGeneratorLoop instance;
    [SerializeField] private Transform _startingTile;
    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private uint _tilesPerChunk = 5;
    [SerializeField] private uint _nextChunkAt = 1;
    [SerializeField] private uint _destroyPreviousChunkAt = 2;

    private GameObject _lastTile;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        _lastTile = _startingTile.gameObject;
        GenerateChunk();
    }


    /// <summary>
    /// Generate a chunk of N tiles
    /// </summary>
    public void GenerateChunk()
    {
        GameObject lastTile = _lastTile;

        GameObject emptyParent = new GameObject("Chunk"); // Creating an empty gameobject where I will child the tiles.
        emptyParent.transform.SetParent(transform);

        for (int i = 0; i < _tilesPerChunk; i++)
        {
            GameObject newTile = _tiles[Random.Range(0, _tiles.Length)];

            Vector3 nextTilePosition = 
                new Vector3(lastTile.transform.position.x, lastTile.transform.position.y,
                lastTile.transform.position.z + ((newTile.transform.lossyScale.z + lastTile.transform.lossyScale.z) / 2));

            
            lastTile = Instantiate(newTile, nextTilePosition, lastTile.transform.rotation, emptyParent.transform);
        
        
            if(i == _nextChunkAt)
            {
                NextChunkTrigger(lastTile); // Adding the trigger for the Next Chunk Generation
            }

            if(i == _destroyPreviousChunkAt)
            {
                DeleteChunkTrigger(lastTile); // Adding the trigger for deleting the Previous Chunk
            }
        }

        _lastTile = lastTile;
    }


    /// <summary>
    /// Destroy Last Chunk
    /// </summary>
    public void DestroyLastChunk()
    {   
        Destroy(transform.GetChild(0).gameObject);
    }

    /// <summary>
    /// Adding the next chunk trigger script to a gameobject
    /// </summary>
    /// <param name="tile"></param>
    private void NextChunkTrigger(GameObject tile)
    {
        tile.AddComponent<NextChunkTrigger>();
    }

    /// <summary>
    /// Adding the destroy previous chunk trigger script to a gameobject
    /// </summary>
    /// <param name="tile"></param>
    private void DeleteChunkTrigger(GameObject tile)
    {
        tile.AddComponent<DestroyPreviousChunkTrigger>();
    }
}
