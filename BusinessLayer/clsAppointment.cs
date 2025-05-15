using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsAppointment
    {
        public int AppointmentID { get; set; }
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int GroupID { get; set; }
        public int HallID { get; set; }

        public clsAppointment()
        {
            AppointmentID = -1;
            StartTime = TimeSpan.Zero;
            EndTime = TimeSpan.Zero;
            GroupID = -1;
            HallID = -1;

        }

        public clsAppointment(int appointmentID, string day, TimeSpan startTime, TimeSpan endTime, int groupID, int hallID)
        {
            AppointmentID = appointmentID;
            Day = day;
            StartTime = startTime;
            EndTime = endTime;
            GroupID = groupID;
            HallID = hallID;
        }

        public bool AddNewAppointment()
        {

            this.AppointmentID = clsAppointmentData.AddNewAppointment(this.Day, this.StartTime, this.EndTime
                , this.GroupID, this.HallID);

            return (this.AppointmentID != -1);
        }

        public static DataTable GetAllAppointments()
        {
            return clsAppointmentData.GetAllAppointments();
        }
        public static DataTable GetAllAppointmentsByGroupID(int GroupID)
        {
            return clsAppointmentData.GetAllAppointmentsByGroupID(GroupID);
        }

        public static clsAppointment Find(int appointmentID)
        {
            string Day = null;
            TimeSpan StartTime = TimeSpan.Zero;
            TimeSpan EndTime = TimeSpan.Zero;
            int GroupID = -1;
            int HallID = -1;

            if (clsAppointmentData.GetAppointmentInfoByID(appointmentID, ref Day, ref StartTime,
               ref EndTime, ref HallID, ref GroupID))
            {
                return new clsAppointment(appointmentID, Day, StartTime, EndTime, GroupID, HallID);
            }
            else
            {
                return null;
            }
        }

        public static bool DeleteAppointment(int AppointmentID)
        {
            return DeleteAppointment(AppointmentID);

        }

        private static bool ArePeriodsOverlaping(clsTimePeriod P1, clsTimePeriod P2)
        {
            return !(P2.Start > P1.End || P1.Start > P2.End);
        }

        private static bool IsPeriodSelected(clsTimePeriod P2, string Day, int HallID)
        {

            DataTable dt = clsAppointmentData.GetAllPeriods();
            foreach (DataRow row in dt.Rows)
            {
                clsTimePeriod P1 =
                    new clsTimePeriod((TimeSpan)row["StartTime"], (TimeSpan)row["EndTime"]);

                if (Day == (string)row["Day"] && HallID == (int)row["HallID"])
                {

                    if (ArePeriodsOverlaping(P1, P2))
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public static bool IsSettingsValid(TimeSpan S, TimeSpan E, string Day, int HallID)
        {
            clsTimePeriod P2 = new clsTimePeriod(S, E);

            return IsPeriodSelected(P2, Day, HallID);
        }

        public static DataTable GetAppointmentsByHallID(int HallID)
        {
            return clsAppointmentData.GetAppointmentsByHallID(HallID);
        }
        public static DataTable GetAppointmentsByGroupID(int GroupID)
        {
            return clsAppointmentData.GetAppointmentsByGroupID(GroupID);
        }
    }
}
