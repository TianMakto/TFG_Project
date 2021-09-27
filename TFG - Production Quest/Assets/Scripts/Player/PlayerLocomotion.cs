using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed;

    [System.NonSerialized]
    public bool OnHole;

    SpriteRenderer m_cmpSpriteRenderer;
    private Life m_life;
    private UIManager ui;
    private InputMaster m_inputs;

    private void Start()
    {
        ui = UIManager.Instance;
        m_life = GetComponent<Life>();
        m_cmpSpriteRenderer = GetComponent<SpriteRenderer>();
        m_inputs = GetComponent<PlayerCombat>().Inputs;
    }

    private void Update()
    {
        if (!m_life.isDead && !OnHole && !ui.OnMenus)
        {
            float Xspeed = m_inputs.Player.Movement.ReadValue<Vector2>().x;
            float Yspeed = m_inputs.Player.Movement.ReadValue<Vector2>().y;

            transform.position += (Vector3.right * Xspeed + Vector3.up * Yspeed) * Time.deltaTime * m_movementSpeed;

            GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(Xspeed + Yspeed));

            if (Mouse.current.position.ReadValue().x <= Screen.width / 2)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
