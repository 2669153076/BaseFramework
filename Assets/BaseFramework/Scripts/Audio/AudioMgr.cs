using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework.Runtime{
    /// <summary>
    /// 音乐音效管理器
    /// </summary>
    public class AudioMgr : SingletonAutoMono<AudioMgr>
    {
        private AudioMgr() { }
        private AudioSource _sfx;
        private AudioSource _bgm;
        private AudioSource _music;

        private float _musicVolume = -1;
        private float _bgmVolume = -1;
        private float _sfxVolume = -1;

        private GameObject _sfxObj = null;
        private List<AudioSource> _sfxList = new();

        public void PlayBGM(AudioClip clip, bool isLoop = false)
        {
            if (_music == null)
            {
                GameObject obj = new GameObject("BGM");
                DontDestroyOnLoad(obj);
                _bgm = obj.AddComponent<AudioSource>();
                _bgm.volume = _bgmVolume < 0 ? 1.0f : _bgmVolume;
            }
            if (_musicVolume < 0)
            {
                _bgmVolume = (float)PlayerPrefsMgr.GetInstance().LoadData("BGMVloume", typeof(float));
                _music.volume = _bgmVolume;
            }

            _bgm.clip = clip;
            _bgm.loop = isLoop;
            _bgm.Play();
        }
        public void StopBGM()
        {
            if (_bgm == null)
            {
                //TODO:打印错误日志
                Debug.LogError("没有找到音乐组件，检查是否播放过音乐");
                return;
            }
            _bgm.Stop();
        }
        public void PauseBGM(AudioClip clip)
        {
            _bgm.Pause();
        }

        public void SetBGMVolume(float volume)
        {
            if (_bgm == null)
            {
                //TODO:打印错误日志
                Debug.LogError("没有找到音乐组件，检查是否播放过音乐");
                return;
            }
            _bgmVolume = volume;
            _bgm.volume = volume;
            PlayerPrefsMgr.GetInstance().SaveData("BGMVolume", volume);
        }


        public void PlayMusic(AudioClip clip,bool isLoop = false)
        {
            if(_music == null){
                GameObject obj = new GameObject("Music");
                DontDestroyOnLoad(obj);
                _music = obj.AddComponent<AudioSource>();
                _music.volume = _musicVolume < 0 ? 1.0f : _musicVolume;
            }
            if (_musicVolume < 0)
            {
                _musicVolume = (float)PlayerPrefsMgr.GetInstance().LoadData("MusicVloume", typeof(float));
                _music.volume = _musicVolume;
            }

            _music.clip = clip;
            _music.loop = isLoop;
            _music.Play();
        }
        public void StopMusic()
        {
            if(_music == null)
            {
                //TODO:打印错误日志
                Debug.LogError("没有找到音乐组件，检查是否播放过音乐");
                return;
            }
            _music.Stop();
        }
        public void PauseMusic(AudioClip clip)
        {
            _music.Pause();
        }

        public void SetMusicVolume(float volume)
        {
            if (_music == null)
            {
                //TODO:打印错误日志
                Debug.LogError("没有找到音乐组件，检查是否播放过音乐");
                return;
            }
            _musicVolume = volume;
            _music.volume = volume;
            PlayerPrefsMgr.GetInstance().SaveData("MusicVolume", volume);
        }


        public void PlaySFX(AudioClip clip, bool isLoop)
        {
            if (_sfxObj = null)
            {
                _sfxObj = new GameObject();
            }

            _sfx = _sfxObj.AddComponent<AudioSource>();

            if (_musicVolume < 0)
            {
                _sfxVolume = (float)PlayerPrefsMgr.GetInstance().LoadData("SFXVloume", typeof(float));
                _sfx.volume = _sfxVolume<0?1.0f:_sfxVolume;
            }

            _sfx.clip = clip;
            _sfx.Play();
            _sfxList.Add(_sfx); 
        }

        public void StopSFX(AudioSource source)
        {
            _sfxList.Remove(_sfx);
            source.Stop();
        }

        public void SetSFXVolume(float volume)
        {
            if (_sfx == null)
            {
                //TODO:打印错误日志
                Debug.LogError("没有找到音乐组件，检查是否播放过音乐");
                return;
            }
            _sfxVolume = volume;
            _sfx.volume = volume;
            PlayerPrefsMgr.GetInstance().SaveData("SFXVolume", volume);
        }
    }
}