using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VolumeMixer.ViewModels;

public class MainViewModel : ObservableRecipient
{
    private double channel1Volume;
    private double channel2Volume;
    private double channel3Volume;
    private double channel4Volume;

    public Command ConnectCommand { get; set; }
    public Command<string> MuteCommand { get; set; }

    public MainViewModel()
    {
        ConnectCommand = new Command(OnConnect);
        MuteCommand = new Command<string>(OnMute);
    }

    void OnMute(string channel)
    {
        (App.Current as App).Hermes.Publish("mute",$"{channel}");

        switch(int.Parse(channel))
        {
            case 1:
                IsOneMuted = !isOneMuted;
                break;
            case 2:
                IsTwoMuted = !isTwoMuted;
                break;
            case 3:
                IsThreeMuted = !isThreeMuted;
                break;
            case 4:
                IsFourMuted = !isFourMuted;
                break;
            default:
                break;
        }
        
    }

    private void OnConnect()
    {
        Shell.Current.GoToAsync("settings");
    }

    public double Channel1Volume
    {
        get
        {
            if (isOneMuted)
                return 0;

            return channel1Volume;
        }

        set
        {
            if (isOneMuted)
            {
                OnPropertyChanged(nameof(Channel1Volume));
            }
            else
            {
                channel1Volume = value;
                PublishVolumeChange(1, value);
            }
            
        }
    }

    public double Channel2Volume
    {
        get
        {
            if (isTwoMuted)
                return 0;

            return channel2Volume;
        }
        set
        {
            if (isTwoMuted)
            {
                OnPropertyChanged(nameof(Channel2Volume));
            }
            else
            {
                channel2Volume = value;
                PublishVolumeChange(2, value);
            }
        }
    }

    public double Channel3Volume
    {
        get
        {
            if (isThreeMuted)
                return 0;

            return channel3Volume;
        }
        set
        {
            if (isThreeMuted)
            {
                OnPropertyChanged(nameof(Channel3Volume));
            }
            else
            {
                channel3Volume = value;
                PublishVolumeChange(3, value);
            }
        }
    }

    public double Channel4Volume
    {
        get
        {
            if (isFourMuted)
                return 0;

            return channel4Volume;
        }
        set
        {
            if (isFourMuted)
            {
                OnPropertyChanged(nameof(Channel4Volume));
            }
            else
            {
                channel4Volume = value;
                PublishVolumeChange(4, value);
            }
        }
    }

    async Task PublishVolumeChange(int channel, double volume)
    {
        Debug.WriteLine($"channel: {channel} | volume: {volume}");
        await (App.Current as App).Hermes.Publish($"volume/{channel}", $"{volume}");
    }

    private bool isOneMuted;
    public bool IsOneMuted
    {
        get {
            return isOneMuted;
        }
        set
        {
            SetProperty(ref isOneMuted, value);
            OnPropertyChanged(nameof(Channel1Volume));
        }
    }

    private bool isTwoMuted;
    public bool IsTwoMuted
    {
        get
        {
            return isTwoMuted;
        }
        set
        {
            SetProperty(ref isTwoMuted, value);
            OnPropertyChanged(nameof(Channel2Volume));
        }
    }

    private bool isThreeMuted;
    public bool IsThreeMuted
    {
        get
        {
            return isThreeMuted;
        }
        set
        {
            SetProperty(ref isThreeMuted, value);
            OnPropertyChanged(nameof(Channel3Volume));
        }
    }

    private bool isFourMuted;
    public bool IsFourMuted
    {
        get
        {
            return isFourMuted;
        }
        set
        {
            SetProperty(ref isFourMuted, value);
            OnPropertyChanged(nameof(Channel4Volume));
        }
    }
}