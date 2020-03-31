using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    public List<Block>[] blocks = new List<Block>[256];
    void Start()
    {
        for(int i=0;i<blocks.Length;i++)
            blocks[i] = new List<Block>();
    }

    void Update()
    {
        
    }
    public void CreateBlock(Block block,Vector3 pos)
    {
        GameObject newBlock= Instantiate(block.gameObject, transform);
        newBlock.transform.position = Snap(pos);

        blocks[newBlock.GetComponent<Block>().ID].Add(newBlock.GetComponent<Block>());
        
    }
    public void DestroyBlock(Block block)
    {
        Destroy(block.gameObject);
        blocks[block.ID].Remove(block);
    }
    Vector3 Snap(Vector3 v)
    {
        return new Vector3(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
    }
}
