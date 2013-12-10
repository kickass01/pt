using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PinkTravel.Helper
{
	public class FilesStatus
	{
		public string name { get; set; }
		public string cropped_image_name { get; set; }
		public int cropped_image_size { get; set; }
		public string type { get; set; }
		public int size { get; set; }
		public string progress { get; set; }
		public string url { get; set; }
		public string thumbnail_url { get; set; }
		public string delete_url { get; set; }
		public string delete_type { get; set; }
		public string error { get; set; }
	}
}