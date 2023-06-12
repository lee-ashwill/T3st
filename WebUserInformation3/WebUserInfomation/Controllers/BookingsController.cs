using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebUserInfomation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUserInfomation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        // GET: api/<BookingsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookingsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookingsController>
        [HttpPost]
        public IActionResult Post([FromBody] BookingProcessor book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            string connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True; Encrypt=false;";
            string query = "INSERT INTO booking (Firstname, Lastname, Cellphone, Email, Nights, [Booking Date], Room) VALUES (@FirstName, @LastName, @CellPhone, @Email, @Nights, @BookingDate, @Room)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", book.Firstname);
                        command.Parameters.AddWithValue("@LastName", book.Lastname);
                        command.Parameters.AddWithValue("@CellPhone", book.Cellphone);
                        command.Parameters.AddWithValue("@Email", book.Email);
                        command.Parameters.AddWithValue("@Nights", book.Nights);
                        command.Parameters.AddWithValue("@BookingDate", book.Date);
                        command.Parameters.AddWithValue("@Room", book.Room);
                        command.ExecuteNonQuery();
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BookingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookingsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
