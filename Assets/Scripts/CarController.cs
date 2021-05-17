using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Sector mySector;
    public CarConfig myCarConfig;
    public int myLane;

    public float variableSpeed;

    private void Update()
    {
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, transform.forward, out _hit, myCarConfig.carConfig.followupDistance))
        {
            CarController carAhead = _hit.transform.GetComponent<CarController>();
            if(carAhead != null)
            {
                variableSpeed = Random.Range(carAhead.variableSpeed/1.1f, carAhead.variableSpeed);
                Debug.DrawRay(transform.position, transform.forward * _hit.distance, Color.yellow);
                Debug.DrawRay(transform.position, transform.up * 2f, Color.red);
                Debug.Log(variableSpeed);
            }
        }
        else
        {
            variableSpeed = Random.Range(myCarConfig.carConfig.minSpeed, myCarConfig.carConfig.maxSpeed);
            Debug.DrawRay(transform.position, transform.up * 2f, Color.green);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DespawnZone"))
        {
            CarSpawner.instance.DestroyCar(this);
        }
    }
}
