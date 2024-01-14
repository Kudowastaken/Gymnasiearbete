using System.Collections.Generic;
using UnityEngine;

public class SlicingTest : MonoBehaviour
{
    public GameObject graphics;
    Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        materials = GetMaterials(graphics);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetVector("sliceCenter", transform.position);
            materials[i].SetVector("sliceNormal", transform.forward);
        }
    }

    Material[] GetMaterials(GameObject graphics)
    {
        var renderers = graphics.GetComponentsInChildren<MeshRenderer>();
        var matList = new List<Material>();
        foreach (var renderer in renderers)
        {
            foreach (var material in renderer.materials)
            {
                matList.Add(material);
            }
        }
        return matList.ToArray();
    }
}
