
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Loader.Proxy
{
    public class FtpProxy
    {
        private string host;
        private int port;
        private string userName;
        private string pwd;

        public FtpProxy(string host, int port, string userName, string pwd)
        {
            if (!host.StartsWith("ftp://"))
                host = "ftp://" + host;
            this.host = host;
            this.port = port;
            this.userName = userName;
            this.pwd = pwd;
        }


        //public void Connect()
        //{
            
        //}

        public void Download(string remotePath, string localPath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host + ":" + port + "/" + remotePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(userName, pwd);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            using (StreamWriter writer = new StreamWriter(localPath))
            {
                writer.Write(reader);
            }
            
            Console.WriteLine("Download Complete, status {0}", response.StatusDescription);

            reader.Close();
            response.Close();
        }

        public byte[] Download(string remotePath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host + ":" + port + "/" + remotePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(userName, pwd);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            var bytes = default(byte[]);
            using (var memstream = new MemoryStream())
            {
                var buffer = new byte[1024];
                var bytesRead = default(int);
                while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                    memstream.Write(buffer, 0, bytesRead);
                bytes = memstream.ToArray();
            }

            reader.Close();
            response.Close();

            return bytes;
        }

        public void Upload(string remotePath, byte[] data)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host + ":" + port + "/" + remotePath);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(userName, pwd);
            
            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            
            response.Close();
        }

        
    }
}
