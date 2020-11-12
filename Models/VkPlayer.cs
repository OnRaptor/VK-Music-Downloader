using System;
using System.Windows;
using System.Windows.Controls;

namespace MVVM_Base.Models
{
    public class VkPlayer : MediaElement 
    {
        private bool _IsPause = false;

        public DependencyProperty PositionProperty = DependencyProperty.Register("PositionSecs", typeof(int), typeof(VkPlayer));
        public bool IsPause { get => _IsPause;
            set
            {
                _IsPause = value;
                if (_IsPause)
                    Pause();
                else
                    Play();
            }
        }
        public int PositionSecs
        {
            get
            {
                MessageBox.Show("Changed", GetValue(PositionProperty).ToString());
                return (int)GetValue(PositionProperty);

            }
            set {
                SetValue(PositionProperty, value);
                Position = TimeSpan.FromSeconds((int) GetValue(PositionProperty));
                MessageBox.Show("Changed", value.ToString());
            }
        }

        public VkPlayer()
        {
            LoadedBehavior = MediaState.Manual;
            UnloadedBehavior = MediaState.Manual;

            MediaOpened += (s, e) => {
                Position = TimeSpan.FromSeconds(0);
            };

            MediaEnded += (s, e) =>
            {
                Position = TimeSpan.FromSeconds(0);
                IsPause = false;
            };

            MediaFailed += (s, e) =>
              {
                  MessageBox.Show(e.ErrorException.ToString());
              };
        }
    }
}
