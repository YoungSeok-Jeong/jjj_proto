using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

public class SpineController : MonoBehaviour {


    [Header("For Test")]
    public Sprite newSprite;    

    private SkeletonAnimation m_visual;
    public bool runTimeRepack;
    public Texture2D runtimeAtlas;
    public Material runtimeMaterial;

    public int test;

    void Awake() {
        m_visual = GetComponent<SkeletonAnimation>();
    }
	// Use this for initialization
	void Start () {
		
	}

    public void ChangeAttachment(string slotName, string partsName, string resPath, bool repack)
    {
        //make sprite
        Texture2D texture = Resources.Load<Texture2D>(resPath);
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        //make skin from material, sprite
        int idx = m_visual.Skeleton.FindSlotIndex(slotName);
        Material mat = m_visual.SkeletonDataAsset.atlasAssets[0].materials[0];
        Spine.Skin newSkin = new Spine.Skin("temp");
        Spine.Skin defaultSkin = m_visual.Skeleton.Data.DefaultSkin;
        Spine.Attachment att = defaultSkin.GetAttachment(idx, partsName);
        Spine.Attachment newAtt = att.GetRemappedClone(sprite, mat);

        newSkin.SetAttachment(idx, partsName, newAtt);

        //repack하여 텍스쳐를 다시 만든다.
        //순간 프레임 드랍 문제 가능성은 있지만 draw call 한 방에 해결.
        if (runTimeRepack)
        {
            Spine.Skin repackedSkin = new Spine.Skin("repacked");
            repackedSkin.Append(m_visual.Skeleton.Data.DefaultSkin);
            repackedSkin.Append(newSkin);

            repackedSkin = repackedSkin.GetRepackedSkin("all new skin", mat, out runtimeMaterial, out runtimeAtlas);
            m_visual.Skeleton.SetSkin(repackedSkin);
        }
        //repack하지 않고 새로운 sprite를 별도의 텍스쳐로 사용하여 순간 세팅속도 빠름.
        //draw call의 증가가 문제
        else
        {
            m_visual.Skeleton.SetSkin(newSkin);
        }
    }

    public void ChangeAttachment(string slotName, string partsName, AtlasAsset atlasAsset, string key, bool repack)
    {
        //make sprite
        //Material exMat = mixedMaterial;
        AtlasAsset atlas = atlasAsset;

        Spine.AtlasRegion reg = atlas.GetAtlas().FindRegion(key);
        Sprite sprite = reg.ToSprite();
        
        //Texture2D texture = (Texture2D)exMat.GetTexture(mixedKey);
        //Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        //make skin from material, sprite
        int idx = m_visual.Skeleton.FindSlotIndex(slotName);
        Material mat = m_visual.SkeletonDataAsset.atlasAssets[0].materials[0];
        Spine.Skin newSkin = m_visual.Skeleton.Skin;
        if (newSkin == null)
        {
            Debug.Log("new skin!!");
            newSkin = new Spine.Skin("temp");
        }
        
        Spine.Skin defaultSkin = m_visual.Skeleton.Data.DefaultSkin;
        Spine.Attachment att = defaultSkin.GetAttachment(idx, partsName);
        Spine.Attachment newAtt = att.GetRemappedClone(sprite, mat);

        newSkin.SetAttachment(idx, partsName, newAtt);

        //repack하여 텍스쳐를 다시 만든다.
        //순간 프레임 드랍 문제 가능성은 있지만 draw call 한 방에 해결.
        if (runTimeRepack)
        {
            Spine.Skin repackedSkin = new Spine.Skin("repacked");
            repackedSkin.Append(m_visual.Skeleton.Data.DefaultSkin);
            repackedSkin.Append(newSkin);

            repackedSkin = repackedSkin.GetRepackedSkin("all new skin", mat, out runtimeMaterial, out runtimeAtlas);
            m_visual.Skeleton.SetSkin(repackedSkin);
        }
        //repack하지 않고 새로운 sprite를 별도의 텍스쳐로 사용하여 순간 세팅속도 빠름.
        //draw call의 증가가 문제
        else
        {
            m_visual.Skeleton.SetSkin(newSkin);
        }
    }

    public void ToStringAllSlot()
    {
        int i = 0;
        foreach (Spine.Slot s in m_visual.Skeleton.slots)
        {
            string name = s.ToString();
            Debug.Log(++i + " = " + name);
        }
    }

    public void ToStringAllAttachment()
    {
   
    }

    public void editShader()
    {
        //셰이더를 Spine/Skeleton Fill을 사용하고 있을 때만 가능.
        Material[] mats = GetComponent<Renderer>().materials;
        foreach (Material mat in mats)
        {
            // Spine-Skeleton-Fill.shader 파일에서 Properties부분의 필드를 접근하여 조작가능.
            //mat.SetFloat("_FillPhase", a * 0.5f);
            //mat.SetColor("_FillColor", Color.red);
        }
    }
}
