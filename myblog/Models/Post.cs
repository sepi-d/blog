using System.ComponentModel.DataAnnotations;
namespace myblog.Models
{
    public class Post
    {
        public Post()
        {
        }
        public int id { get; set; }
        public string title { get; set; }
        public string htmlContent { get; set; }

    }

}

