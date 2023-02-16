using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAnimatorController), typeof(PlayerAudioManager))]
public class PlayerStatus : MonoBehaviour
{
    [Header("OnDeath")]
    public float backOffsetEagle = 10;
    public float backOffsetAccident = 10;
    public float cameraSpeedCentering = 2;


    private PlayerMovement _movementPlayer;
    private PlayerAnimatorController _anim;
    private PlayerAudioManager _playerAudio;
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

        if (_enemy.TryGetComponent<EnemyGrabOnTrigger>(out EnemyGrabOnTrigger grab))
        {
            _grabTrigger = grab;
        }

        _anim = GetComponent<PlayerAnimatorController>();
        _movementPlayer = GetComponent<PlayerMovement>();
        _playerAudio = GetComponent<PlayerAudioManager>();
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
        _anim.Die();
        _chaserCamera.StartCameraLerp(cameraSpeedCentering, backOffsetAccident);
    }

    private void Death()
    {
        _playerAudio.PlayDeath();
        _menuUI.SetActive(true);
        _resartButton.SetActive(true);
        _movementPlayer.enabled = false;
        ScoreManager.instance.SaveTopScore();
    }
}
