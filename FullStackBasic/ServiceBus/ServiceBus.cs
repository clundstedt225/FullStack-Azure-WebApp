using System;

using Repositories;
using Model;

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceBus
{
    public class pubsService
    {
        //Repos
        private IRepository<author> _authorRepository;
        private IRepository<book> _bookRepository;
        private IRepository<sale> _saleRepository;
        private IRepository<store> _storeRepository;
        private IRepository<order> _orderRepository;

        public pubsService(IRepository<author> authorRepo, IRepository<book> bookRepo)
        {
            _authorRepository = authorRepo;
            _bookRepository = bookRepo;
        }

        public pubsService(IRepository<sale> salesRepo, IRepository<store> storeRepo, IRepository<order> orderRepo, IRepository<book> bookRepo)
        {
            _bookRepository = bookRepo;
            _saleRepository = salesRepo;
            _storeRepository = storeRepo;
            _orderRepository = orderRepo;
        }

        public bool submitOrder(order ord)
        {
            return _orderRepository.Add(ord);
        }

        public order CreateNewOrder(string stor_id, string term)
        {
            order od = new order();

            Random random = new Random();
            int length = 6;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string onum = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            od.orderNumber = onum;
            od.orderDate = DateTime.Now;
            od.storeID = stor_id;
            od.orderTerms = term;

            return od;
        }

        public List<saleViewModel> getAllSales()
        {
            List<saleViewModel> saleModels = new List<saleViewModel>();
            List<sale> saleList = _saleRepository.FindAll();

            foreach (sale sl in saleList)
            {
                saleModels.Add(new saleViewModel(sl.stor_id, sl.ord_num, sl.ord_date, sl.quantity, sl.pay_terms, sl.title_id));
            }

            return saleModels;
        }

        public List<storeViewModel> getAllStores()
        {
            List<storeViewModel> storeModels = new List<storeViewModel>();
            List<store> saleList = _storeRepository.FindAll();

            foreach (store st in saleList)
            {
                storeModels.Add(new storeViewModel(st.stor_id, st.stor_name, st.stor_address, st.city, st.state, st.zip));
            }

            return storeModels;
        }

        public List<bookViewModel> getAllBooks()
        {
            List<bookViewModel> bookVMDLS = new List<bookViewModel>();
            List<book> bkList = _bookRepository.FindAll();

            foreach (book bk in bkList)
            {
                bookVMDLS.Add(new bookViewModel(bk.Id, bk.Title, bk.PubID, bk.PubName, bk.Type, bk.Price, bk.PubDate));
            }

            return bookVMDLS;
        }

        public authorViewModel avmFromId(string id)
        {
            author au = _authorRepository.Find(id);

            authorViewModel avm = new authorViewModel(au.au_id, au.au_fname, au.au_lname, au.au_phone, au.au_address, au.au_city, au.au_state, au.au_zip, au.au_contract);

            return avm;
        }

        public List<authorViewModel> getAllAuthors()
        {
            List<authorViewModel> authors = new List<authorViewModel>();

            List<author> auList = _authorRepository.FindAll();

            //Convert author to view model added to list
            foreach (author au in auList)
            {
                authors.Add(new authorViewModel(au.au_id, au.au_fname, au.au_lname, au.au_phone, au.au_address, au.au_city, au.au_state, au.au_zip, au.au_contract));
            }

            return authors;
        }

        public List<bookViewModel> getBooksByAuthorID(string id)
        {
            List<bookViewModel> bookVMDLS = new List<bookViewModel>();
            List<book> bkList = _bookRepository.getBooksByAuthor(id);

            foreach (book bk in bkList)
            {
                bookVMDLS.Add(new bookViewModel(bk.Id, bk.Title, bk.PubID, bk.PubName, bk.Type, bk.Price, bk.PubDate));
            }

            return bookVMDLS;
        }

        public store findStoreByID(string id)
        {
            return _storeRepository.Find(id);
        }

        public book findBookByID(string id)
        {
            return _bookRepository.Find(id);
        }

        public author getAuthorByID(string au_id)
        {
            return _authorRepository.Find(au_id); 
        }

        public bool editAuthor(author au)
        {
            return _authorRepository.Update(au);
        }

        public bool addAuthor(author au)
        {
            return _authorRepository.Add(au);
        }

        public bool deleteAuthor(author au)
        {
            return _authorRepository.Remove(au);
        }
    }

    public class storeViewModel
    {
        public string storeID { get; set; }
        public string storeName { get; set; }
        public string storeAddr { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }

        public storeViewModel(string stID, string name, string addr, string cit, string ste, string zp)
        {
            storeID = stID;
            storeName = name;
            storeAddr = addr;
            city = cit;
            state = ste;
            zip = zp;
        }
    }

    public class saleViewModel
    {
        public string storeID { get; set; }
        public string orderNum { get; set; }
        public string orderDate { get; set; }
        public string quantity { get; set; }
        public string payTerms { get; set; }
        public string titleID { get; set; }

        public saleViewModel(string id, string ordNum, DateTime date, int qty, string terms, string titleId)
        {
            storeID = id;
            orderNum = ordNum;
            orderDate = date.ToShortDateString();
            quantity = qty.ToString();
            payTerms = terms;
            titleID = titleId;
        }
    }

    public class bookViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string PubID { get; set; }
        public string PubName { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
        public string PubDate { get; set; }

        //Assign vars and make string conversions
        public bookViewModel(string id, string title, string pubid, string pname, string type, decimal price, DateTime date)
        {
            Id = id;
            Title = title;
            PubID = pubid;
            PubName = pname;
            Type = type;
            Price = string.Format("{0:C}", price); 
            PubDate = date.ToShortDateString();
        }
    }

    public class authorViewModel
    {
        // the view model class is for creating a display friendly object
        // usually, class properites are converted to strings 

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Contract { get; set; }
        public string ID { get; private set; }
        public string Name { get { return FirstName + " " + LastName; } }

        public authorViewModel(string id, string fname, string lname, string phone, string address, string city, string state, string zip, bool contract)
        {
            FirstName = fname;
            LastName = lname;
            Phone = phone;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            ID = id;

            if (contract)
            {
                Contract = "Yes";
            }
            else
            {
                Contract = "No";
            }
        }
    }


}
