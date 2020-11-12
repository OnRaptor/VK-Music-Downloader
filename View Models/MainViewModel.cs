using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using MVVM_Base.View_Models;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;
using MVVM_Base.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace MVVM_Base.View_Models
{
    public class MainViewModel : ViewModelBase
    {
        public List<VkAudio> Audios { get; set; }

        VkAudio _selectedAudio;
        public VkAudio selectedAudio
        {
            get => _selectedAudio;
            set
            {
                if (value != null)
                    _selectedAudio = value;
                else return;
            }
        }

        string _SearchText { get; set; }
        public string SearchText {get => _SearchText; set
            {
                _SearchText = value;
                if (string.IsNullOrEmpty(_SearchText)) return;
                Search.Execute(_SearchText);
            } }

        WebClient web = new WebClient();
        public MainViewModel()
        {
            VK.Authorize("f6cc9a0093103e8624c888fc7dd37a36d2eb1b6695a4998a5f13efe5cd78fb1f332e28e9062565194975f");
            Task.Run(() =>
            {
                GetMyMusic.Execute(null);
            });
        }

        

        public ICommand GetRecomendations
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Audios = VK.GetUserRecomendations();
                });

            }
        }

        public ICommand GetMyMusic
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Audios = VK.GetUserAudios();
                });

            }
        }

        public ICommand Search
        {
            get
            {
                return new DelegateCommand<string>((name) =>
                {
                    Audios = VK.SearchMusic(name);                    
                }); 

            }
        }

        public ICommand SearchAuthor
        {
            get
            {
                return new DelegateCommand<string>((name) =>
                {
                    Audios = VK.SearchAuthor(name);
                });

            }
        }

        public ICommand ShowPreview
        {
            get
            {
                return new DelegateCommand<string>((url) =>
                {
                    Image preview = new Image();
                    BitmapImage image;
                    image = new BitmapImage();
                    if (url == null) return;
                    image.BeginInit();
                    image.UriSource = new Uri(url);
                    image.EndInit();

                    preview.Source = image;

                    var menu = new System.Windows.Controls.ContextMenu(); menu.Background = new SolidColorBrush(Colors.Black);
                    var item = new System.Windows.Controls.MenuItem(); item.Header = "Открыть"; item.Background = new SolidColorBrush(Colors.Black);
                    item.Click += async (sender, e) => {
                        await Task.Run(() => System.Diagnostics.Process.Start(url));
                    };
                    menu.ItemsSource = new[] {item };
                    preview.ContextMenu = menu;

                    var window = new Window();
                    window.Content = preview;
                    window.ResizeMode = ResizeMode.CanMinimize;
                    window.SizeToContent = SizeToContent.Width;
                    window.Show();
                });

            }
        }

        public ICommand Download
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    string filename;
                    System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                    dialog.ShowDialog();

                    if (selectedAudio.Author == null)
                        selectedAudio.Author = "Unknown";

                    filename = dialog.SelectedPath + $"\\{selectedAudio.Title} - {selectedAudio.Author}.mp3";

                    web.DownloadFileAsync(new Uri(selectedAudio.Url), filename); //audio                    
                });

            }
        }
    }
}
