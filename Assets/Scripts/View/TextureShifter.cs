using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureShifter : MonoBehaviour
{
    Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    void Update()
    {
        float ofs = material.GetFloat("_TxtrOffset");
        material.SetFloat("_TxtrOffset", ofs + 0.01f);
    }
}
