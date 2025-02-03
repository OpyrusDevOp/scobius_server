namespace Scobius.Services;

public class MediaService(IConfiguration config)
{

    public void SaveProfilePicture(string fileData)
    {
        var path = config[""];
    }

    public void SaveVoiceMessage(string voiceContent)
    {

    }

}
