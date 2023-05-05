using UnityEngine;
using System.Collections;
using OpenPool.Simulator;
using UnityEngine.Video;
using Unity.VisualScripting;

public class MovieTextureAutoPlay : MonoBehaviour {
	[SerializeField]
	VideoClip clip;

    void Start() {
		//Material mat = GetComponent<Renderer>().material;		
		//Texture texture = mat.mainTexture;
		var renderer = GetComponent<Renderer>();
		renderer.enabled = false;

		var videoPlayer = gameObject.AddComponent<VideoPlayer>();

		videoPlayer.sendFrameReadyEvents = true;
        videoPlayer.frameReady += VideoPlayer_frameReady;
        videoPlayer.clip = clip;
        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
		videoPlayer.targetMaterialRenderer = renderer;
		videoPlayer.targetMaterialProperty = "_MainTex";		
		videoPlayer.isLooping = true;
		//videoPlayer.frame = 1;
		//renderer.material.color = Color.clear;
        //videoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
		//videoPlayer.Prepare();
		videoPlayer.Play();
		//gameObject.SetActive(false);
		//MovieTexture movie = texture as MovieTexture;

		//movie.Stop();
		//movie.loop = true;
		//movie.Play();
	}

    private void VideoPlayer_frameReady(VideoPlayer source, long frameIdx)
    {
		source.frameReady -= VideoPlayer_frameReady;
		source.sendFrameReadyEvents = false;

		GetComponent<Renderer>().enabled = true;
        //throw new System.NotImplementedException();
    }

    private void VideoPlayer_prepareCompleted(VideoPlayer source)
    {
		source.prepareCompleted -= VideoPlayer_prepareCompleted;		
		source.Play();
		gameObject.SetActive(true);
    }
}
