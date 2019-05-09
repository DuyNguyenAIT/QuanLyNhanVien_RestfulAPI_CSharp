using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace QuanLyNhanVien_Client.Models
{
    public class NhanVien_PhongBanClient
    {
        private string Base_URL = "http://localhost:51654/api/";

        public IEnumerable<NhanVien_PhongBan> FindALL()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.GetAsync("nhanvien_phongban").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content.ReadAsAsync<IEnumerable<NhanVien_PhongBan>>().Result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public NhanVien_PhongBan Find(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.GetAsync("nhanvien_phongban/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage.Content.ReadAsAsync<NhanVien_PhongBan>().Result;
                }
                return null;
            }
            catch
            {

                return null;
            }
        }

        public bool Create(NhanVien_PhongBan nhanVien_phongban)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("nhanvien_phongban", nhanVien_phongban).Result;
                
                return responseMessage.IsSuccessStatusCode;
            }
            catch
            {
                throw;
            }
        }

        public bool Edit(NhanVien_PhongBan nhanVien_phongban)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = client.PutAsJsonAsync("nhanvien_phongban/" + nhanVien_phongban.MaNV_PB, nhanVien_phongban).Result;
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
                HttpResponseMessage responseMessage = client.DeleteAsync("nhanvien_phongban/" + id).Result;
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}