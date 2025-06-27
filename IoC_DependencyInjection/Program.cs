using Microsoft.Extensions.DependencyInjection;

namespace IoC_DependencyInjection
{

   interface ILogger
    {
        void Log(string message);
    }
    class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    interface IMusic
    {
        string GetGenre();
    }
    
    class ClassicMusic : IMusic
    {
        public string GetGenre()
        { return "Classical"; } // for now 
    }

    class MusicPlayer
    {
        private readonly ILogger _logger;
        private readonly IMusic  _music;

        public MusicPlayer(ILogger logger, IMusic music)
        {
            _logger = logger;
            _music = music;
        }

        public void PlayMusic()
        {
            _logger.Log($"Playin' {_music.GetGenre()} music...");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILogger, ConsoleLogger>();
            services.AddSingleton<IMusic, ClassicMusic>();
            services.AddSingleton<MusicPlayer>();

           ServiceProvider serviceProvider = services.BuildServiceProvider();
           MusicPlayer player = serviceProvider.GetRequiredService<MusicPlayer>();
            player.PlayMusic();
        }
    }
}
