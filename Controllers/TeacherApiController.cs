using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace PCLD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherApiController : ControllerBase
    {
        [HttpGet("All" , Name = "GetAllTeachers")]
        public ActionResult<IEnumerable<clsTeacher>> GetAllTeacher()
        {
            DataTable dt = clsTeacher.GetAllTeachers();

            

            List<clsTeacher> teachers = new List<clsTeacher>();
            DataRow row ;


            for (int i = 0; i < dt.Rows.Count; i++) // start from second row (index 1)
            {
                 row = dt.Rows[i];

                teachers.Add(new clsTeacher
                {
                    TeacherID = Convert.ToInt32(row["TeacherID"]),
                    Name = row["Name"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Address = row["Address"].ToString(),
                    Gendor = Convert.ToByte(row["Gendor"]),
                    Percentage = Convert.ToInt32(row["Percentage"]),
                });
            }

            if (teachers.Count == 0)
            {
                return NotFound();
            }

            return Ok(teachers);
        }

        [HttpGet("{id}", Name = "GetTeacherByID")]

        public ActionResult<clsTeacher> GetTeacherByID(int id)
        {
            if (id < 1) return BadRequest($"Not Accepted ID : {id}");

            clsTeacher teacher = clsTeacher.Find(id) ;

            if(teacher == null) return NotFound($"Can not Find Teacher With ID = {id}");

            return Ok(teacher);

        }

        [HttpPost("AddNewTeacher")]

        public ActionResult<clsTeacher> AddNewTeacher(clsTeacher newTeacher)
        {
            if (newTeacher == null) return BadRequest("Invalid Teacher Data");

            clsTeacher teacher = new clsTeacher(newTeacher.TeacherID,newTeacher.Name,
                newTeacher.Phone , newTeacher.Address,newTeacher.Percentage,newTeacher.Gendor , clsTeacher.enMode.AddNew);
            teacher.Save();

            newTeacher.TeacherID = teacher.TeacherID;

            return CreatedAtRoute("GetTeacherByID", new {id =  newTeacher.TeacherID},newTeacher);
        }

        [HttpDelete("{id}",Name = "DeleteTeacher")]
        public ActionResult DeleteTeacher(int id)
        {
            if (id < 1) return BadRequest($"Not Accepted ID : {id}");

            if (clsTeacher.DeleteTeacher(id))
                return Ok($"Teacher With ID : {id} Deleted Successfully");
            else return NotFound($"Teacher With ID : {id} Was not Found");
        }

        [HttpPut("{id}", Name = "UpdateTeacher")]
        public ActionResult<clsTeacher> UpdateTeacher(int id , clsTeacher updatedTeacher)
        {
            if (id < 1) return BadRequest("Invalid ID");

            clsTeacher teacher = clsTeacher.Find(id);

            if (teacher == null) return NotFound($"Teacher With ID : {id} Was not Found");

            teacher.Name = updatedTeacher.Name;
            teacher.Phone = updatedTeacher.Phone;
            teacher.Address = updatedTeacher.Address;
            teacher.Percentage = updatedTeacher.Percentage;
            teacher.Gendor = updatedTeacher.Gendor;
            teacher.Save();

            return Ok(teacher);
        }


    }
}
