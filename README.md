openpool-core-unity
===================

The original repo never had the meta files for the Unity project so I started working through the process of reconciling the broken project references.

This repo is a work in progress.  As it stands I have the scene mostly working again, but there are some references I have not yet determined.  Below is the list of GUIDs that I have mapped after the initial import into Unity 4.5.

The following are references that are still known to be broken  
- 0000000000000000f000000000000000 (2100000 (21 - Material) - OpenPool\Assets\OpenPool\Resources\Models\Balls\Materials\billiard_ballMat.mat)
- 0000000000000000e000000000000000 (13200000 (132 - GUIText) - OpenPool\Assets\OpenPool\Resources\Prefabs\OpenPoolManager.prefab)
- f445281fde84b4e51b2b78b161c9f109 (21200000 (212 - SpriteRenderer) - OpenPool\Assets\OpenPool\Resources\Prefabs\OpenPoolManager.prefab)
- 277cde6ef24dd482da4f36fa24753b41 (3300002 (33 - MeshFilter) - OpenPool\Assets\OpenPool\Resources\Prefabs\EditorOnly.prefab)
- 8d1f65edff2cb46fc9293fd7791acca3 (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Models\Charactor\Materials\eye02_type1.mat)
- 2a434c44363dc49eabcd55ec6b28744a (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Models\Fish\Materials\fish_TATSU_mat.mat)
- baf6db83d83f140f580ae5b3e31b506f (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Models\Fish\Materials\fish_HAGI_mat_1.mat)
- bb45751d9f5d44936a803e0cf290dc2c (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Models\Fish\Materials\fish_FUE_YAKKO_mat_1.mat)
- 6b007e6196a784b2ab064dfacab68669 (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Models\Fish\Materials\eye02_type1.mat)
- 7dd50fe95eeec4528b00f0b4a61b1504 (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Models\Fish\Materials\eye01_type1.mat)
- 32768f07be2bf4a3480af3c23977e32a (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Models\Fish\Materials\eye01_mat.mat)
- 8cc391d4db3014c53a972ce126d3e2bb (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Models\Fish\Materials\eye01_mat.mat)
- 6c223460028bc4d198e79cf4d4fb2ce3 (11400000 (114 - MonoBehaviour) - OpenPool\Assets\_SeaSaw\Prefabs\Effects\PocketAnimaion 5.prefab)
- a9aaa3f5f2d574f228e4a21aa38b61e4 (1165458117 (114 - MonoBehaviour) - OpenPool\Assets\_SeaSaw\Scenes\FishPreview.unity)
- b3728d1488b02490cbd196c7941bf1f8 (1165458117 (114 - MonoBehaviour) - OpenPool\Assets\_SeaSaw\Scenes\FishPreview.unity)
- 017ca72b9e8a749058d13ebd527e98fa (1165458117 (114 - MonoBehaviour) - OpenPool\Assets\_SeaSaw\Scenes\FishPreview.unity)
- ce0cb2621f6d84e21a87414e471a3cce (1165458117 (114 - MonoBehaviour) - OpenPool\Assets\_SeaSaw\Scenes\FishPreview.unity)
- 6f1418cffd12146f2a83be795f6fa5a7 (1165458117 (114 - MonoBehaviour) - OpenPool\Assets\_SeaSaw\Scenes\FishPreview.unity)
- c182fa94a5a0a4c02870641efcd38cd5 (1165458117 (114 - MonoBehaviour) - OpenPool\Assets\_SeaSaw\Scenes\FishPreview.unity)
- cd5b323dcc592457790ff18b528f5e67 (1165458117 (114 - MonoBehaviour) - OpenPool\Assets\_SeaSaw\Scenes\FishPreview.unity)
- c547503fff0e8482ea5793727057041c (1165458117 (114 - MonoBehaviour) - OpenPool\Assets\_SeaSaw\Scenes\FishPreview.unity)
- 6f30fa4ba7d324910b9b2e2708c19d88 (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Textures\Effects\PocketIn\PocketMat 5.mat)
- 70ffdeb79938c4535b706a932349809c (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Textures\Effects\PocketIn\PocketMat 4.mat)
- d6f2ceee2b74944a2b5894ef390de0be (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Textures\Effects\PocketIn\PocketMat 3.mat)
- a996ad2fda1974f76be44ba22f65d9ec (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Textures\Effects\PocketIn\PocketMat 2.mat)
- 178c27d6855c049d087656329ce580fb (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Textures\Effects\PocketIn\PocketMat 1.mat)
- 4e36775a0b642430c831e87aa6ee9eaa (2100000 (21 - Material) - OpenPool\Assets\_SeaSaw\Textures\Effects\PocketIn\PocketMat 0.mat)

OpenPool core module for kinect 2 & Sample Effect powered by Unity platform

# MainModuleNewKinect
- This is the windows application to converts kinect input to openpool formatted OSC packets for effect programs.
- You need a kinect2 for windows to use this application.
- You need Windows 8+, VS2012+, OpenCV 2.4.9 and SDK for Kinect4Windows v2 to build this application.


# UnityModule
- This is the sample effect program created using Unity.
- This does not require Pro version of Unity (Standard version is enough to build).


# License
Each module has its own license. Please check each project directory.
