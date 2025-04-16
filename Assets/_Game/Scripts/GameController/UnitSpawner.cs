using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public Unit Spawn(Unit unitPrefab, Transform location)
    {
        return Instantiate(unitPrefab, location.position, location.rotation);
    }
}