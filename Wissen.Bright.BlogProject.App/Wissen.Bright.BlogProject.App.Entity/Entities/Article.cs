using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wissen.Bright.BlogProject.App.Entity.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string PictureUrl { get; set; }

        public int CategoryId { get; set; }
        public int UserId { get; set; }

        //Navigation Property
        public virtual Category Category { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Tag> Tags { get; set; }
    }
}
