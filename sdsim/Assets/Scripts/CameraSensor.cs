using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSensor : MonoBehaviour {

	public Camera sensorCam;
	// public Camera stereoCam; // My own added code
	public int width = 256;
	public int height = 256;

	Texture2D tex;
	RenderTexture ren;

	// My added code

	// Texture2D tex_2;
	// RenderTexture ren_2;
	void Awake()
	{
		tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		ren = new RenderTexture(width, height, 16, RenderTextureFormat.ARGB32);
		sensorCam.targetTexture = ren;

		// My added Code
		// tex_2 = new Texture2D(width, height, TextureFormat.RGB24, false);
		// ren_2 = new RenderTexture(width, height, 16, RenderTextureFormat.ARGB32);
		// sensorCam.targetTexture = ren_2;
	}

	Texture2D RTImage(Camera cam)
	{
		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = cam.targetTexture;
		cam.Render();
		tex.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
		tex.Apply();
		RenderTexture.active = currentRT;
		return tex;
	}

	public Texture2D GetImage()
	{
		return RTImage(sensorCam);
	}

	public byte[] GetImageBytes()
	{
		return GetImage().EncodeToJPG();
	}
}
