using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wissen.Bright.BlogProject.App.Entity.Entities
{
    public class Tag : BaseEntity
    {
        public string Content { get; set; }

        //Navigation Property
        public virtual List<Article> Articles { get; set; }
    }
}
