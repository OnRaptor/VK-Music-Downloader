using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.AudioBypassService.Extensions;
using System.Windows.Media.Imaging;

namespace MVVM_Base.Models
{
    public class VK
    {
        static VkApi api;
        public static void Authorize(string access_token)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddAudioBypass();
            api = new VkApi();

            api.Authorize(new ApiAuthParams { AccessToken = access_token });
        }

        public static string M3U8ToMp3(string url)
        {
            try
            {
                int ind = url.IndexOf("/index.m3u8");
                url = url.Replace("/index.m3u8", ".mp3");
                int firstindex = url.LastIndexOf('/', ind);
                int secondindex = url.LastIndexOf('/', firstindex - 1);
                url = url.Remove(secondindex, firstindex - secondindex);
            }
            catch { }
            return url;
        }
        static VkAudio VkAudioTo(Audio audio)
        {
            return new VkAudio { Author = audio.Artist, Duration = TimeSpan.FromSeconds(audio.Duration).ToString(@"mm\:ss"), DurationSeconds = audio.Duration, Title = audio.Title, Url = M3U8ToMp3(audio.Url?.AbsoluteUri)?.Replace("https", "http") , ThumbUrl = audio.Album?.Thumb?.Photo68, ThumbUrlFull = audio.Album?.Thumb?.Photo600};
        }

        public static List<VkAudio> GetUserAudios()
        {
            List<VkAudio> audios = new List<VkAudio>();
            foreach (var audio in api.Audio.Get(new VkNet.Model.RequestParams.AudioGetParams { OwnerId = 324943294}))
                audios.Add(VkAudioTo(audio));
           
            return audios.ToList();
        }

        public static List<VkAudio> GetUserRecomendations()
        {
            List<VkAudio> audios = new List<VkAudio>();
            foreach (var audio in api.Audio.GetRecommendations(null, 324943294))
                audios.Add(VkAudioTo(audio));

            return audios.ToList();
        }

        public static List<VkAudio> SearchMusic(string name)
        {
            List<VkAudio> audios = new List<VkAudio>();
            foreach (var audio in api.Audio.Search(new VkNet.Model.RequestParams.AudioSearchParams
            {
                Autocomplete = true,
                SearchOwn = true,
                Query = name
            }))
                audios.Add(VkAudioTo(audio));

            return audios.ToList();
        }

        public static List<VkAudio> SearchAuthor(string name)
        {
            List<VkAudio> audios = new List<VkAudio>();
            foreach (var audio in api.Audio.Search(new VkNet.Model.RequestParams.AudioSearchParams
            {
                Autocomplete = true,
                SearchOwn = true,
                Query = name,
                PerformerOnly = true
            }))
                audios.Add(VkAudioTo(audio));

            return audios.ToList();
        }
    }
}
