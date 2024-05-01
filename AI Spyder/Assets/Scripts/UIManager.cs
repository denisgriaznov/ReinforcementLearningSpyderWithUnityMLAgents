using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textReward;
    [SerializeField] GameObject nameText;
    RLAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = FindObjectOfType<RLAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        textReward.text = "Reward: " + agent.GetCumulativeReward().ToString("F1");
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player!=null)
        {
            nameText.transform.position = player.transform.position;
        }
    }
}
