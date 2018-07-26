using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Storage;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using XamarinFirebaseHA.Helper;
using static XamarinFirebaseHA.Helper.BaseEnum;

namespace XamarinFirebaseHA
{
    public class DbFirebase
    {

        FirebaseClient client = new FirebaseClient("https://hwaproject-3da21.firebaseio.com/");
        public FirebaseAuth userAuth;
        public DbFirebase()
        {
            userAuth = new FirebaseAuth();
        }
        //düz işlem
        public async Task<ObservableCollection<Student>> getList()
        {
            try
            {
                var list = (await client
                            .Child("Student").OrderByKey().WithAuth(UserLocalData.userToken).LimitToLast(5)
                            .OnceAsync<Student>()).OrderByDescending(x => x.Key)
                                                  .Select(x =>
                                                  {
                                                      x.Object.key = x.Key;
                                                      return x;
                                                  })
                                                  .ToList();

                var obs = new ObservableCollection<Student>();
                foreach (var isx in list)
                {
                    obs.Add(isx.Object);
                }
                return obs;
          

            }
            catch (Exception ex)
            {
                if (ex.Message == "401 (Unauthorized)")
                {
                    await authUser();
                    return await getList();
                }

                return null;
            }

        }
        //value balı işem
        public async Task<ObservableCollection<Student>> getListQuery(string orderValue, string val, listCount count)
        {
            try
            {
                var list = (await client
                            .Child("Student").OrderBy(orderValue).StartAt(val).EndAt(val).LimitToLast((int)count).WithAuth(UserLocalData.userToken)
                            .OnceAsync<Student>()).OrderByDescending(x => x.Key)
                                                    .Select(x =>
                                                    {
                                                        x.Object.key = x.Key;
                                                        return x;
                                                    })
                                                  .ToList();
                var obs = new ObservableCollection<Student>();
                foreach (var isx in list)
                {
                    obs.Add(isx.Object);
                }
                return obs;

            }
            catch (Exception ex)
            {
                if (ex.Message == "401 (Unauthorized)")
                {
                    await authUser();
                    return await getListQuery( orderValue,  val,  count);
                }

                return null;
            }

        }

        //dbden key bazlı çekim işlemi
        public async Task<ObservableCollection<Student>> getListQuery(string key, listCount count)
        {
            try
            {
                var list = (await client
                            .Child("Student").OrderByKey().EndAt(key).LimitToLast((int)count).WithAuth(UserLocalData.userToken)
                            .OnceAsync<Student>()).OrderByDescending(x => x.Key).Select(x =>
                            {
                                x.Object.key = x.Key;
                                return x;
                            }).ToList();
                var obs = new ObservableCollection<Student>();
                foreach (var isx in list)
                {
                    obs.Add(isx.Object);
                }
                return obs;

            }
            catch (Exception ex)
            {
                if (ex.Message == "401 (Unauthorized)")
                {
                    await authUser();
                    return await getListQuery(key, count);
                }

                return null;
            }

        }

        public async Task authUser()
        {
            //todo base 64 user name and password
            var user = await App.firebaseAuthProvier.SignInWithEmailAndPasswordAsync("Bb@hwa.com", "123456Bb");
            UserLocalData.userToken = user.FirebaseToken;

        }
        public async Task<bool> saveUser(Student student)
        {
            try
            {

                await client.Child("Student/" + FirebaseKeyGenerator.Next())
                         .WithAuth(UserLocalData.userToken)
                            .PutAsync(student);

                return true;

            }
            catch (Exception ex)
            {
                if (ex.Message == "401 (Unauthorized)")
                {
                    await authUser();
                    return await saveUser(student);
                }

                Debug.WriteLine("bir hatayla karşılaşıldı {0}", ex);
                return false;
            }
        }

        async public Task<string> saveImage(Stream imgStream)
        {
            var stroageImage = await new FirebaseStorage("hardwareandro-6293a.appspot.com")
                .Child("HardwareAndro")
                .PutAsync(imgStream);
            var imgurl = stroageImage;
            return imgurl;
        }

    }
}
