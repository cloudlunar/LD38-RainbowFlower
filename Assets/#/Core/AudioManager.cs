using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public static AudioManager ins;
    private void Start()
    {
        ins = this;
    }
    //根据名称获取音乐AudioSource//
    public AudioSource GetAsrc(string _name)
    {
        if (!this || !transform)
            return null;
        GameObject obj = GameObject.Find("Audio_" + _name);

        if (obj == null)
        {
            obj = new GameObject("Audio_" + _name);
          //  obj.transform.parent = this.transform;
            obj.AddComponent<AudioSource>();
            DontDestroyOnLoad(obj);

        }
        var asrc = obj.GetComponent<AudioSource>();
        asrc.clip = Resources.Load(_name, typeof(AudioClip)) as AudioClip;
        asrc.maxDistance = 100000;
        return asrc;
    }

    //播放指定名称音乐//
    public bool PlayAudio(string _name, float _vol = 1f, float _pitch = 1f, bool _loop = false)
    {

        if (PlayerPrefs.HasKey("Volume"))
            _vol *= PlayerPrefs.GetFloat("Volume");
        AudioSource asrc = GetAsrc(_name);
        if (asrc == null)
            return false;
        asrc.volume = _vol;
        asrc.pitch = _pitch;
        asrc.loop = _loop;
        if (!asrc.isPlaying)
            asrc.Play();
        return true;
    }

    //停止指定名称音乐//
    public bool StopAudio(string _name)
    {

        var ga = GetAsrc(_name);
        if (ga)
        {
            ga.Stop();
            return true;
        }
        return false;
    }
}