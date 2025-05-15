using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsGroup
    {
        public enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode = enMode.Update;

        public int GroupID { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }
        public int TeacherID { get; set; }
        public int CourseID { get; set; }

        public clsGroup()
        {
            this.GroupID = -1;
            this.Name = string.Empty;
            this.Fees = 0;
            this.TeacherID = 0;
            this.CourseID = 0;

            _Mode = enMode.AddNew;
        }

        public clsGroup(int GroupID, string Name, double Fees, int TeacherID, int CourseID,enMode cMode = enMode.Update)
        {
            this.GroupID = GroupID;
            this.Name = Name;
            this.Fees = Fees;
            this.TeacherID = TeacherID;
            this.CourseID = CourseID;

            _Mode = cMode;
        }

        public bool _AddNewGroup()
        {
            this.GroupID = clsGroupData.AddNewGroup(this.Name, this.Fees, this.TeacherID, this.CourseID);

            return this.GroupID != -1;
        }

        private bool _UpdateGroup()
        {
            return clsGroupData.UpdateGroup(this.GroupID, this.Name, this.Fees, this.TeacherID, this.CourseID);
        }

        static public DataTable GetAllGroups()
        {
            return clsGroupData.GetAllGroups();
        }

        static public clsGroup Find(int GroupID)
        {
            string Name = "";
            float Fees = 0;
            int TeacherID = 0;
            int CourseID = 0;


            if (clsGroupData.GetGroupInfoByID(GroupID, ref Name, ref Fees, ref TeacherID, ref CourseID))
            {
                return new clsGroup(GroupID, Name, Fees, TeacherID, CourseID);
            }
            else
                return null;

        }

        static public clsGroup Find(string Name)
        {
            int GroupID = -1;
            double Fees = 0;
            int TeacherID = 0;
            int CourseID = 0;


            if (clsGroupData.GetGroupInfoByName(ref GroupID, Name, ref Fees, ref TeacherID, ref CourseID))
            {
                return new clsGroup(GroupID, Name, Fees, TeacherID, CourseID);
            }
            else
                return null;

        }

        static public bool DeleteGroup(int GroupID)
        {
            return clsGroupData.DeleteGroup(GroupID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewGroup())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
                    break;
                case enMode.Update:
                    return _UpdateGroup();
                    break;

            }
            return false;
        }
    }
}
