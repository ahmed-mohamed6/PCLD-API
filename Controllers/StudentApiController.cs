using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace PCLD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<clsStudent>> GetAllStudent()
        {
            DataTable dt = clsStudent.GetAllStudents();

            List<clsStudent> students = new List<clsStudent>();
            DataRow row;

            for (int i = 0; i < dt.Rows.Count; i++) // start from second row (index 1)
            {
                row = dt.Rows[i];

                students.Add(new clsStudent
                {
                    StudentID = Convert.ToInt32(row["StudentID"]),
                    Name = row["Name"].ToString(),
                    Phone = row["Phone"].ToString(),
                    ParentPhone = row["ParentPhone"].ToString(),
                    Address = row["Address"].ToString(),
                    Gendor = Convert.ToByte(row["Gendor"]),
                    
                });
            }

            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [HttpGet("{id}", Name = "GetStudentByID")]

        public ActionResult<clsTeacher> GetStudentByID(int id)
        {
            if (id < 1) return BadRequest($"Not Accepted ID : {id}");

            clsStudent Student = clsStudent.Find(id);

            if (Student == null) return NotFound($"Can not Find Student With ID = {id}");

            return Ok(Student);

        }

        [HttpPost("AddNewStudent")]

        public ActionResult<clsTeacher> AddNewStudent(clsStudent newStudent)
        {
            if (newStudent == null) return BadRequest("Invalid Student Data");

            clsStudent Student = new clsStudent(newStudent.StudentID , newStudent.Name , newStudent.Phone, newStudent.Address,
                newStudent.ParentPhone, newStudent.Gendor, clsStudent.enMode.AddNew);
            Student.Save();

            newStudent.StudentID = Student.StudentID;

            return CreatedAtRoute("GetStudentByID", new { id = newStudent.StudentID }, newStudent);
        }

        [HttpDelete("{id}", Name = "DeleteStudent")]
        public ActionResult DeleteStudent(int id)
        {
            if (id < 1) return BadRequest($"Not Accepted ID : {id}");

            if (clsStudent.DeleteStudent(id))
                return Ok($"Student With ID : {id} Deleted Successfully");
            else return NotFound($"Student With ID : {id} Was not Found");
        }

        [HttpPut("{id}", Name = "UpdateStudent")]
        public ActionResult<clsTeacher> UpdateStudent(int id, clsStudent updatedStudent)
        {
            if (id < 1) return BadRequest("Invalid ID");

            clsStudent student = clsStudent.Find(id);

            if (student == null) return NotFound($"Student With ID : {id} Was not Found");

            student.Name = updatedStudent.Name;
            student.Phone = updatedStudent.Phone;
            student.ParentPhone = updatedStudent.ParentPhone;
            student.Address = updatedStudent.Address;
            student.Gendor = updatedStudent.Gendor;
            student.Save();

            return Ok(student);
        }
    }
}
