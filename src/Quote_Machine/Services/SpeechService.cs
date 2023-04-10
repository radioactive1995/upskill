using Microsoft.CognitiveServices.Speech;

namespace Quote_Machine.Services;

public interface ISpeechService
{
    Task TextToAudio(string content, string language);
}

public class SpeechService : ISpeechService
{
    public async Task TextToAudio(string content, string language)
    {
        Console.WriteLine("Running speechService!");
        var config = SpeechConfig.FromSubscription("38830a593e654f93811c1e23624f8a4e", "norwayeast");
        config.SpeechSynthesisLanguage = language;
        config.SpeechSynthesisVoiceName = "en-US-JennyNeural";

        using var synthesizer = new SpeechSynthesizer(config);

        var result = await synthesizer.SpeakTextAsync(content);

        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
        {
            Console.WriteLine("Finished speechService!");
        }
    }
}
