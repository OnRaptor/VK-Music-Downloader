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
        public VkAudio selectedAudio { get; set; }

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
            VK.Authorize("b583587550bddabb4d032bd30edc46442cdb4598737df47f45c3adf0caf44baf467a05b8f8f309870f202");
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

        public ICommand ShowPreview
        {
            get
            {
                return new DelegateCommand<string>((url) =>
                {
                    Image preview = new Image();
                    BitmapImage image;
                    image = new BitmapImage();

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
                    filename = dialog.SelectedPath + $"\\{selectedAudio.Title} - {selectedAudio.Author}.mp3";

                    web.DownloadFileAsync(new Uri(selectedAudio.Url), filename); //audio                    
                });

            }
        }
    }
}
