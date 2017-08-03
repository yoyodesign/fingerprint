using System.Xml;

namespace umbraco.interfaces
{
	/// <summary>
	/// Taken from https://github.com/umbraco/Umbraco-CMS - Thanks
	/// </summary>

	public interface IPackageAction : IDiscoverable
    {
		bool Execute(string packageName, XmlNode xmlData);
        string Alias();
        bool Undo(string packageName, XmlNode xmlData);
        XmlNode SampleXml();
    }
}
