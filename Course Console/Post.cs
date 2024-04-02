using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Console
{
    internal class Post
    {
        private static int _currentPostId;

        protected int ID { get; set; }
        protected string Title { get; set; }
        protected string SendByUsername { get; set; }
        protected bool IsPublic { get; set; }

        public Post(string title, string sendByUsername, bool isPublic)
        {
            ID = CreateID();
            Title = title;
            SendByUsername = sendByUsername;
            IsPublic = isPublic;
        }

        private static int CreateID()
        {
            return ++_currentPostId;
        }

        public virtual void Update(string title, bool isPublic)
        {
            Title = title;
            IsPublic = isPublic;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} - {2}", ID, Title, SendByUsername);
        }
    }

    internal class ImagePost : Post
    {
        public int ImageURL { get; set; }

        public ImagePost(string title, string sendByUsername, bool isPublic, int imageURL) : base(title, sendByUsername, isPublic)
        {
            ImageURL = imageURL;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} - {2} - by {3}", ID, Title, ImageURL, SendByUsername);
        }
    }

    internal class VideoPost : Post
    {
        public string VideoURL { get; set; }
        public int Length { get; set; }
        private Stopwatch _timer;
        private bool _isRunning;


        public VideoPost(string title, string sendByUsername, bool isPublic, string videoURL, int length, int timer) : base(title, sendByUsername, isPublic)
        {
            VideoURL = videoURL;
            Length = length;
            _timer = new Stopwatch();
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} - {2} - {3} - by {4}", ID, Title, VideoURL, Length, SendByUsername);
        }

        public void Play()
        {
            if (!_isRunning)
            {
                _timer.Start();
                Console.WriteLine("Video started Plaing at {0} seconds!", _timer.ElapsedMilliseconds / 1000);
                _isRunning = true;
            }
        }

        public void Stop()
        {
            if (_isRunning)
            {
                _timer.Stop();
                if (_timer.ElapsedMilliseconds >= Length * 1000)
                {
                    Console.WriteLine("Video already finnished!");
                    _timer.Reset();
                }
                else
                {
                    Console.WriteLine("Video stoped at {0} seconds!", _timer.ElapsedMilliseconds / 1000);
                }
                _isRunning = false;
            }
        }

        public void Reset()
        {
            _timer.Reset();
        }

        public void Update(string title, bool isPublic, string videoURL, int length)
        {
            base.Update(title, isPublic);
            VideoURL = videoURL;
            Length = length;
        }
    }
}
