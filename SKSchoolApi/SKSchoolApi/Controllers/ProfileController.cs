using SKSchoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Threading.Tasks;
using System.Data;

namespace SKSchoolApi.Controllers
{
    public class ProfileController : ApiController
    {
        Database.DB record = new Database.DB();
        public DataSet Get(string command, string intschool_id, string intUser_id, string intAcademic_id)
        {
            Profile profile = new Profile();
            profile.intschool_id = Convert.ToInt32(intschool_id);
            profile.intUser_id = Convert.ToInt32(intUser_id);
            profile.intAcademic_id = Convert.ToInt32(intAcademic_id);
            DataSet ds = record.ProfileDetails(command, profile);
            return ds;
        }
        public async Task<HttpResponseMessage> Post(string command, string vchProfile, string intschool_id, string intAcademic_id, string intUser_id)
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                var uploadPath = HttpContext.Current.Server.MapPath("~/image");
                //HttpContext.Current.Server.MapPath("~/image");
                string path = uploadPath;
                var multipartFormDataStreamProvider = new CustomUploadMultipartFormProvider(uploadPath, vchProfile);

                await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                // Show all the key-value pairs.
                foreach (var key in multipartFormDataStreamProvider.FormData.AllKeys)
                {
                    foreach (var val in multipartFormDataStreamProvider.FormData.GetValues(key))
                    {
                        Console.WriteLine(string.Format("{0}: {1}", key, val));
                    }
                }

                Profile profile = new Profile();
                profile.intAcademic_id = Convert.ToInt32(intAcademic_id);
                profile.intschool_id = Convert.ToInt32(intschool_id);
                profile.intUser_id = Convert.ToInt32(intUser_id);
                profile.vchProfile = vchProfile;
                DataSet ds = record.ProfileDetails(command, profile);
                return new HttpResponseMessage(HttpStatusCode.OK);


            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent(e.Message)
                };
            }

        }
    }
}