using DataAccessLayer;
using System.Data;

namespace BusinessLayer
{
    public class clsTeacher
    {
        public enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode = enMode.Update;

        public int TeacherID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte Gendor { get; set; }
        public int Percentage { get; set; }
        public clsTeacher()
        {
            TeacherID = -1;
            Name = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
            Gendor = 0;
            Percentage = 0;

            _Mode = enMode.AddNew;
        }
        public clsTeacher(int TeacherID, string Name, string Phone, string Address, int Percentage, byte Gendor,enMode cMode = enMode.Update)
        {
            this.TeacherID = TeacherID;
            this.Name = Name;
            this.Phone = Phone;
            this.Address = Address;
            this.Percentage = Percentage;
            this.Gendor = Gendor;

            _Mode = cMode;
        }

        private bool _AddNewTeacher()
        {
            this.TeacherID = clsTeacherData.AddNewTeacher(this.Name, this.Address, this.Phone
                , this.Percentage, this.Gendor);

            return this.TeacherID != -1;
        }

        private bool _UpdateTeacher()
        {
            return clsTeacherData.UpdateTeacher(this.TeacherID, this.Name, this.Gendor,
                this.Address, this.Phone, this.Percentage);
        }

        static public DataTable GetAllTeachers()
        {
            return clsTeacherData.GetAllTeachers();
        }

        static public bool DeleteTeacher(int TeacherID)
        {
            return clsTeacherData.DeleteTeacher(TeacherID);
        }

        static public clsTeacher Find(int TeacherID)
        {
            string Name = "";
            string Phone = "";
            string Address = "";
            byte Gendor = 0;
            int Percentage = 0;

            if (clsTeacherData.GetTeacherInfoByID(TeacherID, ref Name,
                ref Phone, ref Address, ref Percentage, ref Gendor))
            {
                return new clsTeacher(TeacherID, Name, Phone, Address, Percentage, Gendor);
            }
            else
                return null;

        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTeacher())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
                    break;
                case enMode.Update:
                    return _UpdateTeacher();
                    break;

            }
            return false;
        }
    }
}
