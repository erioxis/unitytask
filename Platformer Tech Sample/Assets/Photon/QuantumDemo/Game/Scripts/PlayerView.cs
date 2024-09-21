using Quantum;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EntityView))]
public class PlayerView : MonoBehaviour
{
    private EntityView _view;

    private float fallDamageThreshold = 1.3f;
    private float fallDamage = 10f;
    private float fallTime = 0f;
    private bool isGrounded = true;
    private float groundCheckDistance = 1.1f;
    private float health = 1000f;
    private TextMeshProUGUI healthText;

    private void Start()
    {
        _view = GetComponent<EntityView>();

        Frame f = QuantumRunner.Default.Game.Frames.Verified;
        PlayerLink playerLink = f.Get<PlayerLink>(_view.EntityRef);

        if (QuantumRunner.Default.Session.IsLocalPlayer(playerLink.Player))
        {
            Camera.main.GetComponent<CameraFollow>().SetTarget(transform);
        }

        healthText = FindObjectOfType<TextMeshProUGUI>();
        healthText.text = "Здоровье: "+health.ToString();
    }

    private void Update()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance);
        if (isGrounded)
        {
            if (fallTime > fallDamageThreshold)
            {
                ApplyFallDamage();
            }

            fallTime = 0f;
        }
        else
        {
            fallTime += Time.deltaTime;
        }
    }

    private void ApplyFallDamage()
    {
        Debug.Log("Игрок получил урон от падения: " + fallDamage);
        health -= fallDamage;
        healthText.text = "Здоровье: "+health.ToString();
    }
}
