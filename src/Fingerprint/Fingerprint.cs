using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Fingerprint
{
	public class Fingerprint
	{
		/// <summary>
		/// Adds a cache busting url for the file requested.
		///
		/// NOTE - In order for this to work, the following rewrite rule must exist in the web.config.
		///
		/// 	
		/// 	<system.webServer>
		/// 	  <rewrite>
		/// 		<rules>
		/// 		  <rule name="fingerprint" >
		/// 			<match url="([\S]+)(/v-[a-z0-9]+/)([\S]+)" />
		/// 			<action type="Rewrite" url="{R:1}/{R:3}" />
		/// 		  </rule>
		/// 		</rules>
		/// 	  </rewrite>
		/// 	</system.webServer>
		/// 												  
		///
		/// </summary>
		/// <param name="rootRelativePath"></param>
		/// <returns></returns>
		public static string Tag(string rootRelativePath)
		{
			// Get absolute file path
			string absolute = HostingEnvironment.MapPath("~" + rootRelativePath);

			if (string.IsNullOrWhiteSpace(absolute)) { return null; }

			// Check cache for path
			if (HttpRuntime.Cache[rootRelativePath] == null)
			{
				// Open file
				using (FileStream fs = new FileStream(absolute, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					// Create a hash out of the file strem
					string hash = HashFile(fs);
					int index = rootRelativePath.LastIndexOf('/');

					// Create new url using hash
					string result = rootRelativePath.Insert(index, "/v-" + hash);
					HttpRuntime.Cache.Insert(rootRelativePath, result, new CacheDependency(absolute));
				}
			}

			return HttpRuntime.Cache[rootRelativePath] as string;
		}

		private static string HashFile(FileStream stream)
		{
			StringBuilder sb = new StringBuilder();

			if (stream != null)
			{
				// Ensure stream is at the beginning
				stream.Seek(0, SeekOrigin.Begin);

				// Create the hash
				MD5 md5 = MD5.Create();
				byte[] hash = md5.ComputeHash(stream);
				foreach (byte b in hash)
					sb.Append(b.ToString("x2"));

				stream.Seek(0, SeekOrigin.Begin);
			}

			return sb.ToString();
		}
	}
}
