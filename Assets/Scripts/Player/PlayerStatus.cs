using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public ChaserCamera chaserCamera;
    public GameObject eagle;
    public GameObject restartUI;

    private SimpleMove _movementPlayer;
    private GrabOnTrigger _grabTrigger;


    void Start()
    {
        _movementPlayer = GetComponent<SimpleMove>();

        if (eagle.TryGetComponent<GrabOnTrigger>(out GrabOnTrigger grab))
        {
            _grabTrigger = grab;
        }
    }


    public void DeathByEagle()
    {
        Death();
        eagle.SetActive(true);
        //Setting the X of the Grabber (The Eagle) equals to the player's one so the grabber fly above him.
        eagle.transform.position = new Vector3(transform.position.x, eagle.transform.position.y, eagle.transform.position.z);
    }


    public void Death()
    {
        restartUI.SetActive(true);
        _movementPlayer.enabled = false;
        ScoreManager.instance.SaveTopScore();
        chaserCamera.CameraDeath();
    }
}
