using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Stofipy.App.Converters;
using Stofipy.App.Messages;
using Stofipy.App.ViewModels;
using Stofipy.BL.Models;
using Stofipy.DAL;

namespace Stofipy.App.Views;

public partial class SectionBottom : IRecipient<PlayFileMessage>, IRecipient<PlayPauseButtonClickedMessage>
{
    private readonly FilesInQueueVM _viewModel;
    private readonly IMessenger _messenger;
    private readonly DALOptions _dalOptions;
    private readonly SecondsToTimeConverter _secondsToTimeConverter;
    private double volumeSliderValueBeforeMuted;

    public SectionBottom(FilesInQueueVM viewModel, IMessenger messenger, DALOptions dalOptions) : base(viewModel)
    {
        _viewModel = viewModel;
        _messenger = messenger;
        _dalOptions = dalOptions;
        messenger.Register<PlayFileMessage>(this);
        messenger.Register<PlayPauseButtonClickedMessage>(this);
        InitializeComponent();
        MediaElement.PropertyChanged += MediaElement_PropertyChanged;
        MediaElement.PositionChanged += MediaElement_OnPositionChanged;
        MediaElement.MediaEnded += MediaElement_MediumEnded;
        MediaElement.Volume = 0.5;
        _secondsToTimeConverter = new SecondsToTimeConverter();
    }

    void MediaElement_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == MediaElement.DurationProperty.PropertyName)
        {
            TimeSlider.Maximum = MediaElement.Duration.TotalSeconds;
            TimeSliderMaxValue.Text = _secondsToTimeConverter.Convert((int)MediaElement.Duration.TotalSeconds, null, null, CultureInfo.CurrentCulture).ToString() ?? string.Empty;
        }
    }
    
    void MediaElement_OnPositionChanged(object? sender, MediaPositionChangedEventArgs e)
    {
        TimeSlider.Value = e.Position.TotalSeconds;
        TimeSliderCurrentValue.Text = _secondsToTimeConverter.Convert((int)e.Position.TotalSeconds, null, null, CultureInfo.CurrentCulture).ToString() ?? string.Empty;
    }
    
    void MediaElement_MediumEnded(object? sender, EventArgs e)
    {
        _messenger.Send(new NextFileMessage());
    }
    
    void OnPlayPauseButtonClicked(object? sender, EventArgs? args)
    {
        switch (MediaElement.CurrentState)
        {
            case MediaElementState.Stopped or MediaElementState.Paused:
                MediaElement.Play();
                _messenger.Send(new MediaElementPlayMessage());
                break;
            case MediaElementState.Playing:
                MediaElement.Pause();
                _messenger.Send(new MediaElementPauseMessage());
                break;
        }
    }
    
    void OnPreviousSongButtonClicked(object sender, EventArgs args)
    {
        _messenger.Send(new PreviousFileMessage());
    }
    
    void OnNextSongButtonClicked(object sender, EventArgs args)
    {
        _messenger.Send(new NextFileMessage());
    }
    
    private async void TimeSlider_DragCompleted(object? sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(sender);

        var newValue = ((Slider)sender).Value;
        await MediaElement.SeekTo(TimeSpan.FromSeconds(newValue), CancellationToken.None);

        MediaElement.Play();
    }
    private void VolumeSlider_ValueChanged(object? sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(sender);

        var newValue = ((Slider)sender).Value;
        MediaElement.Volume = newValue;

        if (newValue == 0.0)
        {
            MediaElement.ShouldMute = true;
            MuteButton.Text = "Muted";
        }
        else
        {
            MediaElement.ShouldMute = false;
            MuteButton.Text = "Mute";
            volumeSliderValueBeforeMuted = newValue;
        }
    }
    
    void OnMuteClicked(object? sender, EventArgs e)
    {
        if (MediaElement.ShouldMute)
        {
            Unmute();
        }
        else
        {
            Mute();
        }
    }

    private void Unmute()
    {
        MediaElement.ShouldMute = false; 
        MuteButton.Text = "Mute";
        VolumeSlider.Value = volumeSliderValueBeforeMuted > 0 ? volumeSliderValueBeforeMuted : 0.5;
    }
    
    private void Mute()
    {
        MediaElement.ShouldMute = true;
        MuteButton.Text = "Muted";
        volumeSliderValueBeforeMuted = VolumeSlider.Value;
        VolumeSlider.Value = 0;
    }
    

    private void PlayItem(FilesInQueueModel item)
    {
        _messenger.Send(new MediaElementPlayMessage());
        MainThread.BeginInvokeOnMainThread(() =>
        {
            MediaElement.MetadataTitle = item.FileName;
            MediaElement.MetadataArtist = item.AuthorName;
            MediaElement.MetadataArtworkUrl = "";

            var contentFolder = _dalOptions.ContentFilesDirectory;
            var mp4File = Path.Combine(contentFolder, $"{item.FileId}.mp4");
            var mp3File = Path.Combine(contentFolder, $"{item.FileId}.mp3");
            var fallbackFile = Path.Combine(contentFolder, "123.mp3");

            string? fileToPlay = null;

            if (File.Exists(mp4File))
                fileToPlay = mp4File;
            else if (File.Exists(mp3File))
                fileToPlay = mp3File;
            else if (File.Exists(fallbackFile))
                fileToPlay = fallbackFile;

            try
            {
                MediaElement.Source = MediaSource.FromFile(fileToPlay);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        });
    }

    public void Receive(PlayFileMessage message)
    {
        PlayItem(message.File);
    }    
    public void Receive(PlayPauseButtonClickedMessage message)
    {
        OnPlayPauseButtonClicked(null, null);
    }

    // private void VolumeSliderAreaEntered(object? sender, PointerEventArgs e)
    // {
    //     if (Application.Current != null && Application.Current.Resources.TryGetValue("GreenLight", out var color))
    //     {
    //         VolumeSlider.MinimumTrackColor = (Color)color;
    //     }
    // }
    // private void VolumeSliderAreaExited(object? sender, PointerEventArgs e)
    // {
    //     if (Application.Current != null && Application.Current.Resources.TryGetValue("White", out var color))
    //     {
    //         VolumeSlider.MinimumTrackColor = (Color)color;
    //     }
    // }
}