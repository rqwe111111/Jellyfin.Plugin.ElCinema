using Jellyfin.Plugin.ElCinema.Services;using MediaBrowser.Controller.Entities;using MediaBrowser.Controller.Entities.Movies;using MediaBrowser.Controller.Entities.TV;using MediaBrowser.Controller.Providers;using MediaBrowser.Model.Entities;using MediaBrowser.Model.Providers;
namespace Jellyfin.Plugin.ElCinema.Providers;
public sealed class ElCinemaImageProvider(ElCinemaClient client):IRemoteImageProvider,IHasOrder
{
 public string Name=>Constants.ProviderName;public int Order=>-5;
 public bool Supports(BaseItem item)=>(Plugin.Instance?.Configuration.EnableImages??true)&&item is Movie or Series or Season or Episode or Person;
 public IEnumerable<ImageType> GetSupportedImages(BaseItem item)=>item is Episode?[ImageType.Primary]:[ImageType.Primary,ImageType.Backdrop];
 public async Task<IEnumerable<RemoteImageInfo>> GetImages(BaseItem item,CancellationToken ct)
 {
  var id=item.GetProviderId(Constants.ProviderId);if(string.IsNullOrWhiteSpace(id))return [];IReadOnlyList<string> urls=[];
  if(item is Person)urls=await client.GetPersonImagesAsync(id,ct);
  else if(item is Episode&&ProviderIds.TryEpisode(id,out var ew,out var ee)){var ep=await client.GetEpisodeAsync(ew,ee,item.ParentIndexNumber,item.IndexNumber,ct);if(!string.IsNullOrWhiteSpace(ep?.ImageUrl))urls=[ep.ImageUrl];}
  else {var work=id;if(item is Season&&ProviderIds.TrySeason(id,out var sw,out _))work=sw;if(work.All(char.IsDigit))urls=await client.GetWorkImagesAsync(work,ct);}
  return urls.Select((u,n)=>new RemoteImageInfo{ProviderName=Name,Url=u,ThumbnailUrl=u,Type=n==0?ImageType.Primary:ImageType.Backdrop,Language=Constants.Language}).ToList();
 }
 public Task<HttpResponseMessage> GetImageResponse(string u,CancellationToken ct)=>client.GetImageResponseAsync(u,ct);
}
