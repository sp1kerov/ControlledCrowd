using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnNewMember : MonoBehaviour
{
    [SerializeField] private PanelGameOver _panelLose;

    public GameObject Member; 
    public static List<GameObject> Members = new List<GameObject>();
    public float UnitSphereRatio = 2.5f;
    public static SpawnNewMember NewMemberSpawnCls;
    public TextMeshProUGUI MemberCount;

    private GameObject _newMember;
    private float _startYPosition;

    private void Start()
    {
        _startYPosition = transform.GetChild(1).gameObject.transform.position.y;
        Members.Add(transform.GetChild(1).gameObject);
        Debug.Log(transform.GetChild(1).gameObject.name);
    }

    public void Update()
    {
        if (Members.Count <= 0)
        {
            _panelLose.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown("a")) 
        {
            SpawnMember(10);
        }

        MemberCount.text = Members.Count.ToString();
    }

    private Vector3 SpwanPosition()
    {
        Vector3 pos = Random.insideUnitSphere * UnitSphereRatio ; 
        Vector3 spawnPos = transform.position + pos;
        spawnPos.y = _startYPosition;
        return spawnPos;
    }

    public void SpawnMember(int memberSize)
    {
        for (int i = 0; i < memberSize; i++)
        {
            _newMember = Instantiate(Member, SpwanPosition(), Quaternion.identity, transform);
            Members.Add(_newMember);
        }
    }
}
