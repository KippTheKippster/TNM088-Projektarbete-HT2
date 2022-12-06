using Godot;
using System;

public class Audio : Node
{
	public void PlaySound(string path, int minPitch = 1, int maxPitch = 1)
	{
		AudioStreamPlayer audio = new AudioStreamPlayer();
		audio.Stream = GD.Load<AudioStream>("res://assets/Music/" + path);
		AddChild(audio);
        Random random = new Random();
        audio.PitchScale = random.Next(minPitch * 100 , maxPitch * 100)/100.0f;
		audio.Play();
	}
}
