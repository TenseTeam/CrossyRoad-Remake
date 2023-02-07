using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GenerateTiles();

        //Debug.Log(_startingTile.)
    }


    public void GenerateTiles()
    {
        GameObject lastTile = _lastTile;

        GameObject emptyParent = new GameObject("Chunk");
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
                NextChunkTrigger(lastTile);
            }

            if(i == _destroyPreviousChunkAt)
            {
                DeleteChunkTrigger(lastTile);
            }
        }

        _lastTile = lastTile;
    }

    public void DestroyLastChunk()
    {
        //Transform[] childs = GetComponentsInChildren<Transform>();

        /*for (int k = 0; k < 10; k++)
        {
            Destroy(transform.GetChild(k).gameObject);
        }*/


        
        Destroy(transform.GetChild(0).gameObject);

        AddCollider(transform.GetChild(1).GetChild((int)_nextChunkAt).gameObject, false);
    }


    private void NextChunkTrigger(GameObject tile)
    {
        tile.AddComponent<NextChunkTrigger>();
        AddCollider(tile, true);

    }

    private void DeleteChunkTrigger(GameObject tile)
    {
        tile.AddComponent<DestroyPreviousChunkTrigger>();
        AddCollider(tile, true);
    }

    private void AddCollider(GameObject toAttach, bool isTrigger)
    {
        BoxCollider triggerChunk = toAttach.AddComponent<BoxCollider>();
        triggerChunk.isTrigger = isTrigger;
        triggerChunk.size = new Vector3(triggerChunk.size.x, triggerChunk.size.y * 100f, triggerChunk.size.z);
    }


}
