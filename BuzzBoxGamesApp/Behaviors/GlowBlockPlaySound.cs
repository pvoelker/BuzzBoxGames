using BuzzBoxGamesApp.Game;
using Plugin.Maui.Audio;
using System;

namespace BuzzBoxGamesApp.Behaviors
{
    public partial class GlowBlockPlaySound : Behavior<GlowBlock>
    {
        private AsyncAudioPlayer? _audioPlayer = null;

        public static readonly BindableProperty SoundNameProperty =
            BindableProperty.Create(nameof(SoundName), typeof(string), typeof(GlowBlockPlaySound), propertyChanged: async (b, o, n) =>
            {
                if (b is GlowBlockPlaySound bb)
                {
                    if (bb._audioPlayer != null)
                    {
                        bb._audioPlayer.Dispose();
                        bb._audioPlayer = null;
                    }

                    if (n != null)
                    {
                        bb._audioPlayer = AudioManager.Current.CreateAsyncPlayer(await FileSystem.OpenAppPackageFileAsync((string)n));
                    }
                }
            });

        public string SoundName
        {
            get => (string)GetValue(SoundNameProperty);
            set => SetValue(SoundNameProperty, value);
        }

        protected override void OnAttachedTo(GlowBlock control)
        {
            control.PropertyChanged += Control_PropertyChanged;

            base.OnAttachedTo(control);
        }

        protected override void OnDetachingFrom(GlowBlock control)
        {
            control.PropertyChanged -= Control_PropertyChanged;

            if (_audioPlayer != null)
            {
                _audioPlayer.Dispose();
                _audioPlayer = null;
            }

            base.OnDetachingFrom(control);
        }

        private async void Control_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(GlowBlock.IsLit))
            {
                if (sender is GlowBlock s)
                {
                    if (s.IsLit)
                    {
                        if (_audioPlayer != null)
                        {
                            if(_audioPlayer.IsPlaying)
                            {
                                _audioPlayer.Stop();
                            }

                            await _audioPlayer.PlayAsync(CancellationToken.None);
                        }
                    }
                }
            }           
        }
    }
}
