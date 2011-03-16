using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using ECollegeAPI.Model.Boilerplate;

namespace ECollegeAPI.Model
{
    public class DropboxBasket
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public List<BasketItemLink> Links { get; set; }
    }
}
