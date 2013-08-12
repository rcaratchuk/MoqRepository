using System;
using NUnit.Framework;
using System.Linq;

/*
//using System.Collections.Generic;
//using System.Linq;

namespace MoqRepositoryTests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestCase ()
		{
		}
	}
}
*/
using System.Collections.Generic;

namespace MoqRepositoryTests
{

	using Moq;

	using MoqRepositorySample;

	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestFixture] //defino como uma classe de teste no NUn
	public class UnitTest1
	{

		public UnitTest1()
		{

		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// 
		[SetUp]
		public void SetupUnitTest1()
		{
			Console.WriteLine (	"entrei no setu");
			// create some mock products to play with
			IList<Device> devices = new List<Device>
			{
				new Device { DeviceId = 1, Name = "Lâmpada 1", Description = "Short description here", Price = 49.99 },
				new Device { DeviceId = 2, Name = "Lâmpada 2", Description = "Short description here", Price = 59.99 },
				new Device { DeviceId = 3, Name = "Lâmpada 3", Description = "Short description here", Price = 29.99 }
			};

			// Mock the Products Repository using Moq
			Mock<IDeviceRepository> mockDeviceRepository = new Mock<IDeviceRepository>();

			// Return all the products
			mockDeviceRepository.Setup(mr => mr.FindAll()).Returns(devices);

			// return a product by Id
			mockDeviceRepository.Setup(mr => mr.FindById(It.IsAny<int>())).Returns((int i) => devices.Where(x => x.DeviceId == i).Single());

			// return a product by Name
			mockDeviceRepository.Setup(mr => mr.FindByName(It.IsAny<string>())).Returns((string s) => devices.Where(x => x.Name == s).Single());

			// Allows us to test saving a product
			mockDeviceRepository.Setup(mr => mr.Save(It.IsAny<Product>())).Returns(
				(Product target) =>
				{
				DateTime now = DateTime.Now;

				if (target.DeviceId.Equals(default(int)))
				{
					target.DateCreated = now;
					target.DateModified = now;
					target.DeviceId = devices.Count() + 1;
					devices.Add(target);
				}
				else
				{
					var original = devices.Where(q => q.DeviceId == target.DeviceId).Single();

					if (original == null)
					{
						return false;
					}

					original.Name = target.Name;
					original.Price = target.Price;
					original.Description = target.Description;
					original.DateModified = now;
				}

				return true;
			});

			// Complete the setup of our Mock Product Repository
			this.MockDevicesRepository = mockDeviceRepository.Object;
		}

		/// <summary>
		/// Gets or sets the test context which provides
		/// information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }

		/// <summary>
		/// Our Mock Products Repository for use in testing
		/// </summary>
		public  IDeviceRepository MockDevicesRepository;

		/// <summary>
		/// Can we return a product By Id?
		/// </summary>
		[Test]
		public void CanReturnDeviceById()
		{
			// Try finding a product by id
			Console.WriteLine ("entrei no teste");
			Device testDevice = this.MockDevicesRepository.FindById(2);

			Assert.IsNotNull(testDevice); // Test if null

			Assert.IsInstanceOf<Device>(testDevice); // Test type
			Assert.AreEqual("Lâmpada 2", testDevice.Name); // Verify it is the right product
		}

		/// <summary>
		/// Can we return a product By Name?
		/// </summary>
		[Test]
		public void CanReturnProductByName()
		{
			// Try finding a product by Name
			Device testDevice = this.MockDevicesRepository.FindByName("Lâmpada 1");

			Assert.IsNotNull(testDevice); // Test if null
			Assert.IsInstanceOf<Device>(testDevice); // Test type
			Assert.AreEqual(3, testDevice.DeviceId); // Verify it is the right product
		}

		/// <summary>
		/// Can we return all products?
		/// </summary>
		[Test]
		public void CanReturnAllDevices()
		{
			// Try finding all products
			IList<Device> testDevices = this.MockDevicesRepository.FindAll();
			Assert.IsNotNull(testDevices); // Test if null
			Assert.AreEqual(3, testDevices.Count); // Verify the correct Number
		}

		/// <summary>
		/// Can we insert a new product?
		/// </summary>
		[Test]
		public void CanInsertDevice()
		{
			// Create a new product, not I do not supply an id
			Device newDevice = new Device
			{ Name = "Pro C#", Description = "Short description here", Price = 39.99 };

			int deviceCount = this.MockDevicesRepository.FindAll().Count;
			Assert.AreEqual(3, deviceCount); // Verify the expected Number pre-insert

			// try saving our new product
			this.MockDevicesRepository.Save(newDevice);

			// demand a recount
			deviceCount = this.MockDevicesRepository.FindAll().Count;
			Assert.AreEqual(4, deviceCount); // Verify the expected Number post-insert

			// verify that our new product has been saved
			Device testDevice = this.MockDevicesRepository.FindByName("Pro C#");
			Assert.IsNotNull(testDevice); // Test if null
			Assert.IsInstanceOf<Device>(testDevice); // Test type
			Assert.AreEqual(4, testDevice.DeviceId); // Verify it has the expected productid
		}

		/// <summary>
		/// Can we update a prodict?
		/// </summary>
		[Test()]
		public void CanUpdateDevice()
		{
			// Find a product by id
			Device testDevice = this.MockDevicesRepository.FindById(1);

			// Change one of its properties
			testDevice.Name = "C# 3.5 Unleashed";

			
			// Save our changes.
			this.MockDevicesRepository.Save(testDevice);

			// Verify the change
		
			Assert.AreEqual("C# 3.5 Unleashed", this.MockDevicesRepository.FindById(1).Name);

		}
	}
}

