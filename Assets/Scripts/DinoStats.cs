using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoStats : MonoBehaviour
{
    [HideInInspector]public float health;

    public float speed; //Скорость, по которой определяеться очередь хода
    public float maxHealth; //Vfrcbvfkmyjt pljhjdmt
    public float moveSpeed; //Скорость передвижения динозавра
    public float damage;

    public Transform attackPoint; //Точка для рейкаста (что бы не кастить из центра персонажа)
    public Image healthBar;
    public float agroDistance; //Длинна вектора рейкаста

    [Tooltip("Layer mask index")]
    public int team; //Из пометки понятно

    public Text damageText;
    public Text healthText;
    public GameObject damagePanel;

    [HideInInspector] public float getDamag;

    private void Start()
    {
        health = maxHealth;

        healthText.text = health.ToString();
        damagePanel.SetActive(false);

        gameObject.layer = team;
    }

    public void GetDamage()
    {
        health -= getDamag;

        damageText.text = damage.ToString();
        healthBar.fillAmount = health / maxHealth;
        healthText.text = health.ToString();
    }
}
