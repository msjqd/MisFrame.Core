using System;
using System.Collections.Generic;
using System.Text;

namespace MisFrame.Core.Model
{
    
    public class Advertisement
    {
		public int Id { get; set; }

        public String ImgUrl { get; set; }

        public String Title { get; set; }

		public String Url { get; set; }

        public String Remark { get; set; }

        public DateTime CreateDate { get; set; }
	}
}
