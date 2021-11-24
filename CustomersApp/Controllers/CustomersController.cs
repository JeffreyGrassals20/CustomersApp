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
                    
                    Console.WriteLine(reader.GetValue(2));
                }


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
                    string query = "INSERT INTO customers (name,lastname,documentid,email,phone) VALUES (@p1,@p2,@p3,@p4,@p5)";

                    var cmd = new NpgsqlCommand(query, db.DBConnection());

                    cmd.Parameters.AddWithValue("p1", customer.Name);
                    cmd.Parameters.AddWithValue("p2", customer.LastName) ;
                    cmd.Parameters.AddWithValue("p3", customer.DocumentID);
                    cmd.Parameters.AddWithValue("p4", customer.Email);
                    cmd.Parameters.AddWithValue("p5", customer.Phone);

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


        public IActionResult Delete()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Delete(Customers customer)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    string query = "DELETE FROM CUSTOMERS WHERE DocumentID = @p1";

                    var cmd = new NpgsqlCommand(query, db.DBConnection());

                    cmd.Parameters.AddWithValue("p1", customer.DocumentID);
       
                    Console.WriteLine("Deleted");

                    cmd.ExecuteNonQuery();

                    db.DBConnection().Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Delete Error " + e);
                }

            }
            else
            {
                Console.WriteLine("Delete State not Valid");
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
    }
}
