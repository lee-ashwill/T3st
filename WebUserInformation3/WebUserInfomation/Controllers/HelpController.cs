using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebUserInfomation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUserInfomation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        // GET: api/<HelpController>
        [HttpGet]
        public IActionResult Get()
        {
            string connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True; Encrypt=false;";
            string query = "SELECT * FROM contactus";
            try
            {
                List<ContactProcessor> users = new List<ContactProcessor>(); // declare and initialize the users variable
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContactProcessor user = new ContactProcessor();
                                user.Firstname = reader.GetString(0);
                                user.Lastname = reader.GetString(1);
                                user.Position = reader.GetString(2);
                                user.Cellphone = reader.GetString(3);
                                user.Email = reader.GetString(4);
                                users.Add(user);
                            }
                        }
                    }
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET api/<HelpController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HelpController>
        [HttpPost]
        public IActionResult Post([FromBody] InvolvedProcessor user)
        {
            if (user == null)
            {
                return BadRequest();
            }



            string connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True; Encrypt=false;";
            string query = "INSERT INTO getinvolved (Firstname, Lastname, Cellphone, Email, Message) VALUES (@Name, @Surname, @Cellphone, @Email, @Message)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", user.Firstname);
                        command.Parameters.AddWithValue("@Surname", user.Lastname);
                        command.Parameters.AddWithValue("@Cellphone", user.Cellphone);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Message", user.Message);
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

      



        // PUT api/<HelpController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HelpController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
        

            string connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True; Encrypt=false;";
            string query = "DELETE FROM getinvolved WHERE Email=@id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        InvolvedProcessor user = new InvolvedProcessor();
                        command.Parameters.AddWithValue("@id", id);
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



    }
}
