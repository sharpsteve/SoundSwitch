﻿using System;
using Serilog;
using SoundSwitch.Audio.Manager;
using SoundSwitch.Audio.Manager.Interop.Enum;

namespace SoundSwitch.Framework.Audio.Microphone
{
    public class MicrophoneMuteToggler
    {
        private readonly AudioSwitcher _switcher;

        public MicrophoneMuteToggler(AudioSwitcher switcher)
        {
            _switcher = switcher;
        }

        /// <summary>
        /// Toggle mute state for the default microphone
        /// </summary>
        /// <param name="deviceId"></param>
        public void ToggleDefaultMute()
        {
            var microphone = _switcher.GetDefaultMmDevice(EDataFlow.eCapture, ERole.eCommunications);
            if (microphone == null)
            {
                Log.Information("Couldn't find a default microphone to toggle mute");
                return;
            }

            try
            {
               microphone.AudioEndpointVolume.Mute = !microphone.AudioEndpointVolume.Mute;
            }
            catch (Exception e)
            {
                Log.Error("Couldn't toggle mute on {device}:\n{exception}", microphone.FriendlyName, e);
            }
            
        }
    }
}