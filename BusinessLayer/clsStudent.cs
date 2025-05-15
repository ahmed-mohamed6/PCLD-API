using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsStudent
    {
        public enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode = enMode.Update;

        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ParentPhone { get; set; }
        public byte Gendor { get; set; }
        public clsStudent()
        {
            StudentID = -1;
            Name = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
            ParentPhone = string.Empty;
            Gendor = 0;

            _Mode = enMode.AddNew;
        }
        public clsStudent(int StudentID, string Name, string Phone, string Address, string ParentPhone, byte Gendor , enMode cMode = enMode.Update)
        {
            this.StudentID = StudentID;
            this.Name = Name;
            this.Phone = Phone;
            this.Address = Address;
            this.ParentPhone = ParentPhone;
            this.Gendor = Gendor;

            _Mode = cMode;
        }

        private bool _AddNewStudent()
        {
            this.StudentID = clsStudentData.AddNewStudent(this.Name, this.Address,
                this.Phone, this.ParentPhone, this.Gendor);

            return this.StudentID != -1;
        }

        private bool _UpdateStudent()
        {
            return clsStudentData.UpdateStudent(this.StudentID, this.Name, this.Gendor
                , this.Address, this.Phone, this.ParentPhone);
        }

        public static bool DeleteStudent(int StudentID)
        {
            return clsStudentData.DeleteStudent(StudentID);
        }
        public static bool DeleteStudentFromGroup(int StudentID, int GroupID)
        {
            return DeleteStudentFromGroup(StudentID, GroupID);
        }

        public static DataTable GetAllStudents()
        {

            return clsStudentData.GetAllStudents();
        }
        public static DataTable GetAllStudentsByGroupID(int GroupID)
        {

            return clsStudentData.GetAllStudentsByGroupID(GroupID);
        }

        public static clsStudent Find(int StudentID)
        {
            string Name = "";
            string Phone = "";
            string ParentPhone = "", Address = "";
            byte Gendor = 0;

            if (clsStudentData.GetStudentInfoByID(StudentID, ref Name,
                ref Phone, ref Address, ref ParentPhone, ref Gendor))
            {
                return new clsStudent(StudentID, Name, Phone, Address, ParentPhone, Gendor);
            }
            else
                return null;

        }

        public static bool AddNewStudentToGroup(int StudentID, int GroupID)
        {
            //int SG_ID = clsStudentGroupData.AddNewStudentToGroup(StudentID, GroupID);

            //return SG_ID != -1;
            return false;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewStudent())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
                    break;
                case enMode.Update:
                    return _UpdateStudent();
                    break;

            }
            return false;
        }
    }
}
