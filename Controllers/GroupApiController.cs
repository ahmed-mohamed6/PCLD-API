using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace PCLD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupApiController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllGroups")]
        public ActionResult<IEnumerable<clsStudent>> GetAllGroups()
        {
            DataTable dt = clsGroup.GetAllGroups();



            List<clsGroup> Groups = new List<clsGroup>();
            DataRow row;


            for (int i = 0; i < dt.Rows.Count; i++) // start from second row (index 1)
            {
                row = dt.Rows[i];

                Groups.Add(new clsGroup()
                {
                    GroupID = Convert.ToInt32(row["GroupID"]),
                    Name = row["Name"].ToString(),
                    Fees = Convert.ToInt32( row["Fees"]),
                    TeacherID = Convert.ToInt32(row["TeacherID"]),
                    CourseID = Convert.ToInt32(row["CourseID"]),


                });
            }

            if (Groups.Count == 0)
            {
                return NotFound();
            }

            return Ok(Groups);
        }

        [HttpGet("{id}", Name = "GetGroupByID")]

        public ActionResult<clsTeacher> GetGroupByID(int id)
        {
            if (id < 1) return BadRequest($"Not Accepted ID : {id}");

            clsGroup Group = clsGroup.Find(id);

            if (Group == null) return NotFound($"Can not Find Group With ID = {id}");

            return Ok(Group);

        }

        [HttpPost("AddNewStudent")]

        public ActionResult<clsGroup> AddNewTeacher(clsGroup newGroup)
        {
            if (newGroup == null) return BadRequest("Invalid Group Data");

            clsGroup Group = new clsGroup(newGroup.GroupID, newGroup.Name,
                newGroup.Fees, newGroup.TeacherID, newGroup.CourseID , clsGroup.enMode.AddNew);
            Group.Save();

            newGroup.GroupID = Group.GroupID;

            return CreatedAtRoute("GetGroupByID", new { id = newGroup.GroupID }, newGroup);
        }

        [HttpDelete("{id}", Name = "DeleteGroup")]
        public ActionResult DeleteGroup(int id)
        {
            if (id < 1) return BadRequest($"Not Accepted ID : {id}");

            if (clsGroup.DeleteGroup(id))
                return Ok($"Group With ID : {id} Deleted Successfully");
            else return NotFound($"Group With ID : {id} Was not Found");
        }

        [HttpPut("{id}", Name = "GroupStudent")]
        public ActionResult<clsGroup> UpdateGroup(int id, clsGroup updatedGroup)
        {
            if (id < 1) return BadRequest("Invalid ID");

            clsGroup Group = clsGroup.Find(id);

            if (Group == null) return NotFound($"Group With ID : {id} Was not Found");

            Group.Name = updatedGroup.Name;
            Group.Fees = updatedGroup.Fees;
            Group.TeacherID = updatedGroup.TeacherID;
            Group.CourseID = updatedGroup.CourseID;
            Group.Save();

            return Ok(Group);
        }
    }
}
