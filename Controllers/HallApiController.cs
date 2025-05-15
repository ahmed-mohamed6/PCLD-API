using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace PCLD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallApiController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllHalls")]
        public ActionResult<IEnumerable<clsHall>> GetAllHall()
        {
            DataTable dt = clsHall.GetAllHalls();



            List<clsHall> Halls = new List<clsHall>();
            DataRow row;


            for (int i = 0; i < dt.Rows.Count; i++) // start from second row (index 1)
            {
                row = dt.Rows[i];

                Halls.Add(new clsHall
                {
                    HallID = Convert.ToInt32(row["HallID"]),
                    Name = row["Name"].ToString()
                });
            }

            if (Halls.Count == 0)
            {
                return NotFound();
            }

            return Ok(Halls);
        }

        [HttpGet("{id}", Name = "GetHallByID")]

        public ActionResult<clsTeacher> GetHallByID(int id)
        {
            if (id < 1) return BadRequest($"Not Accepted ID : {id}");

            clsHall Hall = clsHall.Find(id);

            if (Hall == null) return NotFound($"Can not Find Hall With ID = {id}");

            return Ok(Hall);

        }

        [HttpPost("AddNewStudent")]

        public ActionResult<clsTeacher> AddNewHall(clsHall newHall)
        {
            if (newHall == null) return BadRequest("Invalid Hall Data");

            clsHall Hall = new clsHall(newHall.HallID, newHall.Name, clsHall.enMode.AddNew);
            Hall.Save();

            newHall.HallID = Hall.HallID;

            return CreatedAtRoute("GetHallByID", new { id = newHall.HallID }, newHall);
        }

        [HttpDelete("{id}", Name = "DeleteHall")]
        public ActionResult DeleteHall(int id)
        {
            if (id < 1) return BadRequest($"Not Accepted ID : {id}");

            if (clsHall.DeleteHall(id))
                return Ok($"Hall With ID : {id} Deleted Successfully");
            else return NotFound($"Hall With ID : {id} Was not Found");
        }

        [HttpPut("{id}", Name = "UpdateHall")]
        public ActionResult<clsHall> UpdateHall(int id, clsHall updatedHall)
        {
            if (id < 1) return BadRequest("Invalid ID");

            clsHall Hall = clsHall.Find(id);

            if (Hall == null) return NotFound($"Hall With ID : {id} Was not Found");

            Hall.Name = updatedHall.Name;
            Hall.Save();

            return Ok(Hall);
        }
    }
}
