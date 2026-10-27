namespace Structural
{
    sealed class Amplifier
    {
        public void On() => Console.WriteLine("Amplifier: Powering on.");
        public void Off() => Console.WriteLine("Amplifier: Shutting down.");
        public void SetVolume(int level) => Console.WriteLine($"Amplifier: Volume set to {level}.");
    }

    sealed class Projector
    {
        public void On() => Console.WriteLine("Projector: Warming up.");
        public void Off() => Console.WriteLine("Projector: Cooling down.");
        public void WideScreenMode() => Console.WriteLine("Projector: Widescreen mode enabled.");
    }

    sealed class SmartLights
    {
        public void Dim(int level) => Console.WriteLine($"Lights: Dimmed to {level}%.");
        public void On() => Console.WriteLine("Lights: Full brightness.");
    }

    sealed class StreamingService
    {
        public void Connect() => Console.WriteLine("Streaming: Connected to service.");
        public void Disconnect() => Console.WriteLine("Streaming: Disconnected.");
        public void Stream(string movie) => Console.WriteLine($"Streaming: Now streaming '{movie}'.");
    }

    sealed class HomeTheatreFacade
    {
        private readonly Amplifier _amp;
        private readonly Projector _projector;
        private readonly SmartLights _lights;
        private readonly StreamingService _streaming;

        public HomeTheatreFacade(Amplifier amp, Projector projector, SmartLights lights, StreamingService streaming)
        {
            _amp = amp;
            _projector = projector;
            _lights = lights;
            _streaming = streaming;
        }

        public void WatchMovie(string movie)
        {
            Console.WriteLine($"\n--- Preparing to watch: {movie} ---");
            _lights.Dim(15);
            _projector.On();
            _projector.WideScreenMode();
            _amp.On();
            _amp.SetVolume(20);
            _streaming.Connect();
            _streaming.Stream(movie);
            Console.WriteLine("--- Enjoy the movie! ---\n");
        }

        public void EndMovie()
        {
            Console.WriteLine("\n--- Shutting down home theater ---");
            _streaming.Disconnect();
            _amp.Off();
            _projector.Off();
            _lights.On();
            Console.WriteLine("--- Home theater off ---\n");
        }
    }

    //public class Facade
    //{
    //    public static void Main()
    //    {
    //        var amp = new Amplifier();
    //        var projector = new Projector();
    //        var lights = new SmartLights();
    //        var streaming = new StreamingService();

    //        var theater = new HomeTheatreFacade(amp, projector, lights, streaming);
            
    //        theater.WatchMovie("Interstellar");

    //        theater.EndMovie();
    //    }
    //}
}
