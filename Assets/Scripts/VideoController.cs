using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {
	public GameObject tv;
	public GameObject tv2;
	public Renderer rend;
	public RawImage rend2;
	public RawImage muteBut;
	public Material on;
	public Material off;
	public Texture on2;
	public Texture off2;
	public RawImage fullScreen;

	public Texture mute;
	public Texture unmute;

	public Text timeStamp;
	public Slider timeBar;
	//private bool isPlaying;
	private VideoPlayer videoPlayer;
	private AudioSource audioSource;

	Renderer display;
	MeshCollider displayCollider;

	float delay = 0;
	float onTime = 0;
	void Start() {
		videoPlayer = GetComponent<VideoPlayer>();
		display = GetComponent <Renderer> ();
		displayCollider = GetComponent <MeshCollider> ();
		// Add AudioSource
		audioSource = GetComponent<AudioSource>();

		// Disable play on awake for both video and audio
		videoPlayer.playOnAwake = false;
		videoPlayer.waitForFirstFrame = true;
		audioSource.playOnAwake = false;
		//audioSource.Pause();
		videoPlayer.isLooping = true;

		// Video clip from URL
		videoPlayer.source = VideoSource.Url;
		videoPlayer.url = "http://workthehive.net/unity/videos/HistoryOfAnime.mp4";
		//videoPlayer.url = "https://drive.google.com/file/d/1geQlrYyYwMkAaGBBFo7XIAU9LKox_Niz/view?usp=sharing";

		// Set audio output to audioSource
		videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

		// Assign the audio from video to audioSource to be played
		videoPlayer.EnableAudioTrack(0, true);
		videoPlayer.controlledAudioTrackCount = 1;
		videoPlayer.SetTargetAudioSource(0, audioSource);


		// Set video to play then prepare Audio to prevent buffering
		videoPlayer.Prepare();
		timeBar.maxValue = 39 + 3*60;
		// Play video
		//videoPlayer.Play();

		// Play sound
		//audioSource.Play();
	}

	void Update() {
		//isPlaying = videoPlayer.isPlaying;

		if (!display.enabled) {
			if (videoPlayer.isPlaying) {
				videoPlayer.Pause ();
				rend.material = off;
				rend2.texture = off2;
				fullScreen.enabled = false;
				fullScreen.gameObject.SetActive (false);
				delay = 0;
			}
		}
		displayCollider.enabled = display.enabled;

		if (Input.GetMouseButtonDown (0) && !fullScreen.enabled) {
			RaycastHit hit;
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);
			if (hit.collider.gameObject == this.gameObject) {
				if (tv.activeInHierarchy) {
					tv.SetActive (false);
				} else {
					tv.SetActive (true);
					onTime = 0;
				}
				if (delay == 0)
					delay += Time.deltaTime;
				else if (delay > 0) {
					FullScreen ();
					delay = 0;
				}
			} else if (hit.collider.gameObject == rend.gameObject) {
				PlayPause ();
			}

		}
		if (delay > 0 && videoPlayer.isPlaying) {
			delay += Time.deltaTime;
		}
		if (delay > 0.5f) {
			delay = 0;
		}

		if (tv2.activeInHierarchy || tv.activeInHierarchy) {
			onTime += Time.deltaTime;
		}
		if (onTime > 2) {
			tv2.SetActive (false);
			tv.SetActive (false);
			onTime = 0;
		}

		if (tv2.activeInHierarchy)
			VideoLength ();

	}

	//toggle play and pause
	public void PlayPause(){
		if (!videoPlayer.isPlaying) {
			videoPlayer.Play ();
			audioSource.Play ();
			rend.material = on;
			rend2.texture = on2;
		} else if(videoPlayer.isPlaying) {
			videoPlayer.Pause ();

			rend.material = off;
			rend2.texture = off2;
		}
	}

	//toggle mute of audio
	public void Mute(){
		audioSource.mute = !audioSource.mute;
		if (!audioSource.mute)
			muteBut.texture = mute;
		if (audioSource.mute)
			muteBut.texture = unmute;
	}

	public void FullScreen(){
		if (fullScreen.enabled) {
			fullScreen.enabled = false;
			fullScreen.gameObject.SetActive (false);
		} else {
			fullScreen.enabled = true;
			fullScreen.gameObject.SetActive (true);
		}
	}

	//to togle ui when pressed
	public void UIHider(){
		onTime = 0;
		tv2.SetActive (!tv2.activeInHierarchy);
	}

	//to display video length
	void VideoLength(){
		int currentTime = (int)videoPlayer.time;
		string formatTime = Mathf.FloorToInt (currentTime / 60f) + ":" + (int)(videoPlayer.time - Mathf.FloorToInt (currentTime / 60f) * 60);
		timeStamp.text = formatTime + " / 3:39" ;
	
			
		timeBar.value = (int)videoPlayer.time;

		

	}
	public void ModifyTime(Slider slider){
		videoPlayer.time = (double)slider.value;
	}

}