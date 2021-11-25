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
            try
            {
                //string query = "SELECT Customers.IDCustomers, Name ,LastName, IDAddress, Address FROM Customer c INNER JOIN Address a ON a.CustomerID = c.IDCustomers";

                string query = "Select IDCustomers, Name, LastName, IDAddress, Address from Customers INNER JOIN Address c ON  c.CustomerID = Customers.IDCustomers";
                var cmd = new NpgsqlCommand(query, db.DBConnection());

                NpgsqlDataReader reader = cmd.ExecuteReader();

                List<Address> AddressList = new List<Address>();

                while (reader.Read())
                {
                    AddressList.Add(new Address
                    {
                        CustomerID = Int32.Parse(reader.GetValue(0).ToString()),
                        CustomerName = reader.GetValue(1).ToString(),
                        CustomerLastName = reader.GetValue(2).ToString(),
                        IDAddress = Int32.Parse(reader.GetValue(3).ToString()),
                        Addresses = reader.GetValue(4).ToString()
                    });
                }

                ViewBag.Address = AddressList;

                foreach (var i in AddressList)
                    Console.WriteLine(i);

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
        public IActionResult Create(Address address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string query = $"INSERT INTO address (CustomerID, Address) VALUES ('{address.CustomerID}','{address.Addresses}')";

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

        //GET
        public IActionResult Delete(int id)
        {

            try
            {
                string query = $"DELETE FROM Address Where IDAddress = {id}";
                var cmd = new NpgsqlCommand(query, db.DBConnection());

                cmd.ExecuteNonQuery();
                db.DBConnection().Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Delete Error " + e);
            }

            Console.WriteLine("Delete State not Valid");
            return RedirectToAction("Index");

        }


        [HttpPost]
        public IActionResult Delete()
        {

            return View();

        }


        public IActionResult Update(int id) {

            if (id == 0)
            {
                return NotFound();
            }

            try
            {
                string query = $"Select IDAddress,Address from Address where IDAddress={id}"; 

                var cmd = new NpgsqlCommand(query, db.DBConnection());

                NpgsqlDataReader reader = cmd.ExecuteReader();

                List<Address> AddressList = new List<Address>();

                while (reader.Read())
                {
                    AddressList.Add(new Address
                    {
                        IDAddress = Int32.Parse(reader.GetValue(0).ToString()),
                        Addresses = reader.GetValue(1).ToString()
                    });
                }

                ViewBag.Address = AddressList;

                db.DBConnection().Close();

                foreach (var i in AddressList)
                    Console.Write(i.CustomerID);

            }
            catch (Exception e)
            {
                Console.WriteLine("Update Error " + e);
            }
            return View();

        }

        [HttpPost]
        public IActionResult Update(Address address, int id)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    string query = $"UPDATE Address SET Address ='{address.Addresses}' WHERE IDAddress = {id} ";
                    var cmd = new NpgsqlCommand(query, db.DBConnection());

                    Console.WriteLine("Updated");
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
