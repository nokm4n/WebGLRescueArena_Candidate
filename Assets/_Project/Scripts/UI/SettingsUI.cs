using UnityEngine;
using UnityEngine.UI;
namespace WebGLRescueArena 
{ 
    public sealed class SettingsUI : MonoBehaviour 
    { 
        [SerializeField] private Toggle musicToggle; 
        [SerializeField] private Toggle sfxToggle; 

        private AudioManager audioManager; 
        private SaveService saveService; 

        private void OnEnable() 
        {
            audioManager = FindAnyObjectByType<AudioManager>();
            saveService = FindAnyObjectByType<SaveService>();
            musicToggle.isOn = saveService.MusicEnabled; 
            sfxToggle.isOn = saveService.SfxEnabled; 
        } 
        public void SetMusic(bool enabled) 
        { 
            saveService.SetMusicEnabled(enabled); 
            audioManager.SetMusic(enabled); 
        } 
        public void SetSfx(bool enabled) 
        { 
            saveService.SetSfxEnabled(enabled); 
            audioManager.SetSfx(enabled); 
        } 
    } 
}
