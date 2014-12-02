using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace EApp.Common.Network
{

    public sealed class NetworkUtil
    {
        private NetworkUtil() { }

        public static IPStatus PingServerHostName(string hostNameOrAddress)
        {
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(hostNameOrAddress);
                return reply.Status;
            }
            catch (System.Net.NetworkInformation.PingException ex)
            {
                return IPStatus.TimedOut;
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                return IPStatus.TimedOut;
            }
            catch (Exception ex)
            {
                return IPStatus.Unknown;
            }
        }

        public static string GetV4IPAddress(string hostNameOrAddress)
        {
            IPAddress[] addressList = Dns.GetHostEntry(hostNameOrAddress).AddressList;
            if (addressList == null)
                return string.Empty;

            foreach (IPAddress ip in addressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip.ToString();
            }

            return string.Empty;
        }

        public static HttpStatusCode GetRequestUrlStatus(string requestUriString)
        {
            HttpWebRequest request = WebRequest.Create(requestUriString) as HttpWebRequest;
            request.Method = "GET";
            request.Credentials = CredentialCache.DefaultNetworkCredentials;

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    HttpStatusCode statusCode = response.StatusCode;
                    return statusCode;
                }
            }
            catch (System.Net.WebException ex)
            {
                using (HttpWebResponse response = ex.Response as HttpWebResponse)
                {
                    if (response != null)
                    {
                        HttpStatusCode statusCode = response.StatusCode;
                        return statusCode;
                    }
                    else
                    {
                        return HttpStatusCode.BadRequest;
                    }
                }
            }
        }

    }
}
