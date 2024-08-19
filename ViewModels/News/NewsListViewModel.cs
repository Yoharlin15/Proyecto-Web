using Proyecto_Web.Models;

namespace Proyecto_Web.ViewModels.News
{
    public class NewsListViewModel
    {
        public string Search { get; set; }
        public List<YohaNews> News { get; set; }
        public Pagination Pagination { get; set; }
        public string CreatorId { get; set; }

    }
}
