using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebUserInfomation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUserInfomation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            string connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True; Encrypt=false;";
            string query = "SELECT FirstName, LastName, Age, Gender, Province FROM personalinfo";
            try
            {
                List<UserProcessor> users = new List<UserProcessor>(); // declare and initialize the users variable
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserProcessor user = new UserProcessor();
                                user.Name = reader.GetString(0);
                                user.Surname = reader.GetString(1);
                                user.age = reader.GetInt32(2);
                                user.Gender = reader.GetString(3);
                                user.Province = reader.GetString(4);
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



        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }



        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] UserProcessor user)
        {
            if (user == null)
            {
                return BadRequest();
            }



            string connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True; Encrypt=false;";
            string query = "INSERT INTO personalinfo(FirstName, LastName, Age, Gender, Province) VALUES(@Name, @Surname, @age, @Gender, @Province)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Surname", user.Surname);
                        command.Parameters.AddWithValue("@age", user.age);
                        command.Parameters.AddWithValue("@Gender", user.Gender);
                        command.Parameters.AddWithValue("@Province", user.Province);                   
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



        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }



        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {


            string connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True; Encrypt=false;";
            string query = "DELETE FROM personalinfo WHERE PersonId=@id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                       
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
