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

    public class AddressController : Controller
    {
        ConnectionDB db = new ConnectionDB();
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Create(Address address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string query = "INSERT INTO address (CustomerID, Address) VALUES (@p1,@p2)";

                    var cmd = new NpgsqlCommand(query, db.DBConnection());

                    cmd.Parameters.AddWithValue("p1", address.CustomerID);
                    cmd.Parameters.AddWithValue("p2", address.Addresses);
                   
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


        public IActionResult Delete()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Delete(int addressID)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    string query = "DELETE FROM Address WHERE IDAddress = @p1";

                    var cmd = new NpgsqlCommand(query, db.DBConnection());
                    cmd.Parameters.AddWithValue("p1", addressID);

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
