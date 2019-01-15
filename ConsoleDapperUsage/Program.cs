using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleDapperUsage
{
	class Program
	{
		static void Main(string[] args)
		{

			//InsertOperationWithNoParameters();

			//InsertOperationWithParameters(Guid.NewGuid(), "Yelda", "Ateş", DateTime.Now.AddYears(-19));

			//MultipleInsertOperationParameters(new List<OgrenciTanim> {
			//	new OgrenciTanim{ Id= Guid.NewGuid(), Name= "Melda2", Surname = "Ateşler", BirthDay = DateTime.Now.AddYears(-19) },
			//	new OgrenciTanim{ Id= Guid.NewGuid(), Name= "Helma2", Surname = "Ateşler", BirthDay = DateTime.Now.AddYears(-20) },
			//	new OgrenciTanim{ Id= Guid.NewGuid(), Name= "Jale2", Surname = "Ateşler", BirthDay = DateTime.Now.AddYears(-21) },
			//	new OgrenciTanim{ Id= Guid.NewGuid(), Name= "Şule2", Surname = "Ateşler", BirthDay = DateTime.Now.AddYears(-22) },
			//});

			//MultipleUpdateOperationWithoutParameters();

			//MultipleUpdateOperationWithParameters(new List<OgrenciTanim> {
			//	new OgrenciTanim{ Id= Guid.Parse("ECB95B40-BCE2-4E25-89FD-27BF659DF588"), Name= "MeldaY", Surname = "Ateşler", BirthDay = DateTime.Now.AddYears(-19) },
			//	new OgrenciTanim{ Id= Guid.Parse("B7F60AAF-547C-458B-92BD-FE649EC8D7D3"), Name= "HelmaY", Surname = "Ateşler", BirthDay = DateTime.Now.AddYears(-20) },
			//	new OgrenciTanim{ Id= Guid.Parse("F3081263-7704-40C7-97FD-B44B304A86B1"), Name= "JaleY", Surname = "Ateşler", BirthDay = DateTime.Now.AddYears(-21) },
			//	new OgrenciTanim{ Id= Guid.Parse("52908D36-89C7-4B30-8A1D-21AA70CB4778"), Name= "SevdaY", Surname = "Ateşler", BirthDay = DateTime.Now.AddYears(-22) },
			//});


			List<OgrenciTanim> ogrenciTanimList = GetOgrenciTanimList();

			foreach (OgrenciTanim item in ogrenciTanimList)
			{
				Console.WriteLine($"{item.Name} {item.Surname}. {item.Id}");
			}


			Console.ReadLine();
		}

		#region Dapper Insert Operation
		private static void InsertOperationWithNoParameters()
		{
			// Dapper'a yollanacak olan SQL sorgusu
			string sql = "insert into OgrenciTanim (Id, Name, Surname, Birthday) values ('AFB17DFD-7E2D-4FCA-BD62-22A6156D95C8','Ahmet','Cefakar','10.20.1992')";

			// SQL bağlantı cümlesi
			SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DapperMicOrm; User Id=sa; Password=abc1");

			sqlConnection.Open();
			sqlConnection.Execute(sql);
			sqlConnection.Close();

		}

		private static void InsertOperationWithParameters(Guid id, string name, string surname, DateTime birthday)
		{
			// Dapper'a yollanacak olan SQL sorgusu
			string sql = "insert into OgrenciTanim (Id, Name, Surname, Birthday) values (@Id,@Name,@Surname,@Birthday)";

			// SQL bağlantı cümlesi
			SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DapperMicOrm; User Id=sa; Password=abc1");

			sqlConnection.Open();
			sqlConnection.Execute(sql, new[]
				{
					new { Id = Guid.NewGuid(), Name = name, Surname = surname, BirthDay = birthday}
				});
			sqlConnection.Close();

		}

		private static void MultipleInsertOperationParameters(List<OgrenciTanim> ogrenciTanimList)
		{
			// Dapper'a yollanacak olan SQL sorgusu
			string sql = "insert into OgrenciTanim (Id, Name, Surname, Birthday) values (@Id,@Name,@Surname,@Birthday)";

			// SQL bağlantı cümlesi
			SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DapperMicOrm; User Id=sa; Password=abc1");

			sqlConnection.Open();
			sqlConnection.Execute(sql, ogrenciTanimList); // Aynı Sql komutu 'ogrenciTanimList' listedeki her bir eleman için çalıştırılacak.
			sqlConnection.Close();

		}
		#endregion

		#region Dapper Update Operation
		private static void UpdateOperationWithoutParameters()
		{
			// Dapper'a yollanacak olan SQL sorgusu
			string sql = @"update OgrenciTanim
						   set Birthday = '10.20.1994'
						   where Id = 'AFB17DFD-7E2D-4FCA-BD62-22A6156D95C8'";

			// SQL bağlantı cümlesi
			SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DapperMicOrm; User Id=sa; Password=abc1");

			sqlConnection.Open();
			sqlConnection.Execute(sql);
			sqlConnection.Close();

		}

		private static void MultipleUpdateOperationWithParameters(List<OgrenciTanim> ogrenciTanimList)
		{
			// Dapper'a yollanacak olan SQL sorgusu
			string sql = @"update OgrenciTanim
						  set 
						  Name = @Name,
						  Surname = @Surname,
						  Birthday = @BirthDay
						  where Id = @Id";

			// SQL bağlantı cümlesi
			SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DapperMicOrm; User Id=sa; Password=abc1");

			sqlConnection.Open();
			sqlConnection.Execute(sql, ogrenciTanimList);
			sqlConnection.Close();
		}


		#endregion

		#region Dapper Select Operation
		private static List<OgrenciTanim> GetOgrenciTanimList()
		{
			// Dapper'a yollanacak olan SQL sorgusu
			string sql = @"Select * From [OgrenciTanim]";

			// SQL bağlantı cümlesi
			SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DapperMicOrm; User Id=sa; Password=abc1");

			sqlConnection.Open();
			List<OgrenciTanim> ogrenciTanimList = sqlConnection.Query<OgrenciTanim>(sql).ToList();
			sqlConnection.Close();
			return ogrenciTanimList;

		}
		#endregion
	}

	#region Models
	public class OgrenciTanim
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime BirthDay { get; set; }
	}
	#endregion

}
