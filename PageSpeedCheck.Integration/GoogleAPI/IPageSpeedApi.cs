using System.Threading.Tasks;
using PageSpeedCheck.Model.Dto;

namespace PageSpeedCheck.Integration.GoogleAPI
{
    public interface IPageSpeedApi
    {
        Task<PageSpeedDto> GetPageSpeedAnalyticsAsync(string url);
    }
}