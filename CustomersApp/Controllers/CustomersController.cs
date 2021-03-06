using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomersApp.Models;
using Npgsql;
using CustomersApp.DBConnection;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomersApp.Controllers
{
    public class CustomersController : Controller
    {
        ConnectionDB db = new ConnectionDB();


        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                string query = "SELECT * FROM Customers";

                var cmd = new NpgsqlCommand(query, db.DBConnection());

                NpgsqlDataReader reader = cmd.ExecuteReader();

                List<Customers> CustomersList = new List<Customers>();
                

                while (reader.Read())
                {


                    CustomersList.Add(new Customers
                    {
                        CustomerID = Int32.Parse(reader.GetValue(0).ToString()),
                        Name = reader.GetValue(1).ToString(),
                        LastName = reader.GetValue(2).ToString(),
                        DocumentID = reader.GetValue(3).ToString(),
                        Email = reader.GetValue(4).ToString(),
                        Phone = reader.GetValue(5).ToString()
                    });
                    
                }

                ViewBag.Customers = CustomersList;

                db.DBConnection().Close();

              

            }
            catch (Exception e)
            {
                Console.WriteLine("Create Error" + e);
            }

            return View();
        }






        public IActionResult Create()
        {
        

            return View();
        }

        [HttpPost]
        public IActionResult Create(Customers customer)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    string query = $"CALL InsertCustomers_And_MainAddress('{customer.Name}','{customer.LastName}'," +
                                                                            $"'{customer.DocumentID}'," +
                                                                            $"'{customer.Email}'," +
                                                                            $"'{customer.Phone}'," +
                                                                            $"'{customer.MainAddress}')";
                    var cmd = new NpgsqlCommand(query, db.DBConnection());

                  
                    Console.WriteLine("Added");

                    cmd.ExecuteNonQuery();

                    db.DBConnection().Close();

                }
                catch (Exception e) {
                    Console.WriteLine("Create Error" + e);
                }

            }
            else
            {
                Console.WriteLine("State not Valid");
                return RedirectToAction("Create");

            }
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Delete(int id)
        {
            
                try
                {
                    string query1 = $"CALL DeleteCustomers_And_Address({id})";
                    var cmd1 = new NpgsqlCommand(query1, db.DBConnection());


                    cmd1.ExecuteNonQuery();
                    
                db.DBConnection().Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Delete Error " + e);
                }

                Console.WriteLine("Customer Deleted");
                return RedirectToAction("Index");

        }


        [HttpPost]
        public IActionResult Delete(Customers customer)
        {
            
            return RedirectToAction("Index");
        }


        //GET
        public IActionResult Update(int id)
        {

            if(id == 0)
            {
                return NotFound();
            }

            try
            {
                string query = $"SELECT * FROM Customers where IDCustomers ={id}";

                var cmd = new NpgsqlCommand(query, db.DBConnection());

                NpgsqlDataReader reader = cmd.ExecuteReader();

                List<Customers> CustomersList = new List<Customers>();

                while (reader.Read())
                {
                    CustomersList.Add(new Customers
                    {
                        CustomerID = Int32.Parse(reader.GetValue(0).ToString()),
                        Name = reader.GetValue(1).ToString(),
                        LastName = reader.GetValue(2).ToString(),
                        DocumentID = reader.GetValue(3).ToString(),
                        Email = reader.GetValue(4).ToString(),
                        Phone = reader.GetValue(5).ToString()
                    });
                }

                ViewBag.Customers = CustomersList;

                db.DBConnection().Close();

                foreach (var i in CustomersList)
                    Console.Write(i.CustomerID);

            }catch(Exception e)
            {
                Console.WriteLine("Update Error " + e);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Update(Customers customer, int id)
        {

            if (!ModelState.IsValid)
            {
                try
                {
                    string query = $"UPDATE Customers SET Name ='{customer.Name}'," +
                                   $"LastName ='{customer.LastName}', " +
                                   $"DocumentID ='{customer.DocumentID}', " +
                                   $"Email ='{customer.Email}'," +
                                   $"Phone ='{customer.Phone}' WHERE IDCustomers ={id}";

                    var cmd = new NpgsqlCommand(query, db.DBConnection());

                    Console.WriteLine("Added");
                    cmd.ExecuteNonQuery();
                    db.DBConnection().Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Create Error" + e);
                }
            }
            else
            {
                Console.WriteLine("State not Valid");
                return RedirectToAction("Create");

            }
            return RedirectToAction("Index");
        }
    }
}
