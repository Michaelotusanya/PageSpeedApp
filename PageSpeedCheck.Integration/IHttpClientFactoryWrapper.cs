using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PageSpeedCheck.Integration
{
    public interface IHttpClientFactoryWrapper
    {
        Task<string> GetStringAsync(string url);
    }
}
