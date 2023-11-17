using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Net.Http;
//using System.Text;

//SQL server client
using System.Data.SqlClient;

using Model; // the namespace inside of the Model.dll

namespace Repositories
{
    public interface IRepository<T>
    {
        List<T> FindAll();
        T Find(string id);
        bool Add(T x);
        bool Update(T x);
        bool Remove(T x);
        List<T> getBooksByAuthor(string au_id);
    }

    public class AuthorRepoAPI : IRepository<author>
    {
        private string @_root;

        public AuthorRepoAPI(string @root)
        {
            _root = root; // http://localhost/}
        }

        public bool Add(author x) 
        { 
            string path = _root + "pubs/authors";
            
            using (var client = new HttpClient()) 
            { 
                var content = new StringContent(JsonConvert.SerializeObject(x), Encoding.UTF8, "application/json"); 
                HttpResponseMessage response = client.PostAsync(path, content).GetAwaiter().GetResult(); 
                if (response.IsSuccessStatusCode) 
                { 
                    return true; 
                } 
            } 
            
            return false; 
        }

        public List<author> FindAll() 
        { 
            List<author> authors = null; 
            string path = _root + "pubs/authors"; 

            using (var client = new HttpClient()) 
            { 
                HttpResponseMessage response = client.GetAsync(path).GetAwaiter().GetResult(); 
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); 
                authors = JsonConvert.DeserializeObject<List<author>>(result); 
            } 

            return authors;
        }

        public author Find(string id)
        { 
            author au = null; 
            string path = _root + $"pubs/authors/{id}"; 

            using (var client = new HttpClient()) 
            { 
                HttpResponseMessage response = client.GetAsync(path).GetAwaiter().GetResult(); 
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); 
                au = JsonConvert.DeserializeObject<author>(result); 
            } 

