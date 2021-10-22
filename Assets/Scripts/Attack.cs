using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    //[SerializeField] private float damage;    прописал в DinoStats
    [SerializeField] private LayerMask checkHit; //Слой врага
    [SerializeField] private Text damageText;
    [SerializeField] private Text healthText;
    [SerializeField] private GameObject damagePanel;
    
    private GameObject[] dinoArr;
    private GameObject[] enemyArr;
    private GameObject goal;
    private DinoStats goalStats;
    private DinoStats dinoStats;

    private GameObject enemy;
    
    private bool move;
    private bool hitGoal;



    private bool isAttacking;

    private Vector3 startPosition;
    private Vector3 pos;
    private Vector3 finalPos;

    private Animator anim;

    void Start()
    {
        healthText.text = GetComponent<DinoStats>().health.ToString();
        damagePanel.SetActive(false);
        isAttacking = false;
        anim = GetComponent<Animator>();
        dinoArr = GameObject.FindGameObjectsWithTag("Dino");
        enemyArr = new GameObject[dinoArr.Length / 2];
        dinoStats = GetComponent<DinoStats>();
        startPosition = transform.position;
        pos = transform.rotation.eulerAngles;
    }

    //Большой апдейт
    private void Update()
    {
        //Рейкаст для определения приближённости к врагу
        hitGoal = Physics.Raycast(dinoStats.attackPoint.position , Vector3.right, dinoStats.agroDistance, checkHit);

        //Движение к врагу
        if (move && !hitGoal)
        {
            isAttacking = true;

            anim.SetBool("isRun", true);            
            finalPos = new Vector3(goal.transform.position.x, transform.position.y, goal.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, finalPos, dinoStats.moveSpeed * Time.deltaTime);
            transform.LookAt(new Vector3(finalPos.x, transform.position.y, finalPos.z));
            
        }
        
        //Движение от врага после проведения атаки
        if (!move)
        {
            isAttacking = false;
            anim.SetBool("isAttack", false);
            transform.position = Vector3.MoveTowards(transform.position, startPosition, dinoStats.moveSpeed * Time.deltaTime);

            transform.LookAt(new Vector3(startPosition.x, transform.position.y, startPosition.z));
            
            //Возвращение параметра поворота в исходное значение
            if (transform.position == startPosition)
            {
                isAttacking = false;                

                anim.SetBool("isRun", false);
                transform.rotation = Quaternion.Euler(pos.x, pos.y, pos.z);
                
            }
        }

        

        
    }

    public void OnTriggerEnter(Collider other)
    {
        anim.SetBool("isRun", false);
        anim.SetBool("isAttack", true);
        
        if (!isAttacking)
        {
            damagePanel.SetActive(true);
            damageText.text = other.gameObject.GetComponent<DinoStats>().damage.ToString();
            dinoStats.healthBar.fillAmount -= other.gameObject.GetComponent<DinoStats>().damage / 100;
            healthText.text = (int.Parse(healthText.text) - other.gameObject.GetComponent<DinoStats>().damage).ToString();
        }


    }
    public void OnTriggerExit(Collider other)
    {
        damagePanel.SetActive(false);
    }


    //Начало атаки
    public void AttackStart()
    {
        GoalSelection();
        
        move = true;
    }

    //Конец атаки
    public void AttackEnd(AttackPriority attackPriority)
    {
        
        attackPriority.ReallocationPriority();
        
        move = false;
    }

    //Выбор цели из врагов другой команды с наименьшим здоровьем
    public void GoalSelection()
    {
        int z = 0;
        //Вычисляем кто свой, кто чужой
        for (int i = 0; i < dinoArr.Length; i++)
        {
            
            if (dinoArr[i].layer != gameObject.layer)
            {
                enemyArr[z] = dinoArr[i];
                z++;
            }
        }

        //Выбираем врага с наименьшим здоровьем
        for (int i = 0; i < enemyArr.Length; i++)
        {
            for (int j = 0; j < enemyArr.Length; j++)
            {
                if (dinoArr[i].GetComponent<DinoStats>().health < dinoArr[j].GetComponent<DinoStats>().health)
                {
                    GameObject tamp = enemyArr[i];
                    enemyArr[i] = enemyArr[j];
                    enemyArr[j] = tamp;
                }
            }
        }
        goal = enemyArr[0];
        goalStats = goal.GetComponent<DinoStats>();
    }
}
