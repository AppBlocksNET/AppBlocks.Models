using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace AppBlocks.Models.Commands
{
    public class AuthenticateCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            Trace.WriteLine($"{this}.Execute({parameter}):{System.DateTime.Now.ToShortTimeString()} executed");
            if (parameter == null || parameter.ToString() == ":") return;
            Authenticate(parameter.ToString());
        }

        private static void Authenticate(string details)
        {
            if (string.IsNullOrEmpty(details)) return;// false;

            if (details.Contains(":"))
            {
                var detailsSplit = details.Split(':');

                Context.CurrentUser = Authenticate(detailsSplit[0], detailsSplit[1]);
            }

            return;
        }

        //private static bool Authenticate(string username, string password)
        //{
        //    var results = false;

        //    if (string.IsNullOrEmpty($"{username}{password}"))
        //    {
        //        Trace.WriteLine($"AppBlocks.Authenticate({username}):{results}");
        //        return false;
        //    }
        //}
        private static Item Authenticate(string username, string password)
        {
            //var broker =  WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.UseHttpPost,
            //new Uri("https://mysite.com/mobileauth/Microsoft"),
            //new Uri("myapp://"));
            //var results = broker.GetResults();

            //return !string.IsNullOrEmpty(results.ResponseData);
            //return User.Authenticate(username, password);
            //return username == "dave@grouplings.com" && password != "Gonecrazy!1";
            //var user = new Item().FromServiceAsync<User> (null).Result;
            return From($"?username={username}&password={password}");
        }

        /// <summary>
        /// FromUri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Item From(string source = null)
        {
            if (source == null || source.ToString().StartsWith("?"))
            {
                source = Context.AppBlocksServiceUrl + $"account/authenticate" + source?.ToString();
            }

            var uri = new Uri(source);
            //_logger?.LogInformation($"{typeof(Item).Name}.FromUri({uri}) started:{DateTime.Now.ToShortTimeString()}");
            var content = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.ServerCertificateValidationCallback = (message, cert, chain, errors) => { return true; };
                // response.GetResponseStream();
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    //_logger?.LogInformation($"HttpStatusCode:{response.StatusCode}");
                    var responseValue = string.Empty;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = $"{typeof(Item).FullName} ERROR: Request failedd. Received HTTP {response.StatusCode}";
                        throw new ApplicationException(message);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }

                    content = responseValue;

                    return new Item(content);
                }
            }
            catch (Exception exception)
            {
                //_logger?.LogInformation($"{nameof(Item)}.FromUri({uri}) ERROR:{exception.Message} {exception}");
                return null;
            }
            //_logger?.LogInformation($"content:{content}");

            //return FromJson<T>(content);
            return null;
        }
    }
}