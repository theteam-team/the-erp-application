using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.ViewModels.CRN_Tabels
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Customer
    {
        [Required]
        public string customer_id;
        public string first_name;
        public string middle_name;
        public string last_name;
        [EmailAddress]
        public string email;
        public uint phone_number;
        public string gender;
        public uint loyality_points;
        public uint type;
        public uint year_birth;
        public uint month_birth;
        public uint day_birth;
        public string company;
        public string company_email;
        public bool is_lead;
    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Address
    {
        public uint id;
        public string city;
        public string governate;
        public string street;
        public uint zip_code;
        public string customer_id;
        public string Crm_employee_id;

    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Interest
    {
        public uint interest_id;
        public string customer_id;
    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Customer_interest
    {
        public uint interest_id;
        public string customer_id;
        public uint level_of_interest;
    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Opportunities
    {
        [Required]
        public string opportunity_id;
        public string customer_id;
        public uint status;
        public double expected_revenue;    
        public string notes;
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string start_date;
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string end_data;
    };

    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Crm_employee
    {
        [Required]
        public string id;
        public string first_name;
        public string middle_name;
        public string last_name;
        public string email;
        public uint phone_number;
        public uint year_birth;
        public uint month_birth;
        public uint day_birth;
        public string gender;
        public uint points;
        public bool is_available;
        public string role_id;
    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Opportunities_details
    {
        public string opportunity_id;
        public string product_id;
    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe class Crm_roles
    {
       public string role_id;
        public string role;
    }
    public class Opportunities_product
    {
        [Required]
        public Opportunities Opportunities { get; set; }
        [Required]
        public List<string> product_id { get; set; }
    }   
}
