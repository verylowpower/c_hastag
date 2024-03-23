using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Health PlayerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    // Start is called before the first frame update
    private void Start()
    {
        totalhealthBar.fillAmount = PlayerHealth.currentHealth / 10;
    }

    // Update is called once per frame
    private void Update()
    {
        currenthealthBar.fillAmount = PlayerHealth.currentHealth / 10;
    }
}
