using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebUserInfomation.Models;
using System.IO;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUserInfomation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        
        // GET: api/Images
        [HttpGet]
        public ActionResult<IEnumerable<ImageUploadModel>> Get(byte[] ImageData)
        {
            string _connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True;Encrypt=false;";
            try
            {
                string query = "SELECT Id, ImageData FROM Images";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<ImageUploadModel> images = new List<ImageUploadModel>();

                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["Id"]);
                            byte[] imageData = (byte[])reader["ImageData"];

                            ImageUploadModel image = new ImageUploadModel
                            {
                                ImageData = imageData
                            };

                            images.Add(image);
                        }


                        connection.Close();

                        return Ok(images);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Images/5
        [HttpGet("{id}")]
        public IActionResult GetImage(int id)
        {
            string _connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True;Encrypt=false;";
            try
            {
                string query = "SELECT ImageData FROM Images WHERE Id = @ImageId";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ImageId", SqlDbType.Int).Value = id;
                        connection.Open();

                        byte[] imageData = (byte[])command.ExecuteScalar();

                        if (imageData == null)
                        {
                            return NotFound();
                        }

                        connection.Close();

                        return File(imageData, "image/jpeg"); // Return the image file
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // POST api/<ImagesController>
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest();
            }



            string connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True; Encrypt=false;";
            string query = "INSERT INTO Images (ImageData) VALUES (@ImageData)";



            //creates a byte array to store the file data
            byte[] imageData;
            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                imageData = stream.ToArray();
            }



            try
            {



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ImageData", SqlDbType.VarBinary, -1).Value = imageData;
                        connection.Open();
                        command.ExecuteScalar();
                        connection.Close();



                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ImagesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ImagesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                string connectionString = @"Data Source=DESKTOP-GMP1P5K;Initial Catalog=details;Integrated Security=True;Encrypt=false;";
                string query = "DELETE FROM Images WHERE Id  = @ImageId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))

                    {
                        command.Parameters.Add("@ImageId", SqlDbType.Int).Value = id;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
