﻿using System;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Titanium.Web.Proxy.IntegrationTests
{
    [TestClass]
    public class SslTests
    {
        //[TestMethod]
        //disable this test until CI is prepared to handle
        public void TestSsl()
        {
            // expand this to stress test to find
            // why in long run proxy becomes unresponsive as per issue #184
            string testUrl = "https://google.com";
            int proxyPort = 8086;
            var proxy = new ProxyTestController();
            proxy.StartProxy(proxyPort);

            using (var client = CreateHttpClient(testUrl, proxyPort))
            {
                var response = client.GetAsync(new Uri(testUrl)).Result;
            }
        }

        private HttpClient CreateHttpClient(string url, int localProxyPort)
        {
            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy($"http://localhost:{localProxyPort}", false),
                UseProxy = true
            };

            var client = new HttpClient(handler);

            return client;
        }
    }
}
