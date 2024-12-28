using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using EnumTypes;
using System.Collections;

namespace donghyun.SpaceShip
{

    public class SpaceShip : MonoBehaviour
    {
        //필드
        [SerializeField] private List<GameObject> turret;
        [SerializeField] private GameObject yamatoCannon;
        [SerializeField] private GameObject shield;
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject engine;
        [SerializeField] private float cannonRotateSpeed = 5f;
        [SerializeField] private float turretRotateSpeed = 5f;
        [SerializeField] private float engineRotateSpeed = 5f;
        [SerializeField] private ShipState state = ShipState.None;

        private void FixedUpdate()
        {
            RotateObj(yamatoCannon, cannonRotateSpeed * Time.fixedDeltaTime);

            switch (state)
            {
                case ShipState.None:
                    break;
                case ShipState.WestTurret:
                    OnTurret(turret[0]);
                    break;
                case ShipState.EastTurret:
                    OnTurret(turret[1]);
                    break;
                case ShipState.SouthTurret:
                    OnTurret(turret[2]);
                    break;
                case ShipState.NorthTurret:
                    OnTurret(turret[3]);
                    break;
                case ShipState.YamatoCannon:
                    StartCoroutine(YamatoConnonRoutine());
                    state = ShipState.None;
                    break;
                case ShipState.Engine:
                    OnEngine();
                    break;
                case ShipState.Shield:
                    OnShield();
                    break;
            }
        }

        /// <summary>
        /// 오브젝트의 회전을 담당
        /// </summary>
        /// <param name="go"></param>
        /// <param name="speed"></param>
        private void RotateObj(GameObject go, float speed)
        {
            go.transform.Rotate(0f, 0f, speed);
        }

        /// <summary>
        /// 터렛의 움직임과 발사를 담당
        /// </summary>
        /// <param name="turret"></param>
        private void OnTurret(GameObject turret)
        {
            float maxRotation = 85f;

            Quaternion relativeToParent = Quaternion.Inverse(transform.rotation) * turret.transform.rotation;
            float currentRotation = relativeToParent.eulerAngles.z - turret.transform.parent.localEulerAngles.z;

            if (Input.GetKey(KeyCode.M))
            {

            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if (currentRotation >= -maxRotation)
                {
                    RotateObj(turret, turretRotateSpeed * Time.fixedDeltaTime);
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (currentRotation <= maxRotation)
                {
                    RotateObj(turret, -turretRotateSpeed * Time.fixedDeltaTime);
                }
            }
        }

        private IEnumerator YamatoConnonRoutine()
        {
            for (int i = 0; i < 15; i++)
            {
                OnYamatoCannon();
                yield return new WaitForSeconds(0.6f);
            }
        }

        /// <summary>
        /// 야마토 캐논의 발사를 담당
        /// </summary>
        private void OnYamatoCannon()
        {
            Vector2 direction = yamatoCannon.transform.GetChild(0).position - transform.position;
            direction.Normalize();

            GameObject yamatoMissile = ObjectPoolManager.GetObject(EPoolObjectType.YamatoMissileGroup);
            yamatoMissile.transform.position = yamatoCannon.transform.GetChild(0).position;
            yamatoMissile.transform.rotation = yamatoCannon.transform.rotation;

            yamatoMissile.GetComponent<Rigidbody2D>().linearVelocity = direction;
        }

        /// <summary>
        /// 엔진의 회전과 우주선의 이동 담당
        /// </summary>
        private void OnEngine()
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                RotateObj(engine, engineRotateSpeed * Time.fixedDeltaTime);
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                RotateObj(engine, -engineRotateSpeed * Time.fixedDeltaTime);
            }
        }

        /// <summary>
        /// 쉴드의 회전 담당
        /// </summary>
        private void OnShield()
        {

        }
    }
}
