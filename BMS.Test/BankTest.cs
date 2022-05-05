using BankManagementSystemService.Controllers;
using BankManagementSystemService.Data.Entities;
using BankManagementSystemService.Middleware.Error;
using BankManagementSystemService.Repositories.LoanModule;
using BankManagementSystemService.Repositories.Registration;
using BankManagementSystemService.Utilities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace BMS.Test
{
    public class BankTest
    {
        private BankController _bankController;
        private Mock<IRegisterService> _registerService;
        private Mock<ILoanService> _loanService;
        public Customer _customer;
        public Loan _loan;

        [SetUp]
        public void Setup()
        {
            _registerService = new Mock<IRegisterService>();
            _loanService = new Mock<ILoanService>();
            _customer = new Customer() { Name = "Naveen", Username = "Naveen", Password = "test@123", ContactNo = "9600209432", EmailAddress = "naveenkumarcbe6@gmail.com", Address = "Coimbatore", Country = "India", State = "TamilNadu", AccountType = "Savings", DOB = Convert.ToDateTime("13-07-1994"), CreatedDate = DateTime.UtcNow, Pan = "BPFPN9256N" };
            _loan = new Loan() { CustomerId=1, LoanType="Personal", LoanAmount=200000, LoanDate= DateTime.UtcNow, LoanDuration=24, ROI= 7};
        }

        [Test]
        public void CreateAccount_OkResult()
        {
            // Arrange
            _registerService.Setup(x => x.CreateAccount(_customer));
            var controller = new BankController(_registerService.Object, _loanService.Object);

            // Act
            var actionResult = controller.CreateAccount(_customer);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            Assert.IsNotNull(actionResult);
        }

        [TestCase("nav#gmail.com")]
        [TestCase("nav@gmail")]
        [TestCase("nav@gmailcom")]
        public void IsNotValidEmail(string value)
        {
            bool isValid = Utility.IsValidEmail(value);
            Assert.IsFalse(isValid);
        }

        [TestCase("nav@gmail.com")]
        [TestCase("naveen@outlook.com")]
        [TestCase("nav@cts.com")]
        public void IsValidEmail(string value)
        {
            bool isValid = Utility.IsValidEmail(value);
            Assert.IsTrue(isValid);
        }

        [TestCase("@3rsgdf")]
        [TestCase("awsdsf")]
        [TestCase("aqswzxcdef")]
        public void IsNotValidPan(string value)
        {
            bool isValid = Utility.IsValidPan(value);
            Assert.IsFalse(isValid);
        }

        [TestCase("BPFPN9256N")]
        public void IsValidPan(string value)
        {
            bool isValid = Utility.IsValidPan(value);
            Assert.IsTrue(isValid);
        }

        [TestCase("Abvcf43ed")]
        [TestCase("1234567890")]
        [TestCase("5647389201")]
        public void IsNotValidPhoneNumber(string value)
        {
            bool isValid = Utility.IsValidMobileNumber(value);
            Assert.IsFalse(isValid);
        }

        [TestCase("9600209432")]
        [TestCase("8610058161")]
        [TestCase("7200155557")]
        public void IsValidPhoneNumber(string value)
        {
            bool isValid = Utility.IsValidMobileNumber(value);
            Assert.IsTrue(isValid);
        }

        [Test]
        public void ApplyLoan_OkResult()
        {
            // Arrange
            _loanService.Setup(x => x.ApplyLoan(_loan));
            var controller = new BankController(_registerService.Object, _loanService.Object);

            // Act
            var actionResult = controller.ApplyLoan(_loan);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test]
        public void GetAllLoanDetails_OkResult()
        {
            // Arrange
            _loanService.Setup(x => x.GetAllLoanDetails());
            var controller = new BankController(_registerService.Object, _loanService.Object);

            // Act
            var actionResult = controller.GetAllLoanDetails();

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test]
        public void GetAccountDetails_OkResult()
        {
            // Arrange
            _registerService.Setup(x => x.GetAccountDetails());
            var controller = new BankController(_registerService.Object, _loanService.Object);

            // Act
            var actionResult = controller.GetAccountDetails();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            Assert.IsNotNull(actionResult);
        }


        [Test]
        public void CreateAccount_Throwexception()
        {
            // Arrange
            _customer.EmailAddress = "test#gmail.com";
            _registerService.Setup(x => x.CreateAccount(_customer)).Throws(new AppException());
            var controller = new BankController(_registerService.Object, _loanService.Object);

            // Assert
            Assert.Throws<AppException>(() => controller.CreateAccount(_customer));
        }
    }
}