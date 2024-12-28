using System;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

// 오브젝트 풀
[Serializable]
public class PoolInfo
{
    public EPoolObjectType type;
    public int initCount;
    public GameObject prefab;
    public GameObject container;

    public Queue<GameObject> poolQueue = new Queue<GameObject>();
}

public class ObjectPoolManager : MonoBehaviour
{
    //필드
    private static ObjectPoolManager instance;
    [SerializeField] private List<PoolInfo> poolInfoList; // 오브젝트 풀 리스트

    private void Awake()
    {
        instance = this;
        Initialize();
    }

    /// <summary>
    /// 각 풀마다 정해진 개수의 오브젝트를 생성해주는 초기화 함수 
    /// </summary>
    private void Initialize()
    {
        foreach (PoolInfo poolInfo in poolInfoList)
        {
            for (int i = 0; i < poolInfo.initCount; i++)
            {
                poolInfo.poolQueue.Enqueue(CreatNewObject(poolInfo));
            }
        }
    }

    /// <summary>
    /// 초기화 및 풀에 오브젝트가 부족할 때 오브젝트를 생성하는 함수
    /// </summary>
    /// <param name="poolInfo"></param>
    /// <returns></returns>
    private GameObject CreatNewObject(PoolInfo poolInfo)
    {
        //오브젝트 생성 후 비활성화인 상태로 반환
        GameObject newObject = Instantiate(poolInfo.prefab, poolInfo.container.transform);
        newObject.gameObject.SetActive(false);
        return newObject;
    }

    /// <summary>
    /// ObjectType(Enum)으로 해당하는 PoolInfo를 반환해주는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private PoolInfo GetPoolByType(EPoolObjectType type)
    {
        foreach (PoolInfo poolInfo in poolInfoList)
        {
            if (type == poolInfo.type)
            {
                return poolInfo;
            }
        }
        return null;
    }

    /// <summary>
    /// 오브젝트가 필요할 때 호출하는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static GameObject GetObject(EPoolObjectType type)
    {
        PoolInfo poolInfo = instance.GetPoolByType(type);

        GameObject objInstance = null;
        if (poolInfo.poolQueue.Count > 0)
        {
            objInstance = poolInfo.poolQueue.Dequeue();
        }
        else
        {
            objInstance = instance.CreatNewObject(poolInfo);
        }
        objInstance.SetActive(true);
        return objInstance;
    }

    /// <summary>
    /// 오브젝트 사용 후 다시 풀에 반환하는 함수
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    public static void ReturnObject(GameObject obj, EPoolObjectType type)
    {
        PoolInfo poolInfo = instance.GetPoolByType(type);
        poolInfo.poolQueue.Enqueue(obj);
        obj.SetActive(false);
    }
}
