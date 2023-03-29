using UnityEngine;
using TMPro;

public class Spawners : MonoBehaviour
{
    public enum MultipleOrAddiable
    {
        Multiple,
        Addiable
    }

    public MultipleOrAddiable NewMembers;
    public int NewSpawnSize;
    public GameObject OtherSpawner;
    public TextMeshProUGUI UISpawn;

    private SpawnNewMember _newMemberSpawn;
    private bool _isGateActive = true;
    private Spawners _spawners;
    private bool _avoidSpawnBug =false; 

    private void Start()
    {
        _newMemberSpawn = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<SpawnNewMember>();
        _spawners = OtherSpawner.gameObject.GetComponent<Spawners>();

        switch (NewMembers)
        {
            case MultipleOrAddiable.Addiable:
                UISpawn.text = "+" + NewSpawnSize.ToString();
                break;
            case MultipleOrAddiable.Multiple:
                UISpawn.text = "x" + NewSpawnSize.ToString();
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("teamMember") && _isGateActive  && gameObject.CompareTag("spawner"))
        {
            _spawners.enabled = false;
            OtherSpawner.tag = "Untagged";
            Gate();

            if (_avoidSpawnBug == true)
            {
                switch (NewMembers)
                {
                    case MultipleOrAddiable.Multiple:
                        _newMemberSpawn.SpawnMember(SpawnNewMember.Members.Count * (NewSpawnSize - 1));
                        break;

                    case MultipleOrAddiable.Addiable:
                        _newMemberSpawn.SpawnMember(NewSpawnSize);
                        break;
                }
            }            
        }     
    }

    private void Gate()
    {
        _isGateActive = false;
        _avoidSpawnBug = true;        
    }
}
