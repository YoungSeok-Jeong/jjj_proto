using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TestParts : MonoBehaviour {
    [SpineSlot]
    public string slot_1;
    [SpineAttachment(slotField: "slot_1")]
    public string attachmentKey_1;
    public AtlasAsset atlasAsset_1;
    [SpineAtlasRegion(atlasAssetField: "atlasAsset_1")]
    public string mixedAtlas_1;

    [Space(10)]
    [SpineSlot]
    public string slot_2;
    [SpineAttachment(slotField: "slot_2")]
    public string attachmentKey_2;
    public AtlasAsset atlasAsset_2;
    [SpineAtlasRegion(atlasAssetField: "atlasAsset_2")]
    public string mixedAtlas_2;

    [Space(10)]
    [SpineSlot]
    public string slot_3;
    [SpineAttachment(slotField: "slot_3")]
    public string attachmentKey_3;
    public AtlasAsset atlasAsset_3;
    [SpineAtlasRegion(atlasAssetField: "atlasAsset_3")]
    public string mixedAtlas_3;

    [Space(10)]
    [SpineSlot]
    public string slot_4;
    [SpineAttachment(slotField: "slot_4")]
    public string attachmentKey_4;
    public AtlasAsset atlasAsset_4;
    [SpineAtlasRegion(atlasAssetField: "atlasAsset_4")]
    public string mixedAtlas_4;

    [Space(10)]
    [SpineSlot]
    public string slot_5;
    [SpineAttachment(slotField: "slot_5")]
    public string attachmentKey_5;
    public AtlasAsset atlasAsset_5;
    [SpineAtlasRegion(atlasAssetField: "atlasAsset_5")]
    public string mixedAtlas_5;

	// Use this for initialization
	void Start () {
        SpineController controller = GetComponent<SpineController>();
        controller.ChangeAttachment(slot_1, attachmentKey_1, atlasAsset_1, mixedAtlas_1, false);
        controller.ChangeAttachment(slot_2, attachmentKey_2, atlasAsset_2, mixedAtlas_2, false);
        controller.ChangeAttachment(slot_3, attachmentKey_3, atlasAsset_3, mixedAtlas_3, false);
        controller.ChangeAttachment(slot_4, attachmentKey_4, atlasAsset_4, mixedAtlas_4, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
