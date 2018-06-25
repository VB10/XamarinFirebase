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
        FirebaseClient client;
        public FirebaseAuth userAuth;
        public FirebaseAuthProvider firebaseAuthProvier;
        const string CONFIG_KEY = "AIzaSyBYYor9u8HOT5rz86qwFzxbMTaF4I2ZTZY";
        public DbFirebase()
        {
            client = new FirebaseClient("https://testproject-d9372.firebaseio.com/");
            userAuth = new FirebaseAuth();
            firebaseAuthProvier = new FirebaseAuthProvider(new FirebaseConfig(CONFIG_KEY));

        }

        public async Task<List<Student>> getList()
        {
            try
            {
                var list = (await client
                     .Child("Student")
                            .WithAuth(UserLocalData.userToken)
                     .OnceAsync<Student>())
             .Select(item =>
                     new Student
                     {
                         age = item.Object.age,
                         name = item.Object.name,
                         key = item.Key
                     }
                    ).ToList();
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
            var user = await firebaseAuthProvier.SignInWithEmailAndPasswordAsync("As" + "@hwa.com", "123456s");
            UserLocalData.removeDataAll();
            UserLocalData.userToken = user.FirebaseToken;

        }
        public async Task<bool> saveUser(Student student)
        {
            try
            {
                await client.Child(typeof(Student).Name).WithAuth(UserLocalData.userToken).PostAsync<Student>(student);
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

        async public Task saveImage(Stream imgStream)
        {
            var stroageImage = await new FirebaseStorage("hardwareandro-6293a.appspot.com")
                .Child("HardwareAndro")
                .PutAsync(imgStream);
            var imgurl = stroageImage;
        }

    }
}
