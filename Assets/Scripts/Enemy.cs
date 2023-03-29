using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int EnemyNumbers;
    public float UnitSphereRatio = 2.5f;
    public GameObject EnemySoldier;
    public List<GameObject> Enemies = new List<GameObject>();
    public TextMeshProUGUI EnemyCount;  
    
    private GameObject _newEnemy;
    private float _startYPosition;

    private void Start()
    {
        _startYPosition = transform.GetChild(0).gameObject.transform.position.y;
        Enemies.Add(transform.GetChild(0).gameObject);
        SpawnMember((EnemyNumbers-1));
    }

    private void Update()
    {
        EnemyCount.text = Enemies.Count.ToString();

        if(Enemies.Count <= 0 && EnemyCount.enabled == true)
        {
            EnemyCount.enabled = false;
        }
    }

    private Vector3 SpwanPosition()
    {
        Vector3 pos = Random.insideUnitSphere * UnitSphereRatio;
        Vector3 spawnPos = transform.position + pos;
        spawnPos.y = _startYPosition;
        return spawnPos;
    }

    private void SpawnMember(int memberSize)
    {
        for (int i = 0; i < memberSize; i++)
        {
            _newEnemy = Instantiate(EnemySoldier, SpwanPosition(), Quaternion.identity, transform);
            _newEnemy.transform.localRotation = Quaternion.Euler(0, 180, 0);
            Enemies.Add(_newEnemy);
        }
    }
}
