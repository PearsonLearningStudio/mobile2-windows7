using System;
using System.Collections.Generic;

namespace eCollegeWP7.ViewModels.Application
{
    public class DropboxBasket
    {
        public DropboxBasket()
        {
            Messages = new List<DropboxBasketMessage>();
        }

        public string ID { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string BasketLink { get; set; }
        public string Rel { get; set; }
        public List<DropboxBasketMessage> Messages { get; set; }
    }

    public class DropboxBasketMessage
    {
        public DropboxBasketMessage()
        {
            SubmissionStudent = new DropboxBasketUser();
            Author = new DropboxBasketUser();
        }

        public string ID { get; set; }
        public string Comments { get; set; }
        public DateTime Date { get; set; }
        public DropboxBasketUser SubmissionStudent { get; set; }
        public DropboxBasketUser Author { get; set; }
    }

    public class DropboxBasketUser
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string eMailAdress { get; set; }
        public string Href { get; set; }
        public string Rel { get; set; }
    }
}
