using System.Collections.Generic;
using SalahService.Model;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

namespace SalahService.Services
{
    public interface IAlAdhanService : IHealthCheck
    {
        Data GetTimings(string latitude, string longitude);
    }
}
