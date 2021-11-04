﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess.Models
{
	public class Address
	{
		public int Id { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
	}
}
