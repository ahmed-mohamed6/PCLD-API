using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace PCLD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllAppointments")]
        public ActionResult<IEnumerable<clsAppointment>> GetAllAppointments()
        {
            DataTable dt = clsAppointment.GetAllAppointments();



            List<clsAppointment> Appointments = new List<clsAppointment>();
            DataRow row;


            for (int i = 0; i < dt.Rows.Count; i++) // start from second row (index 1)
            {
                row = dt.Rows[i];

                Appointments.Add(new clsAppointment()
                {
                    AppointmentID = Convert.ToInt32(row["AppointmentID"]),
                    Day = row["Day"].ToString(),
                    StartTime = TimeSpan.Parse(row["StartTime"].ToString()),
                    EndTime = TimeSpan.Parse(row["EndTime"].ToString()),
                    GroupID = Convert.ToInt32(row["GroupID"]),
                    HallID = Convert.ToInt32(row["HallID"]),



                });
            }

            if (Appointments.Count == 0)
            {
                return NotFound();
            }

            return Ok(Appointments);
        }
        
        [HttpGet("GroupID/{GroupId}", Name = "GetAppointmentsByGroupID")]
        public ActionResult<IEnumerable<clsAppointment>> GetAllAppointmentsByGroupID(int GroupId)
        {
            DataTable dt = clsAppointment.GetAppointmentsByGroupID(GroupId);



            List<clsAppointment> Appointments = new List<clsAppointment>();
            DataRow row;


            for (int i = 0; i < dt.Rows.Count; i++) // start from second row (index 1)
            {
                row = dt.Rows[i];

                Appointments.Add(new clsAppointment()
                {
                    AppointmentID = Convert.ToInt32(row["AppointmentID"]),
                    Day = row["Day"].ToString(),
                    StartTime = TimeSpan.Parse(row["StartTime"].ToString()),
                    EndTime = TimeSpan.Parse(row["EndTime"].ToString()),
                    GroupID = Convert.ToInt32(row["GroupID"]),
                    HallID = Convert.ToInt32(row["HallID"]),



                });
            }

            if (Appointments.Count == 0)
            {
                return NotFound();
            }

            return Ok(Appointments);
        }

        [HttpGet("HallID/{HallID}", Name = "GetAppointmentsByHallID")]
        public ActionResult<IEnumerable<clsAppointment>> GetAllAppointmentsByHallID(int HallID)
        {
            DataTable dt = clsAppointment.GetAppointmentsByHallID(HallID);



            List<clsAppointment> Appointments = new List<clsAppointment>();
            DataRow row;


            for (int i = 0; i < dt.Rows.Count; i++) // start from second row (index 1)
            {
                row = dt.Rows[i];

                Appointments.Add(new clsAppointment()
                {
                    AppointmentID = Convert.ToInt32(row["AppointmentID"]),
                    Day = row["Day"].ToString(),
                    StartTime = TimeSpan.Parse(row["StartTime"].ToString()),
                    EndTime = TimeSpan.Parse(row["EndTime"].ToString()),
                    GroupID = Convert.ToInt32(row["GroupID"]),
                    HallID = Convert.ToInt32(row["HallID"]),



                });
            }

            if (Appointments.Count == 0)
            {
                return NotFound();
            }

            return Ok(Appointments);
        }

        [HttpGet("{id}", Name = "GetAppointmentByID")]

        public ActionResult<clsAppointment> GetAppointmentByID(int id)
        {
            if (id < 1) return BadRequest($"Not Accepted ID : {id}");

            clsAppointment Appointment = clsAppointment.Find(id);

            if (Appointment == null) return NotFound($"Can not Find Appointment With ID = {id}");

            return Ok(Appointment);

        }

        [HttpPost("AddNewAppointment")]

        public ActionResult<clsGroup> AddNewTeacher(clsAppointment newAppointment)
        {
            if (newAppointment == null) return BadRequest("Invalid Appointment Data");

            clsAppointment Appointment = new clsAppointment(newAppointment.AppointmentID, newAppointment.Day, newAppointment.StartTime,
                newAppointment.EndTime, newAppointment.GroupID, newAppointment.HallID);

            newAppointment.GroupID = Appointment.GroupID;
            if (Appointment.AddNewAppointment())
            {
                
                return CreatedAtRoute("GetGroupByID", new { id = newAppointment.GroupID }, newAppointment);
            }

            return BadRequest("Was not Saved");
        }

        [HttpDelete("{id}", Name = "DeleteAppointment")]
        public ActionResult DeleteAppointment(int id)
        {
            if (id < 1) return BadRequest($"Not Accepted ID : {id}");

            if (clsGroup.DeleteGroup(id))
                return Ok($"Appointment With ID : {id} Deleted Successfully");
            else return NotFound($"Appointment With ID : {id} Was not Found");
        }

        
    }
}
