using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("OnDeath")]
    public float backOffsetEagle = 10;
    public float backOffsetAccident = 10;
    public float cameraSpeedCentering = 2;


    private SimpleMove _movementPlayer;
    private EnemyGrabOnTrigger _grabTrigger;


    private ChaserCamera _chaserCamera;
    private GameObject _enemy;
    private GameObject _menuUI;
    private GameObject _resartButton;

    void Start()
    {
        if(Extension.Methods.Finder.TryFindGameObjectWithTag(Constants.Tags.PLAYER_REFERENCES, out GameObject go))
        {
            if (go.TryGetComponent<PlayerReferences>(out PlayerReferences pf))
            {
                _chaserCamera = pf.cam;
                _enemy = pf.enemy;
                _menuUI = pf.menuUI;
                _resartButton = pf.menuUI.transform.Find("Restart").gameObject;
            }
        }


        _movementPlayer = GetComponent<SimpleMove>(); // To change with its real movement

        if (_enemy.TryGetComponent<EnemyGrabOnTrigger>(out EnemyGrabOnTrigger grab))
        {
            _grabTrigger = grab;
        }
    }


    public void DeathByEagle()
    {
        Death();
        _chaserCamera.StartCameraLerp(cameraSpeedCentering, backOffsetEagle, _grabTrigger);
        _enemy.SetActive(true);
        //Setting the X of the Grabber (The Eagle) equals to the player's one so the grabber fly above him.
        _enemy.transform.position = new Vector3(transform.position.x, _enemy.transform.position.y, _enemy.transform.position.z);
    }

    public void DeathByAccident()
    {
        Death();
        _chaserCamera.StartCameraLerp(cameraSpeedCentering, backOffsetAccident);
    }

    private void Death()
    {
        _menuUI.SetActive(true);
        _resartButton.SetActive(true);
        _movementPlayer.enabled = false;
        ScoreManager.instance.SaveTopScore();
    }
}
