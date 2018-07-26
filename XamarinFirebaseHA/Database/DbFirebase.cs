using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Storage;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using XamarinFirebaseHA.Helper;

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

        public async Task<List<FirebaseObject<Student>>> getList()
        {
            try
            {
                var list = (await client
                            .Child("Student").WithAuth(UserLocalData.userToken)
                            .OnceAsync<Student>()).ToList();
                return list;

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
