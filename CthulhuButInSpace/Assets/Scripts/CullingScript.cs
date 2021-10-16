using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CullingScript : MonoBehaviour
{
    [SerializeField]
    private float playerDist = 10f;
    private GameObject player;
    private List<Cull> cullingObjs;
    private IEnumerator m_coroutine = null;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cullingObjs = new List<Cull>();
        StartCoroutine(IsActive());
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_coroutine != null) return;
        {
            m_coroutine = IsActive();
            StartCoroutine(m_coroutine);
        }
    }

    public void Register(Cull item)
    {
        cullingObjs.Add(item);
    }

    public void DeRegister(Cull item)
    {
        cullingObjs.Remove(item);
    }

    private IEnumerator IsActive()
    {
        List<Cull> removeList = new List<Cull>();

        if (cullingObjs.Count > 0f)
        {
            foreach (Cull item in cullingObjs)
            {
                if (item.item == null)
                {
                    removeList.Add(item);
                    continue;
                }

                item.item.SetActive(Vector3.Distance(player.transform.position, item.objPos) < playerDist);
                // Alternative one liner, but might be harder to read:
                //item.m_item.SetActive(Vector3.Distance(m_player.transform.position, item.m_itemPos) < m_distanceFromPlayer);

            }
        }

        yield return new WaitForSeconds(0.01f);

        if (removeList.Count > 0f)
        {
            foreach (Cull item in removeList)
            {
                cullingObjs.Remove(item);
            }
        }

        yield return new WaitForSeconds(0.01f);

        m_coroutine = null;
    }
}





public class Cull
{
    public GameObject item;
    public Vector3 objPos;
        public Cull(GameObject item, Vector3 objPos)
        {
        this.item = item;
        this.objPos = objPos;
        }
    
}