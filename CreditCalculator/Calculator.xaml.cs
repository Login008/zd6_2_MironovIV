using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CreditCalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calculator : ContentPage
    {
        public Calculator()
        {
            InitializeComponent();
        }

        private void Calculate(object sender, EventArgs e)
        {
            try //На случай ввода некорректных значений
            {
                if (sumView.Text != null && termView.Text != null && pickerView.SelectedItem != null) //проверка на пустые поля
                {
                    double loanAmount = double.Parse(sumView.Text); //присваиваем значения из полей и слайдера в переменные
                    int loanTerm = int.Parse(termView.Text);
                    double interestRate = sliderView.Value;
                    int paymentType = pickerView.SelectedIndex;

                    if (loanAmount > 0 && loanTerm > 0) //проверка на положительные значения
                    {
                        double monthlyPayment = 0; //инициализация переменных
                        double totalAmount = 0;
                        double overpayment = 0;

                        switch (paymentType) //разветвление в зависимости от выбранного вида платежа
                        {
                            case 0: //аннуитетный платёж
                                double monthlyRate = interestRate / 100 / 12; //вычисления
                                monthlyPayment = loanAmount * (monthlyRate * Math.Pow(1 + monthlyRate, loanTerm)) / (Math.Pow(1 + monthlyRate, loanTerm) - 1);
                                totalAmount = monthlyPayment * loanTerm;
                                overpayment = totalAmount - loanAmount;
                                break;
                            case 1: //дифференцированный платёж
                                monthlyRate = interestRate / 100 / 12; //вычисления
                                double principalPayment = loanAmount / loanTerm;
                                double firstPayment = principalPayment + (loanAmount * monthlyRate);
                                monthlyPayment = firstPayment;

                                totalAmount = 0;
                                for (int i = 0; i < loanTerm; i++) //перебор каждого месяца
                                {
                                    double interestPayment = (loanAmount - (principalPayment * i)) * monthlyRate; //вычисление общей суммы платежей
                                    totalAmount += principalPayment + interestPayment;
                                }
                                overpayment = totalAmount - loanAmount; //переплата
                                break;
                        }
                        paymentLabel.Text = $"Ежемесячный платёж: {monthlyPayment:F2} руб."; //вывод конечного результата
                        sumLabel.Text = $"Сумма: {totalAmount:F2} руб.";
                        repaymentLabel.Text = $"Переплата: {overpayment:F2} руб.";
                    }
                    else
                        DisplayAlert("Ошибка", "Введите только положительные значения", "OK");
                }
                else
                    DisplayAlert("Ошибка", "Заполните пустые поля и выберите вид платежа", "OK");
            }
            catch
            {
                DisplayAlert("Ошибка", "Введите корректные значения", "OK");
            }
        }
    }
}