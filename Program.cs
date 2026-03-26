using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Dynamic;
using Employee_Busniss;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices;

namespace Course_21
{
    internal class Program
    { 

    static decimal ReturnBonase(decimal CurrentSalary,short Bonase)
        {
            return CurrentSalary + CurrentSalary *Bonase;
        }
      static void GetEmployee()
        {
            
            DataTable dt=clsEmployee_Bussnis.GetEmployees();
          

            decimal NewSalary = 0;




            foreach(DataRow dr in dt.Rows)
            { decimal CurrentSalary = 0;
                CurrentSalary = decimal.Parse(dr["Salary"].ToString());
                switch ((string)dr["Department"])
                {


                    case "IT":
                         switch (short.Parse(dr["PerformanceRating"].ToString()))
                         {  
                  
                    
                             case short Rating when Rating> 90:
                                NewSalary = ReturnBonase(CurrentSalary,(short)0.15);
                                break;
                          
                             case short Rating when Rating >= 75 && Rating <= 90:
                                NewSalary =  ReturnBonase(CurrentSalary, (short)0.1);
                                break;
                             case short Rating when Rating >= 50 && Rating <= 75:
                                 NewSalary = ReturnBonase(CurrentSalary, (short)0.5);
                               break;
                             case short Rating when Rating < 50:
                                 NewSalary = CurrentSalary;
                             break;

                        }
                        break;

                    case"HR":
                        switch (short.Parse(dr["PerformanceRating"].ToString()))
                        {


                            case short Rating when Rating > 90:
                                NewSalary = ReturnBonase(CurrentSalary, (short)0.15);
                                break;

                            case short Rating when Rating >= 75 && Rating <= 90:
                                NewSalary = ReturnBonase(CurrentSalary, (short)0.1);
                                break;
                            case short Rating when Rating >= 50 && Rating <= 75:
                                NewSalary = ReturnBonase(CurrentSalary, (short)0.5);
                                break;
                            case short Rating when Rating < 50:
                                NewSalary = CurrentSalary;
                                break;

                        }
                        break;

                    case "Marketing":
                        switch (short.Parse(dr["PerformanceRating"].ToString()))
                        {


                            case short Rating when Rating > 90:
                                NewSalary = ReturnBonase(CurrentSalary, (short)0.15);
                                break;

                            case short Rating when Rating >= 75 && Rating <= 90:
                                NewSalary = ReturnBonase(CurrentSalary, (short)0.1);
                                break;
                            case short Rating when Rating >= 50 && Rating <= 75:
                                NewSalary = ReturnBonase(CurrentSalary, (short)0.5);
                                break;
                            case short Rating when Rating < 50:
                                NewSalary = CurrentSalary;
                                break;

                        }
                        break;
                }
                
                if (clsEmployee_Bussnis.UpdateNewSalary((string)dr["Name"], NewSalary))
                {
                    Print((string)dr["Name"], NewSalary);
                }
                else
                {
                    Console.WriteLine("Failed");
                }
                   
            }


        }
      static void Print(string Name,decimal NewSalary)
        {
            Console.WriteLine($"{Name} New Salary Is {NewSalary}");
        }
        static void Main(string[] args)
        {

            string connectionString = "Server=.;Database=C21_DB1;User Id=sa;Password=sa123456;";
             SqlConnection connection = new SqlConnection(connectionString);
             SqlCommand command = new SqlCommand("SP_ADDNewPerson", connection);
            command.CommandType = CommandType.StoredProcedure;


            // Add parameters
            command.Parameters.AddWithValue("@FirstName", "John");
            command.Parameters.AddWithValue("@LastName", "Doe");
            command.Parameters.AddWithValue("@Email", "john.doe@example.com");
            SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);


            // Execute
            connection.Open();
            command.ExecuteNonQuery();


            // Retrieve the ID of the new person
            int newPersonID = (int)command.Parameters["@NewPersonID"].Value;
            Console.WriteLine($"New Person ID: {newPersonID}");


            connection.Close(); 
            Console.ReadKey();
        }


     
        
    }
}
