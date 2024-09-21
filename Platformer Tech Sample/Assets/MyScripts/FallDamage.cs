using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public float fallDamageThreshold = 5f; // Высота, с которой начинается урон
    public float damageMultiplier = 1f; // Множитель урона от падения
    private float previousY;

    private void Start()
    {
        previousY = transform.position.y;
    }

    private void Update()
    {
        float currentY = transform.position.y;
        if (currentY < previousY) // Если персонаж падает
        {
            float fallDistance = previousY - currentY;
            if (fallDistance > fallDamageThreshold)
            {
                float damage = (fallDistance - fallDamageThreshold) * damageMultiplier;
                ApplyFallDamage(damage);
            }
        }

        previousY = currentY;
    }

    private void ApplyFallDamage(float damage)
    {
        // Здесь добавьте логику нанесения урона
        Debug.Log($"Fall damage applied: {damage}");
        // Например, уменьшите здоровье персонажа
        // playerHealth.TakeDamage(damage);
    }
}
