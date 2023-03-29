using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Battle : MonoBehaviour
{
    private int _battleDistance;
    private bool _letThemBattle = false;
    private GameObject _player;
    private GameObject _enemy;
    private Animator _soldierAnimator;
    private List<GameObject> _enemies;
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");        
        _enemy = gameObject.transform.parent.gameObject;
        _soldierAnimator = GetComponent<Animator>();
        _enemies = _enemy.gameObject.GetComponent<Enemy>().Enemies;
    }

    private void Update()
    {  
        if(SpawnNewMember.Members.Count <= 0)
        {
            StartCoroutine(StopRun());
        }

        if(SpawnNewMember.Members.Count < 30) 
        {
            _battleDistance = 8;
        }

        if (SpawnNewMember.Members.Count >= 30 && SpawnNewMember.Members.Count < 70)
        {
            _battleDistance = 11;
        }

        if (SpawnNewMember.Members.Count >= 70)
        {
            _battleDistance = 13;
        }

        for (int i =0; i<SpawnNewMember.Members.Count; i++)
        {
            for (int j = 0; j < _enemies.Count; j++) 
            {
                if(Vector3.Magnitude(SpawnNewMember.Members[i].transform.position - _enemies[j].transform.position) < _battleDistance)
                {                           
                    if (SpawnNewMember.Members.Count > 0 && _enemies.Count > 0) 
                    {
                        _letThemBattle = true;                        
                    }
                }
            }
        }

        LetBattle();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("soldier"))
        {
            if (collision.gameObject.CompareTag("teamMember"))
            {
                this.gameObject.tag = "Untagged";
                collision.gameObject.tag = "Untagged";

                for (int i = 0; i < SpawnNewMember.Members.Count; i++)
                {
                    if (SpawnNewMember.Members.Count <= 0)
                    {
                        Debug.Log("LOSE MENU");
                    }

                    if (SpawnNewMember.Members.ElementAt(i).name == collision.gameObject.name)
                    {
                        SpawnNewMember.Members.RemoveAt(i);
                        collision.gameObject.SetActive(false);
                        break;
                    }
                }


                for (int i = 0; 0 < _enemies.Count; i++)
                {
                    if (_enemies.ElementAt(i).name == gameObject.name)
                    {
                        if (_enemies.Count <= 1)
                        {
                            PlayerController.Isbattle = false;
                            Order.IsNeedOrder = true;
                        }

                        _enemies.RemoveAt(i);
                        gameObject.SetActive(false);
                        break;
                    }
                }
            }
        }
    }

    private IEnumerator StopRun()
    {
        _soldierAnimator.SetBool("run", false);
        yield return new WaitForSeconds(0.7f);
    }

    private void LetBattle()
    {
        if (_letThemBattle)
        {
            PlayerController.Isbattle = true;            
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, _enemy.transform.position, Time.deltaTime * 0.65f );
            _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _player.transform.position, Time.deltaTime * 0.5f );

            foreach (var enemy in _enemies)
            {
                enemy.GetComponent<Animator>().SetBool("run", true);
            }

            if(SpawnNewMember.Members.Count <= 0 || _enemies.Count <= 0)
            {
                _letThemBattle = false;
            }
        }
    }
}
