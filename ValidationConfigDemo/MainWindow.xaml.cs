using System;
using System.Windows;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Models;


namespace ValidationConfigDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonValidation_Click(object sender, RoutedEventArgs e)
        {
            var product = new Product
            {
                Name = "The name beyond 10",
                Price = 120,
                EmailAddress = "xxxx.yyy.zzz"
            };

            #region 根据配置文件来验证成员

            var productValidator = ConfigurationValidatorFactory
                .FromConfigurationSource(ConfigurationSourceFactory.Create())
                .CreateValidator<Product>("Validation Ruleset");

            var results = productValidator.Validate(product);

            #endregion

            #region 编程来验证成员

            var emailAddressValidator = new RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            {
                MessageTemplate = "The email address is not correct."
            };
            var result = emailAddressValidator.Validate(product.EmailAddress);

            #endregion
        }

        private void ButtonValidationCustomer_Click(object sender, RoutedEventArgs e)
        {
            var cust = new Customer
            {
                FirstName = "Jerry",
                LastName = "He",
                DateOfBirth = DateTime.Now,
                Email = "Jerry.he",
            };

            #region 通过注解属性来验证，并使用RuleSetA规则来验证
            var validator = ValidationFactory.CreateValidator<Customer>("RuleSetA");
            var results = validator.Validate(cust);
            #endregion
        }
    }
}