            return au; 
        }

        public bool Remove(author x) 
        { 
            string path = _root + $"pubs/authors/{x.au_id}"; 

            using (var client = new HttpClient()) 
            { 
                HttpResponseMessage response = client.DeleteAsync(path).GetAwaiter().GetResult(); 
                if (response.IsSuccessStatusCode) 
                    return true; 
            } 

            return false; 
        }

        public bool Update(author x)
        {
            string path = _root + $"authors"; 

            using (var client = new HttpClient()) 
            { 
                var content = new StringContent(JsonConvert.SerializeObject(x), Encoding.UTF8, "application/json"); 
                HttpResponseMessage response = client.PutAsync(path, content).GetAwaiter().GetResult(); 

                if (response.IsSuccessStatusCode) 
                { 
                    return true; 
                } 
            }

            return false;
        }

        List<author> IRepository<author>.getBooksByAuthor(string au_id)
        {
            throw new NotImplementedException();
        }
    }

    public class orderRepoDB : IRepository<order>
    {
        string _connection;

        public orderRepoDB(string conn)
        {
            _connection = conn;
        }

        //Add an order to sales table
        public bool Add(order x)
        {
            bool result = true;
            string sql = "INSERT INTO sales VALUES(@storeid, @ordernum, @orderdate, @qty, @payterms, @titleid)";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@storeid", x.storeID));
                        command.Parameters.Add(new SqlParameter("@ordernum", x.orderNumber));
                        command.Parameters.Add(new SqlParameter("@orderdate", x.orderDate));
                        command.Parameters.Add(new SqlParameter("@payterms", x.orderTerms));

                        //Changes with each item in order
                        command.Parameters.Add(new SqlParameter("@titleid", ""));
                        command.Parameters.Add(new SqlParameter("@qty", 0));

                        foreach (orderItem itm in x.Items)
                        {
                            command.Parameters["@titleid"].Value = itm.titleID;
                            command.Parameters["@qty"].Value = itm.quantity;

                            //Insert an entire entry for each orderItem with only the book and qty differing
                            command.ExecuteNonQuery();   
                        }                       
                    }// end using command 
                }// end of using connection 
            }
            catch (Exception ex)
            {
                return false;
            }

            return result;
        }

        public order Find(string id)
        {
            throw new NotImplementedException();
        }

        public List<order> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<order> getBooksByAuthor(string au_id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(order x)
        {
            throw new NotImplementedException();
        }

        public bool Update(order x)
        {
            throw new NotImplementedException();
        }
    }

    public class storesRepoDB : IRepository<store>
    {
        string _connection;

        public storesRepoDB(string conn)
        {
            _connection = conn;
        }

        public bool Add(store x)
        {
            throw new NotImplementedException();
        }

        public store Find(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM stores WHERE stor_id = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string line = reader["stor_id"].ToString();

                            if (id == line)
                            {
                                //Found author match, instantiate a returnable author
                                store st = new store();
                                //read from data base and set model values
                                st.stor_id = reader["stor_id"].ToString().Trim();
                                st.stor_name = reader["stor_name"].ToString().Trim();
                                st.stor_address = reader["stor_address"].ToString().Trim();
                                st.zip = reader["zip"].ToString().Trim();
                                st.city = reader["city"].ToString().Trim();
                                st.state = reader["state"].ToString().Trim();

                                return st;
                            }
                        }
                    }// end using reader 
                }// end using command 
            }// end of using connection

            //Has not returned yet so return a null author
            store nullSt = new store();

            return nullSt;
        }

        public List<store> FindAll()
        {
            List<store> stores = new List<store>();

            string sql = "SELECT * FROM stores";

            try
            {
                //getfromdb
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                store st = new store()
                                {
                                    //read from data base and set model values
                                    stor_id = reader["stor_id"].ToString().Trim(),
                                    stor_name = reader["stor_name"].ToString().Trim(),
                                    stor_address = reader["stor_address"].ToString().Trim(),
                                    city = reader["city"].ToString().Trim(),
                                    state = reader["state"].ToString().Trim(),
                                    zip = reader["zip"].ToString().Trim()
                                };

                                stores.Add(st);
                            }
                        }// end using reader 
                    }// end using sqlcommand                    
                }// end using connection
            }
            catch (Exception ex)
            {

            }

            return stores;
        }

        //Ignore for this repo
        public List<store> getBooksByAuthor(string au_id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(store x)
        {
            throw new NotImplementedException();
        }

        public bool Update(store x)
        {
            throw new NotImplementedException();
        }
    }

    public class SalesRepoDB : IRepository<sale>
    {
        string _connection;

        public SalesRepoDB(string conn)
        {
            _connection = conn;
        }

        public bool Add(sale x)
        {
            throw new NotImplementedException();
        }

        public sale Find(string id)
        {
            throw new NotImplementedException();
        }

        public List<sale> FindAll()
        {
            List<sale> sales = new List<sale>();

            string sql = "SELECT * FROM sales";

            try
            {
                //getfromdb
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sale sl = new sale()
                                {
                                    //read from data base and set model values
                                    stor_id = reader["stor_id"].ToString().Trim(),
                                    ord_num = reader["ord_num"].ToString().Trim(),
                                    ord_date = Convert.ToDateTime(reader["ord_date"]),
                                    quantity = Convert.ToInt16(reader["qty"]),
                                    pay_terms = reader["payterms"].ToString().Trim(),
                                    title_id = reader["title_id"].ToString().Trim()
                                };

                                sales.Add(sl);
                            }
                        }// end using reader 
                    }// end using sqlcommand                    
                }// end using connection
            }
            catch (Exception ex)
            {

            }

            return sales;
        }

        //Ignore for this repo
        public List<sale> getBooksByAuthor(string au_id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(sale x)
        {
            throw new NotImplementedException();
        }

        public bool Update(sale x)
        {
            throw new NotImplementedException();
        }
    }

    public class BookRepoDB : IRepository<book>
    {
        string _connection;

        public BookRepoDB(string conn)
        {
            _connection = conn;
        }

        public bool Add(book x)
        {
            throw new NotImplementedException();
        }

        public book Find(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM titles WHERE title_id = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string line = reader["title_id"].ToString();

                            if (id == line)
                            {
                                //Found author match, instantiate a returnable author
                                book bk = new book();
                                //read from data base and set model values
                                bk.Title = reader["title_id"].ToString().Trim();
                                bk.Title = reader["title"].ToString().Trim();

                                return bk;
                            }
                        }
                    }// end using reader 
                }// end using command 
            }// end of using connection

            //Has not returned yet so return a null author
            book nullAu = new book();

            return nullAu;
        }

        public List<book> FindAll()
        {
            List<book> books = new List<book>();

            string sql = "SELECT title_id, title, [type], T.pub_id, pub_name, price, pubdate FROM titles T ";
            sql += "JOIN publishers P ";
            sql += "ON P.pub_id = T.pub_id";

            try
            {
                //getfromdb
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                book bk = new book()
                                {
                                    //read from data base and set model values
                                    Id = reader["title_id"].ToString().Trim(),
                                    Title = reader["title"].ToString().Trim(),
                                    PubID = reader["pub_id"].ToString().Trim(),
                                    PubName = reader["pub_name"].ToString().Trim(),
                                    Type = reader["type"].ToString().Trim(),
                                    Price = Convert.ToDecimal(reader["price"]),
                                    PubDate = Convert.ToDateTime(reader["pubdate"])

                                    /* ignored fields
                                    Royalty = Convert.ToInt32(reader[""]),
                                    YTDSales = Convert.ToInt32(reader[""]),
                                    Notes = reader[""].ToString().Trim(),
                                    */
                                };

                                books.Add(bk);
                            }
                        }// end using reader 
                    }// end using sqlcommand                    
                }// end using connection
            }
            catch (Exception ex)
            {

            }

            return books;
        }

        public bool Remove(book x)
        {
            throw new NotImplementedException();
        }

        public bool Update(book x)
        {
            throw new NotImplementedException();
        }

        public List<book> getBooksByAuthor(string au_id)
        {
            List<book> books = new List<book>();

            string sql = "SELECT T.title_id, title, [type], T.pub_id, pub_name, price, pubdate FROM titles T ";
            sql += "JOIN publishers P ";
            sql += "ON P.pub_id = T.pub_id ";
            sql += "JOIN titleauthor TA ";
            sql += "ON TA.title_id = T.title_id ";
            sql += "WHERE TA.au_id = @id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@id", au_id));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                book bk = new book()
                                {
                                    //read from data base and set model values
                                    Id = reader["title_id"].ToString().Trim(),
                                    Title = reader["title"].ToString().Trim(),
                                    PubID = reader["pub_id"].ToString().Trim(),
                                    PubName = reader["pub_name"].ToString().Trim(),
                                    Type = reader["type"].ToString().Trim(),
                                    Price = Convert.ToDecimal(reader["price"]),
                                    PubDate = Convert.ToDateTime(reader["pubdate"])

                                    /* ignored fields
                                    Royalty = Convert.ToInt32(reader[""]),
                                    YTDSales = Convert.ToInt32(reader[""]),
                                    Notes = reader[""].ToString().Trim(),
                                    */
                                };

                                books.Add(bk);

                            }
                        }// end using reader 
                    }// end using command 
                }// end of using connection
            } catch(Exception ex)
            {

            }

            return books;
        }
    }

    public class AuthorRepoDB : IRepository<author>
    {
        string _connection;

        public AuthorRepoDB(string conn)
        {
            _connection = conn;
        }

        //Adds passed down author object to authors table
        public bool Add(author x)
        {           
            bool result = true;
            string sql = "INSERT INTO authors VALUES(@id, @lname, @fname, @phone, @address, @city, @state, @zip, @contract)";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@id", x.au_id));
                        command.Parameters.Add(new SqlParameter("@lname", x.au_lname));
                        command.Parameters.Add(new SqlParameter("@fname", x.au_fname));
                        command.Parameters.Add(new SqlParameter("@phone", x.au_phone));
                        command.Parameters.Add(new SqlParameter("@address", x.au_address));
                        command.Parameters.Add(new SqlParameter("@city", x.au_city));
                        command.Parameters.Add(new SqlParameter("@state", x.au_state));
                        command.Parameters.Add(new SqlParameter("@zip", x.au_zip));
                        command.Parameters.Add(new SqlParameter("@contract", x.au_contract));

                        command.ExecuteNonQuery();

                    }// end using command 
                }// end of using connection 
            }
            catch (Exception ex)
            {
                return false;
            }

            return result;
        }

        public author Find(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM authors WHERE au_id = @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string line = reader["au_id"].ToString();

                            if (id == line)
                            {
                                //Found author match, instantiate a returnable author
                                author au = new author();
                                //read from data base and set model values
                                au.au_id = reader["au_id"].ToString().Trim();
                                au.au_lname = reader["au_lname"].ToString().Trim();
                                au.au_fname = reader["au_fname"].ToString().Trim();
                                au.au_phone = reader["phone"].ToString().Trim();
                                au.au_address = reader["address"].ToString().Trim();
                                au.au_city = reader["city"].ToString().Trim();
                                au.au_state = reader["state"].ToString().Trim();
                                au.au_zip = reader["zip"].ToString().Trim();
                                au.au_contract = (bool)reader["contract"]; //cast from 1/0 to C# bool

                                return au;
                            }
                        }
                    }// end using reader 
                }// end using command 
            }// end of using connection

            //Has not returned yet so return a null author
            author nullAu = new author()
            {
                //read from data base and set model values
                au_id = null,
                au_lname = null,
                au_fname = null,
                au_phone = null,
                au_address = null,
                au_city = null,
                au_state = null,
                au_zip = null,
                au_contract = false
            };

            return nullAu;
        }

        public List<author> FindAll()
        {
            List<author> authors = new List<author>();

            //getfromdb
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM authors", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            author au = new author()
                            {
                                //read from data base and set model values
                                au_id = reader["au_id"].ToString().Trim(),
                                au_lname = reader["au_lname"].ToString().Trim(),
                                au_fname = reader["au_fname"].ToString().Trim(),
                                au_phone = reader["phone"].ToString().Trim(),
                                au_address = reader["address"].ToString().Trim(),
                                au_city = reader["city"].ToString().Trim(),
                                au_state = reader["state"].ToString().Trim(),
                                au_zip = reader["zip"].ToString().Trim(),
                                au_contract = (bool)reader["contract"] //cast from 1/0 to C# bool
                            };

                            authors.Add(au);
                        }
                    }// end using reader 
                }// end using sqlcommand                    
            }// end using connection

            return authors;
        }

        public bool Remove(author x)
        {
            bool result = true;

            //Multiple delete commands to resolve reference conlfict
            string sql = "DELETE FROM authors WHERE au_id = @id";
            string sql2 = "DELETE FROM titleauthor WHERE au_id = @id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@id", x.au_id));

                        int hm = command.ExecuteNonQuery();

                        if (hm <= 0) result = false;

                    }// end using command 1

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@id", x.au_id));

                        int hm = command.ExecuteNonQuery();

                        if (hm <= 0) result = false;

                    }// end using command 2
                }// end of using connection 
            }
            catch (Exception ex)
            {
                
            }

            return result;
        }


        //Takes in author object and updates it's related id match in the DB to the new values
        public bool Update(author x)
        {          
            //ID not changable, used here to find author to update values for
            string sql = "UPDATE authors SET au_lname = @lname, au_fname = @fname, phone = @phne, address = @addr, city = @cty, state = @ste, zip = @zp, contract = @cntrct WHERE au_id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Id", x.au_id));
                        command.Parameters.Add(new SqlParameter("@lname", x.au_lname));
                        command.Parameters.Add(new SqlParameter("@fname", x.au_fname));
                        command.Parameters.Add(new SqlParameter("@phne", x.au_phone));
                        command.Parameters.Add(new SqlParameter("@addr", x.au_address));
                        command.Parameters.Add(new SqlParameter("@cty", x.au_city));
                        command.Parameters.Add(new SqlParameter("@ste", x.au_state));
                        command.Parameters.Add(new SqlParameter("@zp", x.au_zip));
                        command.Parameters.Add(new SqlParameter("@cntrct", x.au_contract));

                        int howMany = command.ExecuteNonQuery();

                        if (howMany <= 0) return false; // failed to make changes 
                    }// end using command 
                }// end of using connection 

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        List<author> IRepository<author>.getBooksByAuthor(string au_id)
        {
            throw new NotImplementedException();
        }
    }


    //example repo for local csv file (non database)
    public class AuthorRepoFile : IRepository<author>
    {
        string _connectionString; 

        public AuthorRepoFile(string connectionString)
        {
            _connectionString = connectionString; 
        }

        public List<author> FindAll()
        {
            List<author> authors = new List<author>();

            // using statement: object is correctly disposed of
            using (StreamReader reader = File.OpenText(_connectionString))
            {
                int count = 0; 

                string line = null;

                line = reader.ReadLine(); // get line from file, or null at end of file

                while (line != null)
                {
                    if (count > 0) // skip first row
                    {
                        string[] fields = line.Split(',');

                        author au = new author()
                        {
                            au_id = fields[0].ToString().Trim(),
                            au_lname = fields[1].ToString().Trim(),
                            au_fname = fields[2].ToString().Trim()
                        }; 

                        authors.Add(au); 

                    }

                    count++;

                    line = reader.ReadLine(); // next line in file 
                } // end while 
            }// end using 

            return authors;

        }

        public author Find(string id)
        {
            throw new NotImplementedException();
        }

        public bool Add(author x)
        {
            throw new NotImplementedException();
        }

        public bool Update(author x)
        {
            throw new NotImplementedException();
        }

        public bool Remove(author x)
        {
            throw new NotImplementedException();
        }

        List<author> IRepository<author>.getBooksByAuthor(string au_id)
        {
            throw new NotImplementedException();
        }
    }
}
