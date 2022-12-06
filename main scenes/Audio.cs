using Godot;
using System;

public class Audio : Node
{
	public void PlaySound(string path, float minPitch = 1, float maxPitch = 1, float volume = 0)
	{
		AudioStreamPlayer audio = new AudioStreamPlayer();
		audio.Stream = GD.Load<AudioStream>("res://assets/Music/" + path);
		AddChild(audio);
        Random random = new Random();
        audio.PitchScale = random.Next((int)(minPitch * 100) , (int)(maxPitch * 100))/100.0f;
        audio.VolumeDb = volume;
        audio.Connect("finished", audio, "queue_free");
        
		audio.Play();
	}
}
