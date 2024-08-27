using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Podly.FeedParser.Xml;

namespace Podly.FeedParser
{
    public partial class HttpFeedFactory : BaseFeedFactory
    {

        public HttpFeedFactory()
            : this(new LinqFeedXmlParser())
        { }

        public HttpFeedFactory(IFeedXmlParser parser)
            : this(parser, null)
        { }

        public HttpFeedFactory(IFeedInstanceProvider instanceProvider)
            : this(new LinqFeedXmlParser(), instanceProvider)
        { }

        public HttpFeedFactory(IFeedXmlParser parser, IFeedInstanceProvider instanceProvider)
            : base(parser, instanceProvider)
        { }

        public override async Task<bool> PingFeed(Uri feedUri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(feedUri);
                    return IsValidXmlReponse(response);
                }
            }
            catch (HttpRequestException)
            {
                // Usually this means we encountered a 404 / 501 error of some sort.
                return false;
            }
        }

        public override async Task<string> DownloadXml(Uri feedUri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(feedUri);
                    response.EnsureSuccessStatusCode();

                    string responseXml = await response.Content.ReadAsStringAsync();
                    return responseXml;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new MissingFeedException($"Was unable to open web-hosted file {feedUri.LocalPath}", ex);
            }
        }

        private static string GetResponseXml(HttpWebResponse response)
        {
            var reader = new StreamReader(response.GetResponseStream());
            var responseXml = reader.ReadToEnd();
            return responseXml;
        }

        private static bool IsValidXmlReponse(HttpResponseMessage response)
        {
            return response != null &&
                   response.StatusCode == HttpStatusCode.OK &&
                   response.Content.Headers.ContentType.MediaType.Contains("xml");
        }
    }
}
