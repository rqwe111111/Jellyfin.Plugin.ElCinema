using Jellyfin.Plugin.ElCinema.Models;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;

namespace Jellyfin.Plugin.ElCinema.Providers;

internal static class ProviderMapper
{
    public static void ApplyWork<T>(MetadataResult<T> result,T item,ElCinemaWork w) where T:BaseItem
    {
        item.Name=w.Title;if(!string.IsNullOrWhiteSpace(w.OriginalTitle)&&!string.Equals(w.OriginalTitle,w.Title,StringComparison.OrdinalIgnoreCase))item.OriginalTitle=w.OriginalTitle;
        item.Overview=w.Overview;item.ProductionYear=w.Year;item.PremiereDate=w.PremiereDate;item.CommunityRating=w.Rating;item.OfficialRating=w.OfficialRating;item.SetProviderId(Constants.ProviderId,w.Id);
        if(w.RuntimeMinutes is >0)item.RunTimeTicks=TimeSpan.FromMinutes(w.RuntimeMinutes.Value).Ticks;if(w.Genres.Count>0)item.Genres=w.Genres.ToArray();if(w.ProductionLocations.Count>0)item.ProductionLocations=w.ProductionLocations.ToArray();AddPeople(result,w.People);
    }

    public static void ApplyEpisode(MetadataResult<Episode> result,Episode item,ElCinemaEpisode e)
    {
        item.Name=string.IsNullOrWhiteSpace(e.Title)?$"الحلقة {e.EpisodeNumber}":e.Title;item.Overview=e.Overview;item.IndexNumber=e.EpisodeNumber;item.ParentIndexNumber=e.SeasonNumber;item.PremiereDate=e.PremiereDate;item.ProductionYear=e.Year;item.CommunityRating=e.Rating;item.SetProviderId(Constants.ProviderId,ProviderIds.Episode(e.WorkId,e.EpisodeId));if(e.RuntimeMinutes is >0)item.RunTimeTicks=TimeSpan.FromMinutes(e.RuntimeMinutes.Value).Ticks;if(e.Genres.Count>0)item.Genres=e.Genres.ToArray();AddPeople(result,e.People);
    }

    private static void AddPeople<T>(MetadataResult<T> result,IEnumerable<ElCinemaPersonCredit> people) where T:BaseItem
    {foreach(var p in people){var pi=new PersonInfo{Name=p.Name,Role=p.Role??string.Empty,Type=p.Kind};if(!string.IsNullOrWhiteSpace(p.Id))pi.SetProviderId(Constants.ProviderId,p.Id);result.AddPerson(pi);}}
}

internal static class ProviderIds
{
    public static string Season(string workId,int season)=>$"{workId}:s{season}";
    public static string Episode(string workId,string episodeId)=>$"{workId}:e{episodeId}";
    public static bool TrySeason(string? value,out string workId,out int season){workId=string.Empty;season=0;var p=(value??string.Empty).Split(":s",StringSplitOptions.None);return p.Length==2&&p[0].All(char.IsDigit)&&int.TryParse(p[1],out season)&&(workId=p[0]).Length>0;}
    public static bool TryEpisode(string? value,out string workId,out string episodeId){workId=string.Empty;episodeId=string.Empty;var p=(value??string.Empty).Split(":e",StringSplitOptions.None);if(p.Length!=2||!p[0].All(char.IsDigit)||!p[1].All(char.IsDigit))return false;workId=p[0];episodeId=p[1];return true;}
}
