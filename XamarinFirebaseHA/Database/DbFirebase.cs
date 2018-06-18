using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Storage;
using Firebase.Xamarin.Database;

namespace XamarinFirebaseHA
{
	public class DbFirebase
	{
		FirebaseClient client;
		public DbFirebase()
		{
            client = new FirebaseClient("https://testproject-d9372.firebaseio.com/");
		}

		public async Task<List<Student>> getList()
		{
			var list = (await client
				.Child("Student")
				.OnceAsync<Student>())
				.Select(item =>
						new Student
						{
							age=item.Object.age,
							name=item.Object.name,
                            key = item.Key
			            }
                       ).ToList();
			return list;

		}

        public async Task<bool> saveUser(Student student)
        {
            try
            {
                await client.Child(typeof(Student).Name).PostAsync<Student>(student);
                return true;

            }
            catch (Exception ex)
            {
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
