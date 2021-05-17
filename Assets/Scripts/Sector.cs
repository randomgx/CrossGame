using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public int sectorId;
    public float sectorSpawnRate;
    public SectorLane[] lanes;
    public Transform[] playerRespawnPoints;

    public int carsInSector;
    public int maxCarsInSector;

    public float sectorSpeedMultiplier;

    private void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(16, 15, 90));
    }
}

[System.Serializable]
public class SectorLane
{
    //Lanes variable for later coding. ie: distance between cars, cars restrictions per lane...
    //Right now car restrictions are only applied by quantity for each sector, not by individual lanes. Will change that later with the new Sector variables
    public Transform carSpawnPoint;
}
