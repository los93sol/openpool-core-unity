using UnityEngine;
using System.Collections;
using OpenPool.Simulator;
using UnityEngine.Video;
using Unity.VisualScripting;

public class MovieTextureAutoPlay : MonoBehaviour {
	[SerializeField]
	VideoClip clip;

    void Start() {
		var renderer = GetComponent<Renderer>();
		renderer.enabled = false;

		var videoPlayer = gameObject.AddComponent<VideoPlayer>();

        videoPlayer.clip = clip;
        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
		videoPlayer.targetMaterialRenderer = renderer;
		videoPlayer.targetMaterialProperty = "_MainTex";		

        videoPlayer.sendFrameReadyEvents = true;
        videoPlayer.frameReady += VideoPlayer_frameReady;

        videoPlayer.Play();
	}

    private void VideoPlayer_frameReady(VideoPlayer source, long frameIdx)
    {
		source.frameReady -= VideoPlayer_frameReady;
		source.sendFrameReadyEvents = false;

		GetComponent<Renderer>().enabled = true;
    }
}
