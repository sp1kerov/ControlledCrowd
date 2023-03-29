using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private PanelWin _panelWin;

    public float BossHealth;
    public static bool LockOnTarget = false;
    public static bool IsBossDead = false;
    public static Vector3 BossPosition;
    private GameObject BossHitArea;

    private float _maxHealt = 100;
    private float _currentHealth;
    private int _attaackMode;
    private bool _isDistanceLessThan15 = false;
    private bool _isDistanceLessThan3 = false;
    private Image _healthBar;
    public TextMeshProUGUI _bossHealthCount;
    public static Animator _bossAnimator;
    private GameObject _player;           

    void Start()
    {
        _currentHealth = _maxHealt / BossHealth;
        _healthBar = GameObject.FindGameObjectWithTag("healthBar").GetComponent<Image>();
        _bossAnimator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        BossHitArea = GameObject.FindGameObjectWithTag("bossHitArea");      
        BossHitArea.SetActive(false);
    }
        
    void Update()
    {        
        _bossHealthCount.text =  (BossHealth).ToString();
        _healthBar.fillAmount = (BossHealth * _currentHealth)/100;
        BossPosition = transform.position;

        if (SpawnNewMember.Members.Count > 0 || IsBossDead == false)
        {
            foreach (var stickMan in SpawnNewMember.Members)
            {
                var stickManDistance = stickMan.transform.position - transform.position;

                if (stickManDistance.sqrMagnitude < 15 * 15)
                {
                    _isDistanceLessThan15 = true;
                }

                if (stickManDistance.sqrMagnitude < 3.5 * 3.5)
                {
                    _isDistanceLessThan3 = true;
                }
            }
        }

        if (BossHealth <= 0)
        {
            Debug.Log("Boss");
            _panelWin.gameObject.SetActive(true);
        }

        LetBattle();

        if (LockOnTarget)
        {            
            BossHitArea.SetActive(true);
            _attaackMode = Random.Range(1, 4);
            _bossAnimator.SetInteger("attackMode", _attaackMode);
        }
               
       if(SpawnNewMember.Members.Count <= 0)
       {
            _bossAnimator.SetInteger("attackMode", 4);
       }
       else if(BossHealth <= 0)
       {
            _bossAnimator.SetInteger("attackMode", 5);
            LockOnTarget = false;
            IsBossDead = true;
       }
    }

    private void LetBattle()
    {
        if (_isDistanceLessThan15 )
        {
            PlayerController.Isbattle = true;
            _bossAnimator.SetBool("inBattle", true);                        
            _bossAnimator.SetInteger("attackMode", 0);
                        
            transform.position = Vector3.Lerp(transform.position, _player.transform.position + new Vector3(0, 0, 0.75f), 0.5f * Time.deltaTime);
            _player.transform.position = Vector3.Lerp(_player.transform.position, transform.position + new Vector3(0, 0, -0.75f), Time.deltaTime * 0.5f);
        }

        if (_isDistanceLessThan3)
        {
            LockOnTarget = true;
            PlayerController.Isbattle = true;

            var bossRotation = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z) - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(bossRotation, Vector3.up), 10f * Time.deltaTime);
        }
    }
}
