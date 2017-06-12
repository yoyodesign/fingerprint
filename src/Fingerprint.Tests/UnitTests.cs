using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Fingerprint.Tests
{
	[TestClass]
	public class FingerprintTests
	{
		[TestMethod]
		public void NullTest()
		{
			string nullFingerpint = null;
			string expected = null;
			string actual = Fingerprint.Tag(nullFingerpint);

			Assert.AreEqual(expected, actual, "Fingerprint should return null value as null");
		}

		[TestMethod]
		public void MissingFileTest()
		{
			string filePath = "assets/missingfile.css";
			string expected = null;
			string actual = Fingerprint.Tag(filePath);
			Debug.WriteLine(actual);
			Assert.AreEqual(expected, actual, "Fingerprint should return missing file value as null");
		}


		// TODO - Add unit tests that can handle HostingEnvironment.MapPath()

		//[TestMethod]
		//public void CssFileTest()
		//{
		//	string filePath = "/assets/style.css";
		//	string expected = "/assets/[ADD EXPECTED HASH HERE]/style.css";
		//	string actual = Fingerprint.Tag(filePath);

		//	Assert.AreEqual(expected, actual, "CSS Fingerprint should return hashed filepath");
		//}

		//[TestMethod]
		//public void JsFileTest()
		//{
		//	string filePath = "assets/script.js";
		//	string expected = "assets/[ADD EXPECTED HASH HERE]/script.js";
		//	string actual = Fingerprint.Tag(filePath);

		//	Assert.AreEqual(expected, actual, "JavaScript Fingerprint should return hashed filepath");
		//}


		//[TestMethod]
		//public void JpgFileTest()
		//{
		//	string filePath = "assets/image.jpg";
		//	string expected = "assets/[ADD EXPECTED HASH HERE]/image.jpg";
		//	string actual = Fingerprint.Tag(filePath);

		//	Assert.AreEqual(expected, actual, "Jpg image Fingerprint should return hashed filepath");
		//}

		//[TestMethod]
		//public void PngFileTest()
		//{
		//	string filePath = "assets/image.png";
		//	string expected = "assets/[ADD EXPECTED HASH HERE]/image.png";
		//	string actual = Fingerprint.Tag(filePath);

		//	Assert.AreEqual(expected, actual, "Png image Fingerprint should return hashed filepath");
		//}
	}
}
