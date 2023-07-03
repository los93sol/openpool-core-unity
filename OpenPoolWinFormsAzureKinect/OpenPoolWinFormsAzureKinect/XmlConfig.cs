using System.IO;
using System.Xml.Serialization;

namespace OpenPoolWinFormsAzureKinect;

public class XmlConfig
{
    private string configFile;

    public XmlConfig(string filename)
    {
        this.configFile = filename;
    }

    public AppConfig Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AppConfig));
        AppConfig ret;
        using (FileStream fs = new FileStream(configFile, FileMode.Open))
        {
            ret = (AppConfig)serializer.Deserialize(fs);
        }
        return ret;
    }

    public void Save(AppConfig conf)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AppConfig));
        using (FileStream fs = new FileStream(this.configFile, FileMode.Create))
        {
            serializer.Serialize(fs, conf);
        }
    }
}
