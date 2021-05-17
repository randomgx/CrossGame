using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public static CarSpawner instance;

    public CarConfig[] carsTypes;
    public Sector[] sectors;
    public Transform carsParent;

    public bool spawningWaves;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        GameObject[] _tempSectorObj = GameObject.FindGameObjectsWithTag("Sector");
        sectors = new Sector[_tempSectorObj.Length];

        for(int i = 0; i < _tempSectorObj.Length; i++)
        {
            sectors[i] = _tempSectorObj[i].GetComponent<Sector>();
        }

        foreach(Sector sector in sectors)
        {
            StartCoroutine(SpawnWave(sector));
        }
    }

    private void FixedUpdate()
    {
        foreach(CarConfig type in carsTypes)
        {
            foreach(CarController car in type.cars)
            {
                car.transform.Translate(car.transform.forward * car.variableSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    public void SpawnCar(CarConfig _type, Sector _sector)
    {
        if(_sector.carsInSector >= _sector.maxCarsInSector || _type.cars.Count >= _type.maxCars) //later change to specific max type of car in sector
        {
            return;
        }

        int _lane = Random.Range(0, 2);
        GameObject c = Instantiate(_type.carPrefab, _sector.lanes[_lane].carSpawnPoint.position, _sector.lanes[_lane].carSpawnPoint.rotation);
        c.GetComponent<CarController>().variableSpeed = Random.Range(_type.carConfig.minSpeed, _type.carConfig.maxSpeed + 1) * _sector.sectorSpeedMultiplier;
        c.transform.parent = carsParent;
        c.name = _type.carConfig.prefabName;
        c.GetComponent<CarController>().mySector = _sector;
        c.GetComponent<CarController>().myLane = _lane;
        c.GetComponent<CarController>().myCarConfig = _type;
        _sector.carsInSector++;
        _type.cars.Add(c.GetComponent<CarController>());
    }

    public void DestroyCar(CarController obj)
    {
        obj.mySector.carsInSector--;
        obj.myCarConfig.cars.Remove(obj.GetComponent<CarController>());
        Destroy(obj.gameObject);
    }

    IEnumerator SpawnWave(Sector sector)
    {
        while (spawningWaves)
        {
            SpawnCar(carsTypes[Random.Range(0, carsTypes.Length)], sector);
            //foreach (CarConfig type in carsTypes)
            //{
            //    SpawnCar(type, sector);
            //}

            yield return new WaitForSeconds(sector.sectorSpawnRate);
        }
    }
}

[System.Serializable]
public class CarConfig
{
    public CarScriptableObject carConfig;
    public GameObject carPrefab;
    //public float spawnRate;
    public int maxCars;

    public List<CarController> cars = new List<CarController>();
}

