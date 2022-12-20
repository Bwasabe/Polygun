using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionCtrl))]
public class ShopCtrl : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _welcomeAudios;

    [SerializeField]
    private List<string> _welcomeText;

    [SerializeField]
    private LayerMask _playerLayer;


    // TODO: transform.find로 상점주인 애니메이터 가져오기
    private Animator _animator;
    private CollisionCtrl _collisionCtrl;

    private Player _player;

    
    private void Awake() {
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }

    private void Start() {
        _player = GameManager.Instance.Player;
        _collisionCtrl.ColliderEnterEvent += EnterPlayer;
        _collisionCtrl.ColliderExitEvent += ExitPlayer;
    }

    private void EnterPlayer(Collider collider)
    {
        if( ((1 <<collider.gameObject.layer) & _playerLayer) > 0)
        {
            Debug.Log("상점에 플레이어가 들어옴");
            SoundManager.Instance.Play(AudioType.Voice, _welcomeAudios[Random.Range(0, _welcomeAudios.Count)]);
            // TODO: 웰컴 텍스트 띄어주기
            _player.GetPlayerComponent<PlayerEquipmentCtrl>().ShopEquipmentCallback += SpawnEquipment;
        }
    }

    private void ExitPlayer(Collider collider)
    {
        if( ((1 <<collider.gameObject.layer) & _playerLayer) > 0)
        {
            Debug.Log("상점에 플레이어가 나감");
            // TODO : 안녕히가세요
            //SoundManager.Instance.Play(AudioType.Voice, _welcomeAudios[Random.Range(0, _welcomeAudios.Count)]);
            // TODO: 웰컴 텍스트 띄어주기
            _player.GetPlayerComponent<PlayerEquipmentCtrl>().ShopEquipmentCallback -= SpawnEquipment;
        }
    }

    private void SpawnEquipment(BaseEquipment equipment)
    {
        Transform parent = equipment.transform.parent;
        // TODO : 풀링
        BaseEquipment g = GameObject.Instantiate(equipment, Vector3.zero, Quaternion.identity, parent);
        // TODO: 감사합니다
        SoundManager.Instance.Play(AudioType.Voice, _welcomeAudios[Random.Range(0, _welcomeAudios.Count)]);


    }
}
