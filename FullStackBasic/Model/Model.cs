using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class author
    {
        [Required(ErrorMessage = "Please enter an ID")]
        [Display(Name ="ID")]
        [RegularExpression(@"^[0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9]$", ErrorMessage ="xxx-xx-xxxx")]
        public string au_id { get; set; }

        [Required, MinLength(1, ErrorMessage="Please enter first name"), MaxLength(40)]
        [Display(Name ="First Name")]
        public string au_fname { get; set; }

        [Required, MinLength(1, ErrorMessage = "Please enter last name"), MaxLength(40)]
        [Display(Name = "Last Name")]
        public string au_lname { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        [Display(Name = "Phone")]
        [RegularExpression(@"^[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]$", ErrorMessage = "xxx-xxx-xxxx")]
        public string au_phone { get; set; }

        [Required, MinLength(1, ErrorMessage = "Please enter address"), MaxLength(40)]
        [Display(Name = "Address")]
        public string au_address { get; set; }

        [Required, MinLength(1, ErrorMessage = "Please enter city"), MaxLength(40)]
        [Display(Name = "City")]
        public string au_city { get; set; }

        [Required, MinLength(2, ErrorMessage = "Please enter state"), MaxLength(2)]
        [Display(Name = "State")]
        public string au_state { get; set; }

        [Required(ErrorMessage = "Please enter a zip code")]
        [Display(Name = "Zip")]
        [RegularExpression(@"^[0-9][0-9][0-9][0-9][0-9]$", ErrorMessage = "xxxxx")]
        public string au_zip { get; set; }

        public bool au_contract { get; set; }              
    }

    public class book { 
        public string Id { get; set; }
        public string Title { get; set; }
        //public publisher Pub { get; set; }
        //class with name and id
        public string PubID { get; set; }
        public string PubName { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Royalty { get; set; }
        public int YTDSales { get; set; }
        public string Notes { get; set; }
        public DateTime PubDate { get; set; }
    }

    public class sale
    {
        public string stor_id { get; set; }
        public string ord_num { get; set; }
        public DateTime ord_date { get; set; }
        public int quantity { get; set; }
        public string pay_terms { get; set; }
        public string title_id { get; set; }

    }

    public class store
    {
        public string stor_id { get; set; }
        public string stor_name { get; set; }
        public string stor_address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    }

    public class order
    {
        public string storeID { get; set; }
        public string storeName { get; set; }
        public string orderNumber { get; set; }
        public string orderTerms { get; set; }
        public DateTime orderDate { get; set; }
        public List<orderItem> Items { get; set; }
    }

    public class orderItem
    {
        public string titleID { get; set; }
        public string quantity { get; set; }

        public orderItem(string id, string qty)
        {
            titleID = id;
            quantity = qty;
        }
    }
}
