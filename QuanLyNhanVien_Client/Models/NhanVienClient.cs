using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QuanLyNhanVien_Client.Models
{
    public class NhanVienClient
    {
        private string Base_URL = "http://localhost:51654/api/";

        public IEnumerable<NhanVien> FindALL()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.GetAsync("nhanvien").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content.ReadAsAsync<IEnumerable<NhanVien>>().Result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<NhanVien> FindALL_PHONGBAN()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.GetAsync("nhanvien_phongban/").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content.ReadAsAsync<IEnumerable<NhanVien>>().Result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public NhanVien Find(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.GetAsync("nhanvien/" + id).Result;
                if(responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content.ReadAsAsync<NhanVien>().Result;
                }
                return null;
            }
            catch
            {

                return null;
            }            
        }

        public bool Create(NhanVien nhanVien)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("nhanvien", nhanVien).Result;
                return responseMessage.IsSuccessStatusCode;
            }
            catch
            {
                throw;
            }
        }

        public bool Edit(NhanVien nhanVien)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.PutAsJsonAsync("nhanvien/" + nhanVien.MaNhanVien, nhanVien).Result;
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
                HttpResponseMessage responseMessage = client.DeleteAsync("nhanvien/" + id).Result;
                return responseMessage.IsSuccessStatusCode;
            }
            catch
            {
                throw;
            }
        }


    }
}