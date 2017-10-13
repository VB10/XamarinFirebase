using System;
using System.Collections.Generic;
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
			client = new FirebaseClient("buraya kendi url girin");
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
							name=item.Object.name
			}).ToList();


			return list;

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
