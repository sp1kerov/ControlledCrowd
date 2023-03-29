using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private PanelGameOver _paneGameOver;

    public static bool IsNeedOrder = true;

    private Animator _memberAnimator;
    private GameObject _enemy;
    private List<GameObject> _enemies;

    public void Start()
    {
        if (gameObject.CompareTag("soldier"))
        {
            _enemy = gameObject.transform.parent.gameObject;
            _enemies = _enemy.gameObject.GetComponent<Enemy>().Enemies;
        }

        if (gameObject.CompareTag("teamMember"))
        {
            _memberAnimator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (gameObject.CompareTag("soldier"))
        {
            if (_enemies.Count < 3 && SpawnNewMember.Members.Count > 10)
            {
                gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, -0.8f), Time.deltaTime * 5f);
            }
            else if (_enemies.Count < 3 && SpawnNewMember.Members.Count > 5)
            {
                gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, -0.5f), Time.deltaTime * 5f);
            }
            else
            {
                gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * 1f);
            }
        }

        if (IsNeedOrder == true)
        {
            if (gameObject.CompareTag("teamMember"))
            {
                if (BossBattle.IsBossDead == false)
                {                    
                    if(SpawnNewMember.Members.Count < 5 )
                    {
                        gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * (1f + 2.5f / (SpawnNewMember.Members.Count)));
                    }
                    else
                    {
                        gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * (0.5f + 1.8f / (SpawnNewMember.Members.Count)));
                    }
                }

                if (BossBattle.LockOnTarget)
                {
                    _memberAnimator.SetBool("inBattle", true);

                    var membersRotation = new Vector3(BossBattle.BossPosition.x, gameObject.transform.position.y, BossBattle.BossPosition.z) - gameObject.transform.position;
                    gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(membersRotation, Vector3.up), 10f * Time.deltaTime);
                }

                if (BossBattle.IsBossDead)
                {
                    StartCoroutine(WinPose());                    
                }
            }
        }
    }

    IEnumerator WinPose()
    {
        yield return new WaitForSeconds(1.5f);

        _memberAnimator.SetBool("victory", true);

        var membersRotation2 = new Vector3(Camera.main.transform.position.x, gameObject.transform.position.y, Camera.main.transform.position.z) - gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(membersRotation2, Vector3.up), 10f * Time.deltaTime);
    }
}
