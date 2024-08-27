using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

#if WINDOWS_PHONE
using QDFeedParser.Extensions;
#endif

namespace Podly.FeedParser
{
    public partial class HttpFeedFactory
    {
        // public override async Task<bool> PingFeedAsync(Uri feeduri)
        // {
        //     try
        //     {
        //         var request = WebRequest.Create(feeduri) as HttpWebRequest;
        //         var response = await request.GetResponseAsync() as HttpWebResponse;

        //         return IsValidXmlReponse(response);
        //     }
        //     catch (WebException)
        //     {
        //         return false;
        //     }
        // }

        public override async Task<bool> PingFeedAsync(Uri feedUri)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Send a HEAD request to check if the resource exists.
                    var request = new HttpRequestMessage(HttpMethod.Head, feedUri);
                    var response = await httpClient.SendAsync(request);

                    return IsValidXmlResponse(response);
                }
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        private bool IsValidXmlResponse(HttpResponseMessage response)
        {
            return response != null &&
                   response.StatusCode == HttpStatusCode.OK &&
                   response.Content.Headers.ContentType.MediaType.Contains("xml");
        }

        // private static bool IsValidXmlReponse(HttpResponseMesssage response)
        // {
        //     return response != null &&
        //            response.StatusCode == HttpStatusCode.OK &&
        //            response.ContentType.Contains("xml");
        // }

        public override async Task<string> DownloadXmlAsync(Uri feedUri)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Send a GET request to download the XML content.
                    var response = await httpClient.GetAsync(feedUri);
                    response.EnsureSuccessStatusCode(); // Throws if the status code is not 2xx

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                throw new MissingFeedException($"Was unable to open web-hosted file {feedUri.LocalPath}", ex);
            }
    }

        // public override async Task<string> DownloadXmlAsync(Uri feeduri)
        // {
        //     try
        //     {
        //         var request = WebRequest.Create(feeduri) as HttpWebRequest;

        //         using (var response = await request.GetResponseAsync() as HttpWebResponse)
        //         {
        //             return GetResponseXml(response);
        //         }
        //     }
        //     /* Usually this means we encountered a 404 / 501 error of some sort. */
        //     catch (WebException ex)
        //     {
        //         throw new MissingFeedException(string.Format("Was unable to open web-hosted file {0}", feeduri.LocalPath), ex);
        //     }
        // }
    }
}
