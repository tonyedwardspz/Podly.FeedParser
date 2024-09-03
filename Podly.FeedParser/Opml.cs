using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace Podly.FeedParser {
    public class Opml {
        ///<summary>
        /// Version of OPML
        ///</summary>
        public string Version { get; set;}

        ///<summary>
        /// Encoding of OPML
        ///</summary>
        public string Encoding { get; set;}

        ///<summary>
        /// Head of OPML
        ///</summary>
        public Head Head { get; set;} = new Head();

        ///<summary>
        /// Body of OPML
        ///</summary>        
        public Body Body { get; set;} = new Body();

        ///<summary>
        /// Constructor
        ///</summary>
        public Opml() 
        {

        }

        ///<summary>
        /// Constructor
        ///</summary>
        /// <param name="location">Location of the OPML file</param>
        public Opml(string location) 
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(location);
                readOpmlNodes(doc);
            } catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
            
        }

        ///<summary>
        /// Constructor
        ///</summary>
        /// <param name="location">Location of the OPML file</param>
        /// <param name="outputFilePath">The output filepath to save the OPML file to</param>
        // public Opml(string inputFilePath, string outputFilePath) 
        // {
        //     if (File.Exists(outputFilePath))
		// 	    File.Delete(outputFilePath);

            
        //     // if the file is on the file system, copy it to the output file path
        //     if (File.Exists(inputFilePath))
        //     {
        //         File.WriteAllText(outputFilePath, File.ReadAllText(inputFilePath));
        //         inputFilePath = outputFilePath;
        //     } else {
        //         // check if the input file path was a url, and download the file using a http client if it is
        //         if (inputFilePath.StartsWith("http"))
        //         {
        //             using (var client = new HttpClient())
        //             {
        //                 using (var response = client.GetAsync(inputFilePath).Result)
        //                 {
        //                     using (var stream = response.Content.ReadAsStreamAsync().Result)
        //                     {
        //                         using (var destStream = File.Create(outputFilePath))
        //                         {
        //                             stream.CopyTo(destStream);
        //                         }
        //                     }
        //                 }
        //             }
        //             inputFilePath = outputFilePath;
        //         }
        //     }

        //     XmlDocument doc = new XmlDocument();
        //     doc.Load(inputFilePath);
        //     readOpmlNodes(doc);
        // }

        ///<summary>
        /// Constructor
        ///</summary>
        /// <param name="doc">XMLDocument of the OPML</param>
        public Opml(XmlDocument doc) 
        {
            readOpmlNodes(doc);
        }      


        private void readOpmlNodes(XmlDocument doc) {
            foreach (XmlNode nodes in doc) 
            {
                if (nodes.Name.Equals("opml", StringComparison.CurrentCultureIgnoreCase)) 
                {
                    foreach (XmlNode childNode in nodes)
                    {

                        if (childNode.Name.Equals("head", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Head = new Head((XmlElement) childNode);
                        }

                        if (childNode.Name.Equals("body", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Body = new Body((XmlElement) childNode);
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();
            String ecoding = string.IsNullOrEmpty(Encoding)?"UTF-8":Encoding;
            buf.Append($"<?xml version=\"1.0\" encoding=\"{ecoding}\" ?>\r\n");
            String version = string.IsNullOrEmpty(Version)?"2.0":Version;
            buf.Append($"<opml version=\"{version}\">\r\n");
            buf.Append(Head.ToString());
            buf.Append(Body.ToString());
            buf.Append("</opml>");

            return buf.ToString();
        }

    }
}