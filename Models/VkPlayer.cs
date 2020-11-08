using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVVM_Base.Models
{
    public class VkPlayer : MediaElement
    {
        private bool _IsPause = false;
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
        public Image preview { get; set; }
        public ContentControl content { get; set; }
        public double PositionSecs
        {
            get
            {
                return Position.TotalSeconds;
            }
            set {
                Position = TimeSpan.FromSeconds(PositionSecs);
            }
        }

        public VkPlayer()
        {
            LoadedBehavior = MediaState.Manual;
            UnloadedBehavior = MediaState.Manual;
            MediaOpened += (s, e) => {
                Position = TimeSpan.FromSeconds(0);
                IsPause = false;
                Play();
            };
            MediaEnded += (s, e) =>
            {
                Position = TimeSpan.FromSeconds(0);
                IsPause = true;
            };

            MediaFailed += (s, e) =>
              {
                  MessageBox.Show(e.ErrorException.ToString());
              };
        }
    }
}
