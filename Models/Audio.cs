using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Net;
using System.Windows;

namespace MVVM_Base.Models
{
    public class VkAudio : VK
    {
        public string Title { get; set; }
        public bool IsCorrect
        {
            get { return String.IsNullOrWhiteSpace(Url); }
        }
        public string Author { get; set; }
        public string Duration { get; set; }
        public int DurationSeconds { get {
                TimeSpan.TryParse(Duration, out TimeSpan tick);
                return tick.Seconds;
            } set { Duration = value.ToString(@"mm\:ss"); } }
        public string ThumbUrl { get; set; }
        public string ThumbUrlFull { get; set; }
        public string Url { get; set; }

        public string file
        {
            get
            {
                MessageBox.Show("DownloadStarted");
                WebClient web = new WebClient();
                string path = $"{Directory.GetCurrentDirectory()}\\temp\\audio.mp3";
                web.DownloadFileAsync(new Uri(Url), path);
                return path;
            }
        }

        public BitmapImage Thumb
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ThumbUrl))
                    ThumbUrl = "https://dummyimage.com/68x68/292723/fff.jpg&text=No+Preview";

                BitmapImage image;
                image = new BitmapImage();

                image.BeginInit();
                image.UriSource = new Uri(ThumbUrl);
                image.EndInit();

                return image;

            }
            set { Thumb = value; }
        }

    }
}
