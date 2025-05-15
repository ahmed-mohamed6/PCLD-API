using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsHall
    {
        public enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode = enMode.Update;

        public int HallID { get; set; }
        public string Name { get; set; }

        public clsHall()
        {
            this.HallID = -1;
            this.Name = string.Empty;

            _Mode = enMode.AddNew;
        }

        public clsHall(int HallID, string Name,enMode cMode = enMode.Update)
        {
            this.HallID = HallID;
            this.Name = Name;

            _Mode = cMode;
        }

        public bool _AddNewHall()
        {
            this.HallID = clsHallData.AddNewHall(this.Name);

            return this.HallID != -1;
        }

        private bool _UpdateHall()
        {
            return clsHallData.UpdateHall(this.HallID, this.Name);
        }

        public static clsHall Find(int HallID)
        {
            string Name = "";


            if (clsHallData.GetHallInfoByID(HallID, ref Name))
            {
                return new clsHall(HallID, Name);
            }
            else
                return null;

        }

        public static DataTable GetAllHalls()
        {
            return clsHallData.GetAllHalls();
        }

        public static bool DeleteHall(int id)
        {
            return clsHallData.DeleteHall(id);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewHall())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
                    break;
                case enMode.Update:
                    return _UpdateHall();
                    break;

            }
            return false;
        }
    }
}
