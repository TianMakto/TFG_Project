using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fountain : InteractableFather
{

    [SerializeField]
    private float amountToGain;

    [SerializeField]
    private SpriteRenderer m_basinRenderer;

    [SerializeField]
    private SpriteRenderer m_midRenderer;

    [SerializeField]
    private Sprite m_basinEmpty;

    [SerializeField]
    private Sprite m_midEmpty;

    protected override void Start()
    {
        base.Start();
        m_text.GetComponent<TextMeshProUGUI>().text = "Recover " + amountToGain + "HP";
    }

    public override void Interact(PlayerInteract player)
    {
        base.Interact(player);
        player.GetComponent<Life>().Heal(amountToGain);


        m_basinRenderer.GetComponent<Animator>().enabled = false;
        m_midRenderer.GetComponent<Animator>().enabled = false;

        m_basinRenderer.sprite = m_basinEmpty;
        m_midRenderer.sprite = m_midEmpty;
    }
}
