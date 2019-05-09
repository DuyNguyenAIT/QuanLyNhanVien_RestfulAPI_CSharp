using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QuanLyNhanVien_Client.Models
{
    public class ChucVuClient
    {
        private string Base_URL = "http://localhost:51654/api/";

        public IEnumerable<ChucVu> FindALL()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.GetAsync("chucvu").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content.ReadAsAsync<IEnumerable<ChucVu>>().Result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public ChucVu Find(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.GetAsync("chucvu/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content.ReadAsAsync<ChucVu>().Result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool Create(ChucVu chucvu)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("chucvu", chucvu).Result;
                return responseMessage.IsSuccessStatusCode;
            }
            catch
            {
                throw;
            }
        }

        public bool Edit(ChucVu chucvu)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.PutAsJsonAsync("chucvu/" + chucvu.MaChucVu, chucvu).Result;
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.DeleteAsync("chucvu/" + id).Result;
                return responseMessage.IsSuccessStatusCode;
            }
            catch
            {
                throw;
            }
        }
    }
}