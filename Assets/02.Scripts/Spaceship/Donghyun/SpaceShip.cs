using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public enum State
{ 
    None,
    WestTurret,
    EastTurret,
    SouthTurret,
    NorthTurret,
    YamatoCannon,
    Engine,
    Shield
}


public class SpaceShip : MonoBehaviour
{
    [SerializeField] private List<GameObject> turret;
    [SerializeField] private GameObject yamatoCannon;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject engine;
    [SerializeField] private float cannonRotateSpeed = 5f;
    [SerializeField] private float turretRotateSpeed = 5f;
    [SerializeField] private float engineRotateSpeed = 5f;
    [SerializeField] private State state = State.None;

    private void FixedUpdate() 
    {
        RotateObj(yamatoCannon, cannonRotateSpeed * Time.fixedDeltaTime);

        switch (state)
        {
            case State.None:
                break;
            case State.WestTurret:
                OnTurret(turret[0]);
                break;
            case State.EastTurret:
                OnTurret(turret[1]);
                break;
            case State.SouthTurret:
                OnTurret(turret[2]);
                break;
            case State.NorthTurret:
                OnTurret(turret[3]);
                break;
            case State.YamatoCannon:
                OnYamatoCannon();
                break;
            case State.Engine:
                OnEngine();
                break;
            case State.Shield:
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

        if(Input.GetKey(KeyCode.M))
        {

        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if(currentRotation >= -maxRotation)
            {
                RotateObj(turret, turretRotateSpeed * Time.fixedDeltaTime);
            }
        }
        else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (currentRotation <= maxRotation)
            {
                RotateObj(turret, -turretRotateSpeed * Time.fixedDeltaTime);
            }
        }
    }

    /// <summary>
    /// 야마토 캐논의 발사를 담당
    /// </summary>
    private void OnYamatoCannon()
    {
        Vector2 direction = yamatoCannon.transform.position - transform.position;
        direction.Normalize();

        
        
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
