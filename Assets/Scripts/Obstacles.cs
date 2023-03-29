using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacles : MonoBehaviour
{
    public bool IsRightHammer = false;

    private GameObject Boss;    
    
    private void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("boss");

        if (gameObject.CompareTag("hammer"))
        {
            if (IsRightHammer)
            {
                transform.DORotate(new Vector3(0, 0, 90), 1.5f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutSine);
            }

            else if (IsRightHammer == false)
            {
                transform.DORotate(new Vector3(0, 0, -90), 1.5f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutSine);
            }
        }

        if (gameObject.CompareTag("obstacle") && gameObject.transform.parent.gameObject.CompareTag("sharp"))
        {
            transform.DOMoveY(-2.2f, Random.Range(0.2f, 2f)).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("obstacle"))
        {
            if (collision.gameObject.CompareTag("teamMember"))
            {
                collision.gameObject.tag = "Untagged";

                for (int i = 0; i < SpawnNewMember.Members.Count; i++)
                {
                    if (SpawnNewMember.Members.Count <= 0)
                    {
                        Debug.Log("LOSE MENU");
                    }

                    if (SpawnNewMember.Members[i].name == collision.gameObject.name)
                    {
                        SpawnNewMember.Members.RemoveAt(i);
                        collision.gameObject.SetActive(false);
                        break;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("parent obstacle"))
        {
            if (other.gameObject.CompareTag("teamMember"))
            {
                Order.IsNeedOrder = false;
            }
        }

        if (gameObject.CompareTag("bossHitArea"))
        {
            if (other.gameObject.CompareTag("teamMember") && Boss.GetComponent<BossBattle>().BossHealth >= 0)
            {
                other.gameObject.tag = "Untagged";

                for (int i = 0; i < SpawnNewMember.Members.Count; i++)
                {
                    if (SpawnNewMember.Members[i].name == other.gameObject.name)
                    {
                        StartCoroutine(DeathTime(i, other.gameObject));
                        break;
                    }
                }
            }

            if (Boss.GetComponent<BossBattle>().BossHealth <= 0)
            {
                other.gameObject.GetComponent<Animator>().SetBool("victory", true);

                var membersRotation2 = new Vector3(Camera.main.transform.position.x, other.gameObject.transform.position.y, Camera.main.transform.position.z) - other.gameObject.transform.position;
                other.gameObject.transform.rotation = Quaternion.Slerp(other.gameObject.transform.rotation, Quaternion.LookRotation(membersRotation2, Vector3.up), 10f * Time.deltaTime);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("parent obstacle"))
        {
            if (other.gameObject.CompareTag("teamMember"))
            {
                StartCoroutine("NeedOrder");
            }
        }
    }

    IEnumerator NeedOrder()
    {
        yield return new WaitForSeconds(1f);
        Order.IsNeedOrder = true;
    }

    IEnumerator DeathTime(int i, GameObject a)
    {
        yield return new WaitForSeconds(1.5f);
        
        if (Boss.GetComponent<BossBattle>().BossHealth > 0)
        {
            Boss.GetComponent<BossBattle>().BossHealth--;

            yield return new WaitForSeconds(0.3f);

            SpawnNewMember.Members.RemoveAt(i);

            if (SpawnNewMember.Members.Count <= 0)
            {
                Debug.Log("LOSE MENU");
            }

            a.gameObject.SetActive(false);
        }

        if (Boss.GetComponent<BossBattle>().BossHealth <= 0)
        {
            a.tag = "teamMember";   
        }
    }
}
