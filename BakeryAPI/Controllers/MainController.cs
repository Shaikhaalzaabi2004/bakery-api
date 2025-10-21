using BakeryAPI.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session6API.Models;

namespace Session6API.Controllers
{
    [ApiController]
    [Route("api")]
    public class MainController : ControllerBase
    {
        BakeryDbContext db = new();
        [HttpPost("login")]
        public ActionResult Login(LoginRequests loginRequest) 
        {
            try
            {
                var validUser = db.Customers.FirstOrDefault(c => c.Email == loginRequest.Email && c.Password == loginRequest.Password);
                if (validUser != null)
                    return Ok(validUser);
                return NotFound("Invalid Credentials");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("products")]
        public ActionResult GetProducts() 
        {
            try
            {
                var products = db.Products.Include(p => p.ProductsIngredients).ThenInclude(i => i.Ingredient).Include(c => c.Category).ToList();
                if (products.Count > 0)
                    return Ok(products);
                return NotFound("No Products Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("categories")]

        public ActionResult GetCategories() 
        {
            try
            {
                var categories = db.Categories.ToList();
                if (categories.Count > 0)
                    return Ok(categories);
                return NotFound("No Categories Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("customer/{id}")]
        public ActionResult GetCustomerById(int id) 
        {
            try
            {
                var Customer = db.Customers.Include(t=> t.Transactions).FirstOrDefault(c => c.Id == id);
                if (Customer != null)
                    return Ok(Customer);
                return NotFound("No Customer With ID");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("orders/{id}")]
        public ActionResult GetCustomerOrders(int id) 
        {
            try
            {
                var Orders = db.Transactions.Include(p => p.Product).Where(c => c.CustomerId == id).ToList();
                if (Orders.Count > 0)
                    return Ok(Orders);
                return NotFound("Customer Has No Orders");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("orders")]
        public ActionResult PlaceOrder(Transaction transaction) 
        {
            try
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();

                var customer = db.Customers.FirstOrDefault(c => c.Id == transaction.CustomerId);
                
                if(customer == null)
                    return NotFound("No Customer With ID");
                
                customer.Balance -= transaction.Price * transaction.Quantity;
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
