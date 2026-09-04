using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image heart4;
    public Image heart5;

    void Update()
    {
        heart1.enabled = playerHealth.currentHealth >= 1;
        heart2.enabled = playerHealth.currentHealth >= 2;
        heart3.enabled = playerHealth.currentHealth >= 3;
        heart4.enabled = playerHealth.currentHealth >= 4;
        heart5.enabled = playerHealth.currentHealth >= 5;
    }
}