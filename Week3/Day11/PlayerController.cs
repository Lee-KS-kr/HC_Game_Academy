using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class PlayerController : MonoBehaviour, IAttackable
    {
        #region Variable
        private const int _bulletCount = 7;
        private const float _attackCoolDown = 1.2f;

        [SerializeField] private Animator _animator;
        [SerializeField] private CameraShake _camera;

        private HumanBodyBones _bones = HumanBodyBones.Spine;
        [SerializeField] private Collider _target = null;
        private Collider[] _colliders;
        private Transform _spine;
        [SerializeField] Transform _gun;
        [SerializeField] private Bullet _bullet;

        private int _attackCount = 0;
        private float _moveSpeed = -1f;
        private float _playerSight = 5f;
        [SerializeField] private float _attackCool = 0f;
        public int HP { get; set; }

        public HpDelegate hpDelegate { get; set; }
        public DieDelegate onDieDelegate { get; set; }

        private readonly int hashMoveSpeed = Animator.StringToHash("moveSpeed");
        private readonly int hashIsBulletEmpty = Animator.StringToHash("isBulletEmpty");
        private readonly int hashOnAttack = Animator.StringToHash("onAttack");
        #endregion

        private void Start()
        {
            _spine = _animator.GetBoneTransform(_bones);
        }

        private void Update()
        {
            _attackCool += Time.deltaTime;

            OnMove();
            OnAttack();
            FindEnemy();
            AutoAttack();
        }

        private void LateUpdate()
        {
            if (null == _target || Vector3.Magnitude(transform.position - _target.transform.position) > _playerSight) return; // 비교 대상도 같이 제곱을 해 줘야한다.
            _spine.LookAt(_target.transform.position + Vector3.up, Vector3.up); // 이미 여기서 타겟이 있기 때문에 여기에서 캐싱하는 방법도 있다
            //var dir = _target.transform.position - _spine.transform.position;
            //_spine.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }

        private void OnMove()
        {
            if (Input.GetMouseButton(0))
            {
                int mask = 1 << 3; // 이 친구의 정체에 대해 써놓을 것
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hitInfo, mask))
                {
                    _moveSpeed = Mathf.Clamp(_moveSpeed + Time.deltaTime, 0, 3f);
                    transform.LookAt(hitInfo.point);
                    transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _moveSpeed);
                    //transform.Translate(transform.position + transform.TransformDirection(transform.forward * Time.deltaTime * _moveSpeed));
                    _animator.SetFloat(hashMoveSpeed, _moveSpeed);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _moveSpeed = -1;
                _animator.SetFloat(hashMoveSpeed, _moveSpeed);
            }
        }

        private void NewMove()
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
            if (Input.GetMouseButton(0))
            {

            }
            if (Input.GetMouseButtonUp(0))
            {

            }
        }

        private void OnAttack()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_bulletCount == _attackCount)
                {
                    _animator.SetBool(hashIsBulletEmpty, true);
                    _animator.SetTrigger(hashOnAttack);
                    _attackCount = 0;
                }
                else
                {
                    _attackCount++;
                    _animator.SetTrigger(hashOnAttack);
                    _attackCool = 0f;
                    _camera.StartCoroutine(_camera.Shake());
                }
            }
        }

        private void FindEnemy()
        {
            int enemyLayer = 1 << 6; // 에너미 레이어 6번
            _colliders = Physics.OverlapSphere(transform.position, _playerSight, enemyLayer);
            if (_colliders.Length < 1)
            {
                _target = null;
                return;
            }

            var distance = _playerSight;
            for (int i = 0; i < _colliders.Length; i++)
            {
                int wallMask = 1 << 7; // 벽 레이어 7번
                if (Physics.Linecast(transform.position + Vector3.up, _colliders[i].transform.position + Vector3.up, wallMask))
                {
                    //_colliders[i] = null;
                    continue;
                }

                //var newDis = Vector3.SqrMagnitude(transform.position - _colliders[i].transform.position);
                var newDis = Vector3.Distance(transform.position, _colliders[i].transform.position);
                if (distance > newDis)
                {
                    distance = newDis;
                    _target = _colliders[i];
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _playerSight);
        }

        private void AutoAttack()
        {
            if (_target == null || _attackCool < _attackCoolDown)
                return;

            if (_bulletCount == _attackCount)
            {
                _animator.SetBool(hashIsBulletEmpty, true);
                _animator.SetTrigger(hashOnAttack);
                _attackCount = 0;

                return;
            }

            OnFire();
        }

        private void OnFire()
        {
            var from = transform.position + Vector3.up;
            var dir = (_target.transform.position + Vector3.up) - from;
            var ray = new Ray(from, dir);
            var mask = 1 << 6; // 에너미 레이어 6번
            var damage = Random.Range(10, 21);

            if (!Physics.Raycast(ray, out var hitInfo, _playerSight, mask))
                return;

            _bullet.SetDirection(_target.transform.position - _gun.transform.position);
            _bullet.gameObject.transform.position = _gun.transform.position;
            _bullet.gameObject.SetActive(true);

            //_bullet.SetDestination(_target.transform.position + Vector3.up);
            //_bullet.gameObject.transform.position = transform.position + transform.up + transform.forward;
            //_bullet.gameObject.SetActive(true);
            Debug.Log(hitInfo.collider.gameObject.name);
            var hit = hitInfo.collider.gameObject.GetComponent<Enemy>();
            // 이것을 회피하는 방법은? 내가 쳐다보고 있다는 것은 내가 걔를 알고있다는 의미. 이것을 회피하기 위해서 여러 방법이 있을 수 있다.
            // 예를 들어 한번 수집을 해놓는다던가(딕셔너리에 키와 밸류 등으로... 회피 방법 생각해보기
            hit.OnHit(damage, transform.position);

            _attackCount++;
            _animator.SetTrigger(hashOnAttack);
            _attackCool = 0f;
            _camera.StartCoroutine(_camera.Shake());
        }

        public void OnHit(int dmg, Vector3 pos)
        {
            HP -= dmg;
        }

        public void OnDie()
        {

        }
    }
}