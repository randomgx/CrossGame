using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "Game Instances/Car", order = 1)]
public class CarScriptableObject : ScriptableObject
{
    public string prefabName;
    public float minSpeed;
    public float maxSpeed;

    public float followupDistance = 10;
}