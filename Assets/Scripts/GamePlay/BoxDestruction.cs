using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class BoxDestruction : MonoBehaviour
{
    [SerializeField] private GameObject[] breakPieces;
    private List<GameObject> spawnedPieces = new List<GameObject>();
    private bool isBeingCleanedUp = false;

    public void CleanupPieces()
    {
        isBeingCleanedUp = true;
        Debug.Log("Cleaning up spawned pieces");

        foreach (GameObject piece in spawnedPieces)
        {
            if (piece != null)
                Destroy(piece);
        }

        spawnedPieces.Clear();
    }

    private void OnDestroy()
    {
        if (isBeingCleanedUp) return;

        Debug.Log("Spawning pieces");
        foreach (GameObject piecePrefab in breakPieces)
        {
            GameObject piece = Instantiate(piecePrefab, transform.position, Quaternion.identity);
            spawnedPieces.Add(piece);

            Vector2 explosionDir = (piece.transform.position - transform.position).normalized;
            float randomTorque = Random.Range(-50f, 50f);

            piece.GetComponent<BreakPiece>().Initialize(explosionDir * 10f, randomTorque);
        }
    }
}