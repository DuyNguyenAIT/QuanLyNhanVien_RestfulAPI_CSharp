using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QuanLyNhanVien_Client.Models
{
    public class PhongBanClient
    {
        private string Base_URL = "http://localhost:51654/api/";

        public IEnumerable<PhongBan> FindALL()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.GetAsync("phongban").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content.ReadAsAsync<IEnumerable<PhongBan>>().Result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public PhongBan Find(int? id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.GetAsync("phongban/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content.ReadAsAsync<PhongBan>().Result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool Create(PhongBan phongBan)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("phongban", phongBan).Result;
                return responseMessage.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(PhongBan phongBan)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.PutAsJsonAsync("phongban/" + phongBan.MaPhongBan, phongBan).Result;
                return responseMessage.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.DeleteAsync("phongban/" + id).Result;
                return responseMessage.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}