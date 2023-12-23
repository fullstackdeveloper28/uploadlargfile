using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace WebAPIDemo.Controllers
{
    public class LoginController : ApiController
    {
        [Route("Angular/api/Login")]
        [HttpPost]
        public HttpResponseMessage Login(Login login)
        {
            DataTable ldt = new DataTable();
            List<Login> loginList = new List<Login>();

            bool isSuccess = true;
            HttpResponseMessage response = null;
            string InputData = string.Empty;
            Login login1 = new Login();
            try
            {
                ldt = DB.LoginDetails(login);

                if (ldt.Rows.Count > 0)
                {

                    for (int i = 0; i < ldt.Rows.Count; i++)
                    {

                        login1.LoginName = ldt.Rows[i]["LOGIN_NAME"].ToString();
                        login1.Password = ldt.Rows[i]["PASSWORD"].ToString();
                        login1.Mobile = ldt.Rows[i]["MOBILE"].ToString();
                        login1.Status = "Success";
                        loginList.Add(login1);
                    }
                }
                else
                {
                    isSuccess = false;
                    login1.Status = "Failed";
                    loginList.Add(login1);

                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                login1.Status = "error";
                loginList.Add(login1);
                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(InputData, Encoding.UTF8, "application/json")//"[{'Error':'InternalServerError'}]"
                };

            }
            InputData = InputData = new JavaScriptSerializer().Serialize(loginList);

            if (isSuccess)
            {


                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(InputData, Encoding.UTF8, "application/json")

                };
            }
            else
            {
                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(InputData, Encoding.UTF8, "application/json")//"[{'Error':'InternalServerError'}]"
                };
            }
            return response;
        }

        [Route("Angular/api/AddUpdate")]
        [HttpPost]
        public HttpResponseMessage InsertUpdateRegistration(Login login)
        {

            List<Login> loginList = new List<Login>();

            bool isSuccess = true;
            HttpResponseMessage response = null;
            string InputData = string.Empty;
            int iResult = 0;
            try
            {
                iResult = DB.InsertUpdateRegistration(login);

                if (iResult > 0)
                {
                    isSuccess = true;
                    login.Status = "Success";
                    loginList.Add(login);

                }
                else
                {
                    isSuccess = false;
                    login.Status = "Failed";
                    loginList.Add(login);


                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                login.Status = "error";
                loginList.Add(login);

                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("{'Status':'Failed'}", Encoding.UTF8, "application/json")//"[{'Error':'InternalServerError'}]"
                };

            }
            InputData = InputData = new JavaScriptSerializer().Serialize(loginList);

            if (isSuccess)
            {


                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(InputData, Encoding.UTF8, "application/json")

                };
            }
            else
            {
                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(InputData, Encoding.UTF8, "application/json")//"[{'Error':'InternalServerError'}]"
                };
            }
            return response;
        }

        [Route("Angular/api/UsersDetails")]
        [HttpGet]
        public HttpResponseMessage Users()
        {
            DataTable ldt = new DataTable();
            List<Login> usersList = new List<Login>();

            bool isSuccess = true;
            HttpResponseMessage response = null;
            string InputData = string.Empty;
            Login user = new Login();
            try
            {
                ldt = DB.UserDetails();

                if (ldt.Rows.Count > 0)
                {
                    //List<Login> usersList = new List<Login>();
                    usersList = (from DataRow dr in ldt.Rows
                                 select new Login()
                                 {
                                     Id = Convert.ToInt32(dr["Id"]),
                                     Name = dr["NAME"].ToString(),
                                     LoginName = dr["LOGIN_NAME"].ToString(),
                                     Password = dr["PASSWORD"].ToString(),
                                     Mobile = dr["MOBILE"].ToString(),
                                     Status = "Success",

                                 }).ToList();
                    //usersList.Clear();
                    //for (int i = 0; i < ldt.Rows.Count; i++)
                    //{
                    //    user.Id = Convert.ToInt32(ldt.Rows[i]["ID"]);
                    //    user.Name = ldt.Rows[i]["NAME"].ToString();
                    //    user.LoginName = ldt.Rows[i]["LOGIN_NAME"].ToString();
                    //    user.Password = ldt.Rows[i]["PASSWORD"].ToString();
                    //    user.Mobile = ldt.Rows[i]["MOBILE"].ToString();
                    //    user.Status = "Success";
                    //    usersList.Add(user);
                    //}
                }
                else
                {
                    //isSuccess = false;
                    user.Status = "Failed";
                    usersList.Add(user);

                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                user.Status = "error";
                usersList.Add(user);
                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(InputData, Encoding.UTF8, "application/json")//"[{'Error':'InternalServerError'}]"
                };

            }
            InputData = InputData = new JavaScriptSerializer().Serialize(usersList);

            if (isSuccess)
            {


                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(InputData, Encoding.UTF8, "application/json")

                };
            }
            else
            {
                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(InputData, Encoding.UTF8, "application/json")//"[{'Error':'InternalServerError'}]"
                };
            }
            return response;
        }
        [Route("Angular/api/UserDelete")]
        [HttpPost]
        public HttpResponseMessage DeleteUser(Login login)
        {

            List<Login> loginList = new List<Login>();

            bool isSuccess = true;
            HttpResponseMessage response = null;
            string InputData = string.Empty;
            int iResult = 0;
            try
            {
                iResult = DB.DeleteUser(login);

                if (iResult > 0)
                {
                    isSuccess = true;
                    login.Status = "Success";
                    loginList.Add(login);

                }
                else
                {
                    isSuccess = false;
                    login.Status = "Failed";
                    loginList.Add(login);


                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                login.Status = "error";
                loginList.Add(login);

                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("{'Status':'Failed'}", Encoding.UTF8, "application/json")//"[{'Error':'InternalServerError'}]"
                };

            }
            InputData = InputData = new JavaScriptSerializer().Serialize(loginList);

            if (isSuccess)
            {


                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(InputData, Encoding.UTF8, "application/json")

                };
            }
            else
            {
                response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(InputData, Encoding.UTF8, "application/json")//"[{'Error':'InternalServerError'}]"
                };
            }
            return response;
        }

        //[Route("Angular/api/UserDelete")]
        //[HttpDelete]
        //public HttpResponseMessage DeleteUser(Login login)
        //{

        //    List<Login> loginList = new List<Login>();
        //    //Login login = new Login();
        //    bool isSuccess = true;
        //    HttpResponseMessage response = null;
        //    string InputData = string.Empty;
        //    int iResult = 0;
        //    try
        //    {
        //        //iResult = DB.DeleteUser(Id);

        //        //if (iResult > 0)
        //        //{
        //        //    login.Id = Id;
        //        //    isSuccess = true;
        //        //    login.Status = "Success";
        //        //    loginList.Add(login);

        //        //}
        //        //else
        //        //{
        //        //    login.Id = Id;
        //        //    isSuccess = false;
        //        //    login.Status = "Failed";
        //        //    loginList.Add(login);


        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        isSuccess = false;
        //        login.Status = "error";
        //        loginList.Add(login);

        //        response = new HttpResponseMessage()
        //        {

        //            StatusCode = HttpStatusCode.InternalServerError,
        //            Content = new StringContent("{'Status':'Failed'}", Encoding.UTF8, "application/json")//"[{'Error':'InternalServerError'}]"
        //        };

        //    }
        //    InputData = InputData = new JavaScriptSerializer().Serialize(loginList);

        //    if (isSuccess)
        //    {


        //        response = new HttpResponseMessage()
        //        {

        //            StatusCode = HttpStatusCode.OK,
        //            Content = new StringContent(InputData, Encoding.UTF8, "application/json")

        //        };
        //    }
        //    else
        //    {
        //        response = new HttpResponseMessage()
        //        {

        //            StatusCode = HttpStatusCode.InternalServerError,
        //            Content = new StringContent(InputData, Encoding.UTF8, "application/json")//"[{'Error':'InternalServerError'}]"
        //        };
        //    }
        //    return response;
        //}

    }
}
public class Login
{
    public Int32 Id { get; set; }
    public String Name { get; set; }
    public String LoginName { get; set; }
    public string Password { get; set; }
    public string Mobile { get; set; }
    public string Status { get; set; }
}
public class DB
{
    public static string myconstring = ConfigurationManager.ConnectionStrings["AngularDB"].ConnectionString;
    public static DataTable LoginDetails(Login login)
    {
        DataTable ldt = new DataTable();
        try
        {
            using (SqlConnection con = new SqlConnection(myconstring))
            {
                SqlDataAdapter adapter1 = new SqlDataAdapter("GetLoginDetails", con);
                adapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter1.SelectCommand.Parameters.AddWithValue("@loginName", login.LoginName);
                adapter1.SelectCommand.Parameters.AddWithValue("@password", login.Password);

                adapter1.Fill(ldt);

            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return ldt;
    }
    public static DataTable UserDetails()
    {
        DataTable ldt = new DataTable();
        try
        {
            using (SqlConnection con = new SqlConnection(myconstring))
            {
                SqlDataAdapter adapter1 = new SqlDataAdapter("sp_GetUserDetails", con);
                adapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter1.Fill(ldt);

            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return ldt;
    }
    public static int InsertUpdateRegistration(Login login)
    {
        int output = 0;
        try
        {
            using (SqlConnection connection = new SqlConnection(myconstring))
            {

                SqlCommand cmd = new SqlCommand("sp_InsertUpdateRegistration", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", login.Id);
                cmd.Parameters.AddWithValue("@NAME", login.Name);
                cmd.Parameters.AddWithValue("@LOGIN_NAME", login.LoginName);
                cmd.Parameters.AddWithValue("@PASSWORD", login.Password);
                cmd.Parameters.AddWithValue("@MOBILE", login.Mobile);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                output = 1;
            }
        }
        catch (Exception ex)
        {

        }
        return output;
    }
    public static int DeleteUser(Login login)
    {
        int output = 0;
        try
        {
            using (SqlConnection connection = new SqlConnection(myconstring))
            {

                SqlCommand cmd = new SqlCommand("sp_deleteUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", login.Id);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                output = 1;
            }
        }
        catch (Exception ex)
        {

        }
        return output;
    }

}
