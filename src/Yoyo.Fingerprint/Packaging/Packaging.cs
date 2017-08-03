using Microsoft.Web.XmlTransform;
using System.Web;
using System.Xml;
using umbraco.interfaces;

namespace Yoyo.Fingerprint.Packaging
{
	// Courtesy of http://web.archive.org/web/20151017043514/http://www.nibble.be/?p=461
	public class PackageActions
	{
		public class TransformConfig : IPackageAction
		{
			public string Alias()
			{
				return "YoyoFingerprint";
			}

			private bool Transform(string packageName, XmlNode xmlData, bool uninstall = false)
			{
				//The config file we want to modify
				var file = xmlData.Attributes.GetNamedItem("file").Value;

				string sourceDocFileName = VirtualPathUtility.ToAbsolute(file);

				//The xdt file used for tranformation
				var fileEnd = "install.xdt";
				if (uninstall)
				{
					fileEnd = string.Format("un{0}", fileEnd);
				}

				var xdtfile = string.Format("{0}.{1}", xmlData.Attributes.GetNamedItem("xdtfile").Value, fileEnd);
				string xdtFileName = VirtualPathUtility.ToAbsolute(xdtfile);

				// The translation at-hand
				using (var xmlDoc = new XmlTransformableDocument())
				{
					xmlDoc.PreserveWhitespace = true;
					xmlDoc.Load(HttpContext.Current.Server.MapPath(sourceDocFileName));

					using (var xmlTrans = new XmlTransformation(HttpContext.Current.Server.MapPath(xdtFileName)))
					{
						if (xmlTrans.Apply(xmlDoc))
						{
							// If we made it here, sourceDoc now has transDoc's changes
							// applied. So, we're going to save the final result off to
							// destDoc.
							xmlDoc.Save(HttpContext.Current.Server.MapPath(sourceDocFileName));
						}
					}
				}
				return true;
			}

			public bool Execute(string packageName, XmlNode xmlData)
			{
				return Transform(packageName, xmlData);
			}

			public bool Undo(string packageName, XmlNode xmlData)
			{
				return Transform(packageName, xmlData, true);
			}

			public XmlNode SampleXml()
			{
				string str = "<Action runat=\"install\" undo=\"false\" alias=\"Yoyo.Fingerprint.TransformConfig\" file=\"~/web.config\" xdtfile=\"~/app_plugins/yoyo.fingerprint/config/temp/web.config\"></Action>";
				return ParseStringToXmlNode(str);
			}

			private static XmlNode ParseStringToXmlNode(string value)
			{
				var xmlDocument = new XmlDocument();
				var xmlNode = AddTextNode(xmlDocument, "error", "");

				try
				{
					xmlDocument.LoadXml(value);
					return xmlDocument.SelectSingleNode(".");
				}
				catch
				{
					return xmlNode;
				}
			}

			private static XmlNode AddTextNode(XmlDocument xmlDocument, string name, string value)
			{
				var node = xmlDocument.CreateNode(XmlNodeType.Element, name, "");
				node.AppendChild(xmlDocument.CreateTextNode(value));
				return node;
			}
		}
	}
}