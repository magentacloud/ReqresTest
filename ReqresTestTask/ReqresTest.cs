using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using ReqresTestTask.Models;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ReqresTestTask
{
    public class HttpResponse
    {
        public int StatusCode { get; set; }
        public string ResponseBody { get; set; }
    }
    public class ReqresTestSettings
    {
        public int TestUserId { get; set; }
        public string TestUserEmail { get; set; }
        public string TestUserFirstName { get; set; }
        public string TestUserLastName { get; set; }
        public string TestUserAvatarUrl { get; set; }
        public string TestUserPassword { get; set; }
        public string TestUserLoginToken { get; set; }
    }
    public class Tests
    {
        static readonly HttpClient Client = new HttpClient();
        public ReqresTestSettings settings = new ReqresTestSettings();
        
        [SetUp]
        public void Setup()
        {
            settings = JsonConvert.DeserializeObject<ReqresTestSettings>(File.ReadAllText("ReqresTestSettings.json"));
        }

        [Test]
        public void TestGetUserList()
        {
            try
            {

                Task<HttpResponse> resp = GetHttpResponse("https://reqres.in/api/users");
                HttpResponse result = resp.Result;

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                UserList userList = JsonConvert.DeserializeObject<UserList>(result.ResponseBody, new JsonSerializerSettings{
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                }); 

                Assert.AreEqual(200, result.StatusCode);
                Assert.Greater(userList.Data.Count, 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [Test]
        public void TestGetPageUserList()
        {
            try
            {

                Task<HttpResponse> resp = GetHttpResponse("https://reqres.in/api/users?page=2");
                HttpResponse result = resp.Result;

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                UserList userList = JsonConvert.DeserializeObject<UserList>(result.ResponseBody, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });

                Assert.AreEqual(200, result.StatusCode);
                Assert.AreEqual(2, userList.Page);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [Test]
        public void TestEmptyGetPageUserList()
        {
            try
            {

                Task<HttpResponse> resp = GetHttpResponse("https://reqres.in/api/users?page=4");
                HttpResponse result = resp.Result;

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                UserList userList = JsonConvert.DeserializeObject<UserList>(result.ResponseBody, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });

                Assert.AreEqual(200, result.StatusCode);
                Assert.Zero(userList.Data.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [Test]
        public void TestUserListPerPage()
        {
            try
            {

                Task<HttpResponse> resp = GetHttpResponse("https://reqres.in/api/users?per_page=2");
                HttpResponse result = resp.Result;

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                UserList userList = JsonConvert.DeserializeObject<UserList>(result.ResponseBody, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });

                Assert.AreEqual(200, result.StatusCode);
                Assert.AreEqual(2, userList.Data.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [Test]
        public void TestEmailUserDataFromUserList()
        {
            try
            {

                Task<HttpResponse> resp = GetHttpResponse("https://reqres.in/api/users?page=2");
                HttpResponse result = resp.Result;

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                UserList userList = JsonConvert.DeserializeObject<UserList>(result.ResponseBody, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });
                //Тут вычисляется индекс записи с нужным пользователем из секции data на основе Id пользователя, параметров из ответа page и per_page
                int userIndexOnPage = settings.TestUserId - (userList.PerPage * (userList.Page - 1)) - 1;

                Assert.AreEqual(200, result.StatusCode);
                Assert.AreEqual(settings.TestUserEmail, userList.Data[userIndexOnPage].Email);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [Test]
        public void TestIdUserDataFromUserList()
        {
            try
            {

                Task<HttpResponse> resp = GetHttpResponse("https://reqres.in/api/users?page=2");
                HttpResponse result = resp.Result;

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                UserList userList = JsonConvert.DeserializeObject<UserList>(result.ResponseBody, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });
                //Тут вычисляется индекс записи с нужным пользователем из секции data на основе Id пользователя, параметров из ответа page и per_page
                int userIndexOnPage = settings.TestUserId - (userList.PerPage * (userList.Page - 1)) - 1;

                Assert.AreEqual(200, result.StatusCode);
                Assert.AreEqual(settings.TestUserId, userList.Data[userIndexOnPage].Id);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [Test]
        public void TestFirstNameUserDataFromUserList()
        {
            try
            {

                Task<HttpResponse> resp = GetHttpResponse("https://reqres.in/api/users?page=2");
                HttpResponse result = resp.Result;

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                UserList userList = JsonConvert.DeserializeObject<UserList>(result.ResponseBody, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });
                //Тут вычисляется индекс записи с нужным пользователем из секции data на основе Id пользователя, параметров из ответа page и per_page
                int userIndexOnPage = settings.TestUserId - (userList.PerPage * (userList.Page - 1)) - 1;

                Assert.AreEqual(200, result.StatusCode);
                Assert.AreEqual(settings.TestUserFirstName, userList.Data[userIndexOnPage].FirstName);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [Test]
        public void TestLastNameUserDataFromUserList()
        {
            try
            {
                Task<HttpResponse> resp = GetHttpResponse("https://reqres.in/api/users?page=2");
                HttpResponse result = resp.Result;

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                UserList userList = JsonConvert.DeserializeObject<UserList>(result.ResponseBody, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });
                //Тут вычисляется индекс записи с нужным пользователем из секции data на основе Id пользователя, параметров из ответа page и per_page
                int userIndexOnPage = settings.TestUserId - (userList.PerPage * (userList.Page - 1)) - 1;

                Assert.AreEqual(200, result.StatusCode);
                Assert.AreEqual(settings.TestUserLastName, userList.Data[userIndexOnPage].LastName);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [Test]
        public void TestAvatarNameUserDataFromUserList()
        {
            try
            {
                Task<HttpResponse> resp = GetHttpResponse("https://reqres.in/api/users?page=2");
                HttpResponse result = resp.Result;

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                UserList userList = JsonConvert.DeserializeObject<UserList>(result.ResponseBody, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });
                //Тут вычисляется индекс записи с нужным пользователем из секции data на основе Id пользователя, параметров из ответа page и per_page
                int userIndexOnPage = settings.TestUserId - (userList.PerPage * (userList.Page - 1)) - 1;

                Assert.AreEqual(200, result.StatusCode);
                Assert.AreEqual(settings.TestUserAvatarUrl, userList.Data[userIndexOnPage].Avatar);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [Test]
        public void TestSuccessfulLogin()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            UserLogin userLogin = new UserLogin();
            userLogin.Email = settings.TestUserEmail;
            userLogin.Password = settings.TestUserPassword;
            string json = JsonConvert.SerializeObject(userLogin, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            Task<HttpResponse> resp = PostHttpResponse("https://reqres.in/api/login", json);
            HttpResponse result = resp.Result;
            LoginToken loginToken = JsonConvert.DeserializeObject<LoginToken>(result.ResponseBody, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(settings.TestUserLoginToken, loginToken.Token);
        }
        [Test]
        public void TestUnsuccessfulLoginWithoutEmail()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            UserLogin userLogin = new UserLogin();
            userLogin.Email = "";
            userLogin.Password = settings.TestUserPassword;
            string json = JsonConvert.SerializeObject(userLogin, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            Task<HttpResponse> resp = PostHttpResponse("https://reqres.in/api/login", json);
            HttpResponse result = resp.Result;
            LoginError loginError = JsonConvert.DeserializeObject<LoginError>(result.ResponseBody, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Missing email or username", loginError.Error);
        }
        [Test]
        public void TestUnsuccessfulLoginWithoutPassword()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            UserLogin userLogin = new UserLogin();
            userLogin.Email = settings.TestUserEmail;
            userLogin.Password = "";
            string json = JsonConvert.SerializeObject(userLogin, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            Task<HttpResponse> resp = PostHttpResponse("https://reqres.in/api/login", json);
            HttpResponse result = resp.Result;
            LoginError loginError = JsonConvert.DeserializeObject<LoginError>(result.ResponseBody, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Missing password", loginError.Error);
        }
        [Test]
        public void TestUnsuccessfulLoginWithUncorrectEmail()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            UserLogin userLogin = new UserLogin();
            userLogin.Email = "111111";
            userLogin.Password = settings.TestUserPassword;
            string json = JsonConvert.SerializeObject(userLogin, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            Task<HttpResponse> resp = PostHttpResponse("https://reqres.in/api/login", json);
            HttpResponse result = resp.Result;
            LoginError loginError = JsonConvert.DeserializeObject<LoginError>(result.ResponseBody, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("user not found", loginError.Error);
        }
        public async Task<HttpResponse> GetHttpResponse(string url)
        {
            HttpResponse result = new HttpResponse();
            HttpResponseMessage response = await Client.GetAsync(url);
            result.StatusCode = (int)response.StatusCode;
            result.ResponseBody = await response.Content.ReadAsStringAsync();
            return result;
        }
        public async Task<HttpResponse> PostHttpResponse(string url, string data)
        {
            HttpResponse result = new HttpResponse();
            var stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PostAsync(url, stringContent);
            result.StatusCode = (int)response.StatusCode;
            result.ResponseBody = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}